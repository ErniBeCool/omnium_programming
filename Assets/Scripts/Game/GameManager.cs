using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private CharacterFactory characterFactory;
    [SerializeField] private Base playerBase;
    [SerializeField] private GameObject floatingTextTemplate;
    [SerializeField] private ItemsService itemsService;
    [SerializeField] private AudioSystemService audioService;
    [SerializeField] private SkillService skillService;

    public ScoreSystem scoreSystem;
    private int currentWave = 0;
    private int enemiesToSpawn = 5;
    private int enemiesAlive = 0;
    private float waveCooldown = 10f;
    private float waveTimer = 10f;
    private bool isWaveActive = false;
    private bool isGameStarted = false;

    public static GameManager Instance { get; private set; }
    public CharacterFactory CharacterFactory => characterFactory;
    public Base PlayerBase => playerBase ?? (playerBase = CreateDefaultBase());
    public int Score => scoreSystem != null ? scoreSystem.Score : 0;
    public ItemsService ItemsService => itemsService;
    public AudioSystemService AudioService => audioService;
    public SkillService SkillService => skillService;

    private Base CreateDefaultBase()
    {
        Debug.LogWarning("PlayerBase was null, creating a default one.");
        GameObject baseObj = new GameObject("DefaultBase");
        return baseObj.AddComponent<Base>();
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            if (characterFactory != null && characterFactory.transform.parent == null)
            {
                DontDestroyOnLoad(characterFactory.gameObject);
            }
            else if (characterFactory != null)
            {
                Debug.LogWarning("characterFactory is not a root GameObject. Moving to root.");
                characterFactory.transform.SetParent(null);
                DontDestroyOnLoad(characterFactory.gameObject);
            }
            Initialize();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Initialize()
    {
        scoreSystem = new ScoreSystem();
        if (playerBase == null)
        {
            GameObject baseObj = new GameObject("PlayerBase");
            playerBase = baseObj.AddComponent<Base>();
        }
        itemsService.Initialize();
        audioService.Initialize();
        skillService.Initialize();
    }

    public void StartGame()
    {
        if (isGameStarted)
        {
            Debug.Log("Game already started, ignoring StartGame call.");
            return;
        }

        if (characterFactory == null)
        {
            Debug.LogError("characterFactory is null in StartGame!");
            return;
        }

        Character player = characterFactory.GetCharacter(CharacterType.Player);
        if (player == null)
        {
            Debug.LogError("Failed to create player!");
            return;
        }
        player.transform.position = Vector3.zero;
        player.gameObject.SetActive(true);
        player.Initialize();
        player.LiveComponent.OnCharacterDeath += CharacterDeathHandler;

        if (playerBase == null)
        {
            Debug.LogError("playerBase is null! Creating a new one.");
            GameObject baseObj = new GameObject("PlayerBase");
            playerBase = baseObj.AddComponent<Base>();
        }
        playerBase.transform.position = Vector3.zero;

        if (scoreSystem == null)
        {
            Debug.LogError("scoreSystem is null!");
            scoreSystem = new ScoreSystem();
        }
        scoreSystem.StartGame();
        isGameStarted = true;
        StartNextWave();
    }

    private void Update()
    {
        if (isGameStarted && !isWaveActive)
        {
            waveTimer -= Time.deltaTime;
            if (waveTimer <= 0)
            {
                StartNextWave();
            }

            if (scoreSystem.Score >= 100 && scoreSystem.Score % 100 == 0)
            {
                SkillService.UpgradeHealth(10f); // +10 к здоровью
                SkillService.UpgradePickupRange(0.5f); // +0.5 к дистанции
            }
        }
    }

    private void StartNextWave()
    {
        currentWave++;
        enemiesAlive = enemiesToSpawn;
        isWaveActive = true;
        Debug.Log($"Wave {currentWave} started! Enemies: {enemiesToSpawn} (Triggered from: {(new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name})");

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            SpawnEnemy();
        }
        enemiesToSpawn += 2;
        waveTimer = waveCooldown;
    }

    private void SpawnEnemy()
    {
        if (characterFactory == null)
        {
            Debug.LogError("characterFactory is null!");
            return;
        }

        Character enemy = characterFactory.GetCharacter(CharacterType.DefaultEnemy);
        if (enemy == null)
        {
            Debug.LogError("Failed to get enemy from characterFactory!");
            return;
        }

        if (characterFactory.Player == null)
        {
            Debug.LogError("Player is null in characterFactory!");
            return;
        }

        Debug.Log("Spawning enemy: " + enemy.name);
        Vector3 playerPosition = characterFactory.Player.transform.position;
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        float spawnDistance = gameData != null ? Random.Range(10f, gameData.MaxSpawnOffset) : 15f;
        Vector3 spawnOffset = new Vector3(randomDirection.x * spawnDistance, 0, randomDirection.y * spawnDistance);
        enemy.transform.position = playerPosition + spawnOffset;
        enemy.gameObject.SetActive(true);
        enemy.Initialize();
        enemy.LiveComponent.OnCharacterDeath += CharacterDeathHandler;
    }

    private void CharacterDeathHandler(Character deathCharacter)
    {
        switch (deathCharacter.CharacterType)
        {
            case CharacterType.Player:
                GameOver();
                break;
            case CharacterType.DefaultEnemy:
                int scoreGained = deathCharacter.CharacterData.ScoreCost;
                scoreSystem.AddScore(scoreGained);
                enemiesAlive--;
                if (enemiesAlive <= 0 && isWaveActive)
                {
                    isWaveActive = false;
                    Debug.Log("Wave completed!");
                }
                ShowFloatingScore(deathCharacter.transform.position, scoreGained);
                
                ItemsService.SpawnItem(deathCharacter.transform.position);
                GameManager.Instance.AudioService.PlayDeathSound();
                break;
        }

        deathCharacter.gameObject.SetActive(false);
        characterFactory.ReturnCharacter(deathCharacter);
        deathCharacter.LiveComponent.OnCharacterDeath -= CharacterDeathHandler;
    }

    private void ShowFloatingScore(Vector3 position, int score)
    {
        if (floatingTextTemplate == null)
        {
            Debug.LogError("FloatingTextTemplate is not assigned!");
            return;
        }

        GameObject textObj = Instantiate(floatingTextTemplate, position + Vector3.up * 1f, Quaternion.identity);
        textObj.SetActive(true);
        TextMeshPro text = textObj.GetComponentInChildren<TextMeshPro>();
        if (text != null)
            text.text = "+" + score.ToString();
    }

    public void GameOver()
    {
        scoreSystem.Endgame();
        Debug.Log("Game Over");
        isWaveActive = false;
    }
}
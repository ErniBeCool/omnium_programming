using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private CharacterFactory characterFactory;
    [SerializeField] private Base playerBase;

    private ScoreSystem scoreSystem;
    private int currentWave = 0;
    private int enemiesToSpawn = 5; // Начальное количество врагов в волне
    private int enemiesAlive = 0;
    private float waveCooldown = 10f; // Перерыв между волнами
    private float waveTimer = 0f;
    private bool isWaveActive = false;

    public static GameManager Instance { get; private set; }
    public CharacterFactory CharacterFactory => characterFactory;
    public Base PlayerBase => playerBase;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
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
        //StartGame();
    }

    public void StartGame()
    {
        if (characterFactory == null)
        {
            Debug.LogError("characterFactory is null!");
            return;
        }

        Character player = characterFactory.GetCharacter(CharacterType.Player);
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

        scoreSystem.StartGame();
        StartNextWave();
    }

    private void Update()
    {
        if (!isWaveActive)
        {
            waveTimer -= Time.deltaTime;
            if (waveTimer <= 0)
            {
                StartNextWave();
            }
        }
    }

    private void StartNextWave()
    {
        currentWave++;
        enemiesAlive = enemiesToSpawn;
        isWaveActive = true;
        Debug.Log($"Wave {currentWave} started! Enemies: {enemiesToSpawn}");

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            SpawnEnemy();
        }
        enemiesToSpawn += 2; // Увеличиваем сложность каждой волны
        waveTimer = waveCooldown;
    }

    private void CharacterDeathHandler(Character deathCharacter)
    {
        switch (deathCharacter.CharacterType)
        {
            case CharacterType.Player:
                GameOver();
                break;
            case CharacterType.DefaultEnemy:
                scoreSystem.AddScore(deathCharacter.CharacterData.ScoreCost);
                enemiesAlive--;
                if (enemiesAlive <= 0 && isWaveActive)
                {
                    isWaveActive = false;
                    Debug.Log("Wave completed!");
                }
                break;
        }

        deathCharacter.gameObject.SetActive(false);
        characterFactory.ReturnCharacter(deathCharacter);
        deathCharacter.LiveComponent.OnCharacterDeath -= CharacterDeathHandler;
    }

    private void SpawnEnemy()
    {
        Character enemy = characterFactory.GetCharacter(CharacterType.DefaultEnemy);
        Vector3 playerPosition = characterFactory.Player.transform.position;
        Vector2 randomDirection = Random.insideUnitCircle.normalized; // Случайное направление на круге
        float spawnDistance = Random.Range(10f, gameData.MaxSpawnOffset); // Увеличиваем минимальное расстояние
        Vector3 spawnOffset = new Vector3(randomDirection.x * spawnDistance, 0, randomDirection.y * spawnDistance);
        enemy.transform.position = playerPosition + spawnOffset;
        enemy.gameObject.SetActive(true);
        enemy.Initialize();
        enemy.LiveComponent.OnCharacterDeath += CharacterDeathHandler;
    }

    public void GameOver()
    {
        scoreSystem.Endgame();
        Debug.Log("Game Over");
        isWaveActive = false;
    }
}
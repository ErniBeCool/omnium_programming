using UnityEngine;

public class Base : MonoBehaviour
{
    private float health = 100f;
    private float maxHealth = 100f;
    private int defenseLevel = 1;
    private int upgradeCost = 50;

    public float Health => health;
    public int DefenseLevel => defenseLevel;

    public void TakeDamage(float damage)
    {
        health -= damage / defenseLevel;
        if (health <= 0)
        {
            health = 0;
            gameObject.SetActive(false); // Деактивация вместо уничтожения
            GameManager.Instance.GameOver();
            Debug.Log("Base destroyed! Game Over!");
        }
    }

    public bool UpgradeBase(int playerScore, ScoreSystem scoreSystem)
    {
        if (playerScore >= upgradeCost)
        {
            scoreSystem.AddScore(-upgradeCost);
            defenseLevel++;
            maxHealth += 50f;
            health = maxHealth;
            upgradeCost += 50;
            Debug.Log($"Base upgraded! Defense Level: {defenseLevel}, Health: {health}");
            return true;
        }
        return false;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillService : MonoBehaviour
{
    private float healthBonus = 0f; // Бонус к здоровью
    private float pickupRangeBonus = 0f; // Бонус к дистанции подбора

    public float HealthBonus => healthBonus;
    public float PickupRangeBonus => pickupRangeBonus;

    public void Initialize()
    {
        // Начальные значения
        healthBonus = 0f;
        pickupRangeBonus = 0f;
    }

    // Улучшение здоровья
    public void UpgradeHealth(float amount)
    {
        healthBonus += amount;
        Debug.Log($"Health bonus increased to: {healthBonus}");
    }

    // Улучшение дистанции подбора
    public void UpgradePickupRange(float amount)
    {
        pickupRangeBonus += amount;
        Debug.Log($"Pickup range bonus increased to: {pickupRangeBonus}");
    }
}
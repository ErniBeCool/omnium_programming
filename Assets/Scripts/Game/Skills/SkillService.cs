using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillService : MonoBehaviour
{
    private float healthBonus = 0f; // ����� � ��������
    private float pickupRangeBonus = 0f; // ����� � ��������� �������

    public float HealthBonus => healthBonus;
    public float PickupRangeBonus => pickupRangeBonus;

    public void Initialize()
    {
        // ��������� ��������
        healthBonus = 0f;
        pickupRangeBonus = 0f;
    }

    // ��������� ��������
    public void UpgradeHealth(float amount)
    {
        healthBonus += amount;
        Debug.Log($"Health bonus increased to: {healthBonus}");
    }

    // ��������� ��������� �������
    public void UpgradePickupRange(float amount)
    {
        pickupRangeBonus += amount;
        Debug.Log($"Pickup range bonus increased to: {pickupRangeBonus}");
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int scoreCost;
    [SerializeField] private float timeBetweenAttacks;
    [SerializeField] private Transform characterTransform;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private float weaponDamage = 10f;
    [SerializeField] private int weaponLevel = 1;

    public float DefaultSpeed => speed;
    public int ScoreCost => scoreCost;
    public float TimeBetweenAttacks => timeBetweenAttacks;
    public Transform CharacterTransform => characterTransform;
    public CharacterController CharacterController => characterController;
    public float WeaponDamage => weaponDamage * weaponLevel;
    public int WeaponLevel => weaponLevel;

    public bool UpgradeWeapon(int playerScore, ScoreSystem scoreSystem)
    {
        int upgradeCost = weaponLevel * 30; // Стоимость улучшения
        if (playerScore >= upgradeCost)
        {
            scoreSystem.AddScore(-upgradeCost);
            weaponLevel++;
            Debug.Log($"Weapon upgraded! Level: {weaponLevel}, Damage: {WeaponDamage}");
            return true;
        }
        return false;
    }
}
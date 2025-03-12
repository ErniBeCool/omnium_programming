using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceItem : Item
{
    [SerializeField] private int scoreValue = 10; // Количество очков за предмет

    protected override void OnPickedUp()
    {
        GameManager.Instance.scoreSystem.AddScore(scoreValue); // Добавляем очки через ScoreSystem
        gameObject.SetActive(false); // Деактивируем предмет
    }
}
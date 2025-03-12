using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceItem : Item
{
    [SerializeField] private int scoreValue = 10; // ���������� ����� �� �������

    protected override void OnPickedUp()
    {
        GameManager.Instance.scoreSystem.AddScore(scoreValue); // ��������� ���� ����� ScoreSystem
        gameObject.SetActive(false); // ������������ �������
    }
}
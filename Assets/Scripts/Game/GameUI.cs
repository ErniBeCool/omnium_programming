using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Slider playerHealthSlider;
    [SerializeField] private Slider baseHealthSlider;
    [SerializeField] private GameObject playerHealthUI;
    [SerializeField] private GameObject baseHealthUI;

    private bool isInitialized = false;

    void Update()
    {
        if (!isInitialized)
        {
            if (GameManager.Instance == null || GameManager.Instance.CharacterFactory?.Player == null || GameManager.Instance.PlayerBase == null)
                return;

            if (playerHealthUI == null || baseHealthUI == null || playerHealthSlider == null || baseHealthSlider == null || scoreText == null)
            {
                Debug.LogError("One or more UI elements are not assigned in the inspector!");
                return;
            }

            playerHealthUI.transform.SetParent(GameManager.Instance.CharacterFactory.Player.transform, false);
            playerHealthUI.transform.localPosition = new Vector3(0, 2, 0);
            baseHealthUI.transform.SetParent(GameManager.Instance.PlayerBase.transform, false);
            baseHealthUI.transform.localPosition = new Vector3(0, 2, 0);

            playerHealthSlider.maxValue = GameManager.Instance.CharacterFactory.Player.LiveComponent.MaxHealth;
            baseHealthSlider.maxValue = GameManager.Instance.PlayerBase.Health;
            isInitialized = true;
        }

        if (isInitialized)
        {
            if (scoreText != null)
                scoreText.text = "Score: " + GameManager.Instance.Score.ToString();
            if (playerHealthSlider != null && GameManager.Instance.CharacterFactory.Player != null)
                playerHealthSlider.value = GameManager.Instance.CharacterFactory.Player.LiveComponent.Health;
            if (baseHealthSlider != null && GameManager.Instance.PlayerBase != null)
                baseHealthSlider.value = GameManager.Instance.PlayerBase.Health;
        }
    }
}
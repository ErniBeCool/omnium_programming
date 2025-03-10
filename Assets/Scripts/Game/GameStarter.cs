using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    private bool hasStarted = false;

    void Start()
    {
        if (!hasStarted && GameManager.Instance != null)
        {
            GameManager.Instance.StartGame();
            hasStarted = true;
        }
    }
}
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        
        SceneManager.LoadScene(2);
    }

    public void OpenSettings()
    {
        
        Debug.Log("Settings opened");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game quit"); 
    }
}
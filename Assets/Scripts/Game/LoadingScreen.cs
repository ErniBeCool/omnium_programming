using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private Slider progressBar;

    void Start()
    {
        StartCoroutine(LoadMainMenuAsync());
    }

    private System.Collections.IEnumerator LoadMainMenuAsync()
    {
        
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);

        
        while (!operation.isDone)
        {
            if (progressBar != null)
            {
                progressBar.value = operation.progress; 
            }
            yield return null;
        }
    }
}
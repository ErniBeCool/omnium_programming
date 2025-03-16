using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private Slider progressBar;
    
    [SerializeField] private GameObject menu;

    private IEnumerator coroutine;
    void Start()
    {
        coroutine = LoadMainMenuAsync();
        StartCoroutine(coroutine);
    }

    private IEnumerator LoadMainMenuAsync()
    {
        
        // AsyncOperation operation = SceneManager.LoadSceneAsync(1);

        
        /*
        while (!operation.isDone)
        {
            if (progressBar != null)
            {
                progressBar.value = operation.progress;
            }
            yield return null;
        }*/
        
        if (progressBar != null)
        {
            
            while (progressBar.value < 100f)
            {
                progressBar.value += Time.deltaTime;
                yield return null;
            }
            if (!Mathf.Approximately(progressBar.value, 100f))
            {
                progressBar.value += Time.deltaTime;
                yield return new WaitForSeconds(0.5f);
                
            }
        }
    }
}
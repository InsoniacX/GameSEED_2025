using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Loading : MonoBehaviour
{
    public GameObject loadingScreen;

    public int sceneID = 3;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(LoadSceneAsync(sceneID));
    }

    IEnumerator LoadSceneAsync(int sceneID)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID);
        loadingScreen.SetActive(true);
        while (!operation.isDone)
        {
            yield return null;
        }
        loadingScreen.SetActive(false);
    }
}

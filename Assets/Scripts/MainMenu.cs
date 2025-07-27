using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
        Time.timeScale = 1; // Ensure time scale is reset when loading a new scene
    }

    //public void Credits()
    //{
    //    Debug.Log("Loading Credits...");
    //    SceneManager.LoadScene("");
    //}

    public void ExitGame()
    {
        Debug.Log("Exitting game...");
        Application.Quit();
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public GameObject container;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            container.SetActive(true);
            Time.timeScale = 0; // Pause the game
        }
    }

    public void ResumeBtn()
    {
        container.SetActive(false);
        Time.timeScale = 1; // Resume the game
    }

    public void MainMenuBtn(int sceneID)
    {
        // Load the main menu scene (assuming it's named "MainMenu")
        SceneManager.LoadScene(sceneID);
        Time.timeScale = 1; // Ensure time scale is reset when loading a new scene
    }
}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance;

    [SerializeField] private GameObject container;

    // Fungsi untuk menampilkan game over
    //public void ShowGameOver(int score)
    //{
    //    // Tampilkan panel game over
    //    container.SetActive(true);

    //    // Update teks skor
    //    scoreText.text = "your score is : " + score.ToString();

    //    // Jeda game
    //    Time.timeScale = 0f;
    //}

    // Fungsi untuk mengulang game
    public void RetryGame()
    {
        // Lanjutkan waktu game
        Time.timeScale = 1f;

        // Muat ulang scene saat ini
        SceneManager.LoadScene(0);
    }

    // Fungsi untuk keluar dari game
    public void MainMenuBtn(int sceneID)
    {
        // Load the main menu scene (assuming it's named "MainMenu")
        SceneManager.LoadScene(sceneID);
        Time.timeScale = 1; // Ensure time scale is reset when loading a new scene
    }
}
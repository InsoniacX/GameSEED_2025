using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LogicScript : MonoBehaviour
{
    public float score;
    private float highScore;
    public GameObject container;
    public TMP_Text scoreText;
    public TMP_Text inGameScoreText;

    void Start()
    {
        highScore = PlayerPrefs.GetFloat("HighScore", 0);
    }

    private void FixedUpdate()
    {
        Debug.Log("Score : " + score);
    }

    public void addScore(float scoreTodAdd)
    {
        score += scoreTodAdd;
        Debug.Log($"Skor bertambah sebesar {scoreTodAdd}. Total skor: {score}", this);
    }

    public void gameOver()
    {
        Time.timeScale = 0f;
        container.SetActive(true);

        if (scoreText != null)
        {
            scoreText.text = "Your Score is : " + score.ToString("0");
        }

        if (score > highScore)
        {
            PlayerPrefs.SetFloat("HighScore", score);
            highScore = score;
        }
    }

    public void retryBtn()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void mainMenuBtn()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }
}
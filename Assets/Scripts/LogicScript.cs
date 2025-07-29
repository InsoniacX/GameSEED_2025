using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using TMPro;

public class LogicScript : MonoBehaviour
{

    public float score;
    private float highScore;
    public GameObject container;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        Debug.Log("Score : " + score);
    }

    public void addScore(float scoreTodAdd)
    {
        score += scoreTodAdd;
    }

    public void gameOver()
    {
        container.SetActive(true);
        if (score >  highScore)
        {
            highScore += score;
        }
    }

    public void retryBtn()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1; // Resume the game
    }

    public void mainMenuBtn()
    {
        // Load the main menu scene (assuming it's named "MainMenu")
        SceneManager.LoadScene(1);
        Time.timeScale = 1; // Ensure time scale is reset when loading a new scene
    }

}

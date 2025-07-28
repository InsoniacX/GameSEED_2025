using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class LogicScript : MonoBehaviour
{

    public float score;
    private float highScore;
    public GameObject player;

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
        SceneManager.LoadScene(0);
       
        if (score >  highScore)
        {
            highScore += score;
            PlayerPrefs.SetFloat("HighScore", score);
        }
    }
}

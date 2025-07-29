using UnityEngine;

public class PlayerColiison : MonoBehaviour
{
    private LogicScript logicScript;
    private bool isOnPlatform;

    private void Awake()
    {
        logicScript = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Skor Point"))
        {
            logicScript.addScore(20);
            collision.gameObject.SetActive(false);
        } else if (collision.CompareTag("Obstacle"))
        {
            logicScript.gameOver();
            collision.gameObject.SetActive(false);
        }
    }
}

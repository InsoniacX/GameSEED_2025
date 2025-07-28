using UnityEngine;

public class PlayerColiison : MonoBehaviour
{
    private LogicScript logicScript;
    private bool isOnPlatform;

    private float horizontalInput;
    private float playerStep;

    private void Awake()
    {
        logicScript = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");


        if (horizontalInput > 0.01f || horizontalInput < 0.01)
        {
            playerStep += horizontalInput;
            if (playerStep > 800 || playerStep < -800)
            {
                playerStep = 0;
                logicScript.addScore(10);
            }
        }
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

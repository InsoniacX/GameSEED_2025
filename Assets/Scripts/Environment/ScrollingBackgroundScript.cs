using UnityEngine;

public class ScrollingBackgroundScript : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Renderer bgRenderer;
    [SerializeField] private WallCheckScript wallCheckScript;
    MovingObjectScript[] allPlatforms;


    private void Start()
    {
        allPlatforms = Object.FindObjectsByType<MovingObjectScript>(FindObjectsSortMode.None);
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        bool isTouchingWallOrGround = wallCheckScript.isTouchingWall || wallCheckScript.isTouchingGround;

        bool moveRight = horizontalInput > 0.01f && !isTouchingWallOrGround;

        bool moveLeft = horizontalInput < -0.01f && !isTouchingWallOrGround;

        foreach (MovingObjectScript platform in allPlatforms)
        {
            if (platform.isPlayerOnBoat)
            {
                if (platform.isMovingRight)
                    moveRight = true;
                else
                    moveLeft = true;
            }
        }

        if (moveRight)
        {
            bgRenderer.material.mainTextureOffset += new Vector2(speed * Time.deltaTime, 0);
        }
        else if (moveLeft)
        {
            bgRenderer.material.mainTextureOffset -= new Vector2(speed * Time.deltaTime, 0);
        }

    }
}

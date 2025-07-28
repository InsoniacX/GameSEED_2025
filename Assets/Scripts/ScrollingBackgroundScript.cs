using UnityEngine;

public class ScrollingBackgroundScript : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Renderer bgRenderer;
    [SerializeField] private WallCheckScript wallCheckScript;
    [SerializeField] private MovingObjectScript movingObjectScript;

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        bool isTouchingWallOrGround = wallCheckScript.isTouchingWall || wallCheckScript.isTouchingGround;

        bool moveRight = (horizontalInput > 0.01f && !isTouchingWallOrGround) ||
                            (movingObjectScript.isPlayerOnBoat && movingObjectScript.isMovingRight);

        bool moveLeft = horizontalInput < -0.01f && !isTouchingWallOrGround ||
                            (movingObjectScript.isPlayerOnBoat && !movingObjectScript.isMovingRight);

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

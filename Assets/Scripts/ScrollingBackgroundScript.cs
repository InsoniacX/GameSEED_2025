using UnityEngine;

public class ScrollingBackgroundScript : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Renderer bgRenderer;
    [SerializeField] private WallCheckScript wallCheckScript;

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput > 0.01f && !wallCheckScript.isTouchingWall)
            bgRenderer.material.mainTextureOffset += new Vector2(speed * Time.deltaTime, 0);
        else if (horizontalInput < -0.01f && !wallCheckScript.isTouchingWall)
            bgRenderer.material.mainTextureOffset -= new Vector2(speed * Time.deltaTime, 0);

    }
}

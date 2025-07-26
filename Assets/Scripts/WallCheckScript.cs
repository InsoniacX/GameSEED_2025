using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class WallCheckScript : MonoBehaviour
{
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wall;
    [SerializeField] private LayerMask ground;
    const float wallRadius = 1f;
    public bool isTouchingWall = false;

    private bool isFacingRight = true;

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput > 0) isFacingRight = true;
        else if (horizontalInput < 0) isFacingRight = false;

        Vector2 direction = isFacingRight ? Vector2.right : Vector2.left;

        RaycastHit2D hit = Physics2D.Raycast(wallCheck.position, direction, wallRadius, ground);

        isTouchingWall = hit.collider != null;
    }
}

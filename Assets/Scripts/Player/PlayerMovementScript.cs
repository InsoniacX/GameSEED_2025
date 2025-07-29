//using NUnit.Framework.Constraints;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private Rigidbody2D playerBody;
    private Animator playerAnimation;
    private BoxCollider2D playerCollider;
    private float wallJumpCooldown;
    private float horizontalInput;
    public bool isKnockedBack = false;
    private float knockbackTimer;

    private void Awake()
    {
        playerBody = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<Animator>();
        playerCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (isKnockedBack)
        {
            knockbackTimer += Time.deltaTime;
            if (knockbackTimer >= 0.5f)
            {
                isKnockedBack = false;
                knockbackTimer = 0;
            }
            return;
        }

        horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput > 0.01f && transform.localScale.x < 0)
            Flip();
        else if (horizontalInput < -0.01f && transform.localScale.x > 0)
            Flip();

        playerAnimation.SetBool("isRunning", horizontalInput != 0);
        playerAnimation.SetBool("isGrounded", IsGrounded());

        if(wallJumpCooldown > 0.2f)
        {
            playerBody.linearVelocity = new Vector2(horizontalInput * speed, playerBody.linearVelocity.y);

            if (OnWall() && !IsGrounded())
            {
                playerBody.gravityScale = 0;
                playerBody.linearVelocity = Vector2.zero;
            }
            else
                playerBody.gravityScale = 3;

            if (Input.GetKeyDown(KeyCode.Space))
                Jump();

        }
        else
            wallJumpCooldown += Time.deltaTime;
    }

    private void Jump()
    {
        if (IsGrounded())
        {
            playerBody.linearVelocity = new Vector2(playerBody.linearVelocity.x, jumpPower);
            playerAnimation.SetTrigger("Jump");
        }
        else if(OnWall() && !IsGrounded())
        {
            if (horizontalInput == 0)
            {
                playerBody.linearVelocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
                playerBody.linearVelocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);

            wallJumpCooldown = 0;
        }
    }

    public void ApplyKnockback(Vector2 direction, float force, float duration = 0.5f)
    {
        isKnockedBack = true;
        knockbackTimer = 0f;
        playerBody.linearVelocity = Vector2.zero;
        playerBody.AddForce(direction.normalized * force, ForceMode2D.Impulse);
    }

    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private bool IsGrounded()
    {
        Debug.DrawRay(playerCollider.bounds.center + Vector3.left * 0.5f, Vector2.down * 0.2f, Color.red);

        RaycastHit2D raycastHit = Physics2D.BoxCast(playerCollider.bounds.center, playerCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool OnWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(playerCollider.bounds.center, playerCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    public bool CanAttack()
    {
        return horizontalInput == 0 && IsGrounded() && !OnWall();
    }
}

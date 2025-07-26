using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    [SerializeField] private float speed;
    private Rigidbody2D playerBody;
    private Animator playerAnimation;
    private bool isGrounded;

    private void Awake()
    {
        playerBody = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        playerBody.linearVelocity = new Vector2(horizontalInput * speed, playerBody.linearVelocity.y);

        if(horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if(horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            Jump();

        playerAnimation.SetBool("isRunning", horizontalInput != 0);
        playerAnimation.SetBool("isGrounded", isGrounded);
    }

    private void Jump()
    {
        playerBody.linearVelocity = new Vector2(playerBody.linearVelocity.x, speed);
        isGrounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}

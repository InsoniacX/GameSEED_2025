using UnityEngine;

public class PlayerColiison : MonoBehaviour
{
    private LogicScript logicScript;
    private bool isOnPlatform;

    private float horizontalInput;
    private float playerStep;
    private ParallaxBackground[] allParallax;
    private HingeJoint2D playerJoint;
    private Rigidbody2D rb;
    private Animator playerAnimation;

    private bool canGrabRope = true;
    private float ropeCooldown = 0.5f;
    private float ropeCooldownTimer = 0f;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<Animator>();
    }

    private void Awake()
    {
        logicScript = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        allParallax = UnityEngine.Object.FindObjectsByType<ParallaxBackground>(
    FindObjectsSortMode.None
);
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
                //logicScript.addScore(10);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerAnimation.SetBool("isSwing", false);
            HingeJoint2D joint = GetComponent<HingeJoint2D>();
            if (joint != null)
            {
                Destroy(joint);
                playerJoint = null;
                canGrabRope = false;
            }
        }

        if (!canGrabRope)
        {
            ropeCooldownTimer += Time.deltaTime;
            if (ropeCooldownTimer >= ropeCooldown)
            {
                canGrabRope = true;
                ropeCooldownTimer = 0f;
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Skor Point"))
        {
            logicScript.addScore(20);
            collision.gameObject.SetActive(false);
        } else if (collision.CompareTag("FallingLine"))
        {
            logicScript.gameOver();
            collision.gameObject.SetActive(false);
        } else if (collision.CompareTag("Portal"))
        {
            foreach (ParallaxBackground pb in allParallax)
            {
                pb.ResetParallax();
            }
        } 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("RopeSegment") && canGrabRope)
        {
            if (GetComponent<HingeJoint2D>() == null)
            {
                playerAnimation.SetBool("isSwing", true);
                playerJoint = gameObject.AddComponent<HingeJoint2D>();
                playerJoint.connectedBody = collision.rigidbody;
                playerJoint.autoConfigureConnectedAnchor = false;
                playerJoint.anchor = Vector2.zero;
                playerJoint.connectedAnchor = Vector2.zero;
            }
        }
    }
}

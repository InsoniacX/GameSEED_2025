using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyAI : MonoBehaviour
{
    [Header("AI Settings")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float detectionRange = 5f;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private float attackCooldown = 1.5f;
    [SerializeField] private int damage = 20;
    [SerializeField] private int contactDamage = 10;
    [SerializeField] private float damageCooldown = 2.0f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.1f;
    [SerializeField] private LayerMask groundLayer;

    private bool isGrounded;

    private Rigidbody2D enemyBody;
    //private Animator animator;
    private Transform player;
    private float attackTimer;
    private bool isFacingRight = true;
    private float lastContactDamageTime;

    private void Awake()
    {
        enemyBody = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    private void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (distance <= detectionRange && distance > attackRange)
        {
            MoveTowardsPlayer();
            //animator?.SetBool("isAttacking", false);
        }
        else if (distance <= attackRange)
        {
            AttackPlayer();
        }
        else
        {
            enemyBody.linearVelocity = new Vector2(0, enemyBody.linearVelocity.y);
            //animator?.SetBool("isRunning", false);
        }

        attackTimer += Time.deltaTime;
    }

    private void MoveTowardsPlayer()
    {
        //animator?.SetBool("isRunning", true);

        float direction = Mathf.Sign(player.position.x - transform.position.x);
        enemyBody.linearVelocity = new Vector2(direction * moveSpeed, enemyBody.linearVelocity.y);

        if ((direction > 0 && !isFacingRight) || (direction < 0 && isFacingRight))
            Flip();
    }

    private void AttackPlayer()
    {
        if (attackTimer < attackCooldown) return;
        //animator?.SetTrigger("Attack");

        if (Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            PlayerHealth health = player.GetComponent<PlayerHealth>();
            if (health != null && !health.IsDead())
            {
                health.TakeDamage(damage, transform);
            }
            attackTimer = 0;
        }
    }

    private void TryDamagePlayer(GameObject playerObject)
    {
        if (Time.time - lastContactDamageTime < damageCooldown)
            return;

        PlayerHealth playerHealth = playerObject.GetComponent<PlayerHealth>();
        if (playerHealth != null && !playerHealth.IsDead())
        {
            playerHealth.TakeDamage(contactDamage, transform);
            lastContactDamageTime = Time.time;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Abaikan tabrakan
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.collider);
            return;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            TryDamagePlayer(collision.gameObject);
        }
    }


    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
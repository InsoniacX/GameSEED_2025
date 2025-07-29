using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 50;
    //[SerializeField] private Animator enemyAnimator;
    [SerializeField] private Rigidbody2D enemyRigidbody;

    private int currentHealth;
    private bool isDead = false;

    private void Awake()
    {
        currentHealth = maxHealth;

        /*if (enemyAnimator == null)
            enemyAnimator = GetComponent<Animator>();*/

        if (enemyRigidbody == null)
            enemyRigidbody = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            //enemyAnimator?.SetTrigger("Hurt");
        }
    }

    private void Die()
    {
        isDead = true;
        Debug.Log($"{gameObject.name} has died.");

        //enemyAnimator?.SetTrigger("Die");

        // Disable movement or AI component (optional)
        if (TryGetComponent<EnemyAI>(out var ai))
            ai.enabled = false;

        // Disable collider and gravity (optional)
        if (TryGetComponent<Collider2D>(out var col))
            col.enabled = false;

        enemyRigidbody.linearVelocity = Vector2.zero;
        enemyRigidbody.simulated = false;

        // Optional: destroy after delay
        Destroy(gameObject, 2f);
    }

    public bool IsDead()
    {
        return isDead;
    }
}

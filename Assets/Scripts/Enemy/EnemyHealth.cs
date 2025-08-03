using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 50;
    //[SerializeField] private Animator enemyAnimator;
    [SerializeField] private Rigidbody2D enemyRigidbody;

    private int currentHealth;
    private bool isDead = false;
    public event System.Action OnDeath;

    public enum EnemyType
    {
        Normal,
        Middle,
        Boss
    }
    public EnemyType enemyType = EnemyType.Normal;

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
        Debug.Log($"{gameObject.name} took {amount} damage. Remaining health: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
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

    public void FloatForSeconds(float duration)
    {
        StartCoroutine(FloatCoroutine(duration));
    }

    private IEnumerator FloatCoroutine(float duration)
    {
        if (enemyRigidbody != null)
        {
            enemyRigidbody.gravityScale = -0.2f; // Naik pelan
            yield return new WaitForSeconds(duration);
            enemyRigidbody.gravityScale = 1f; // Normal kembali
        }
    }
}

using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 50;
    [SerializeField] private int scoreValue = 10; // Variabel ini sekarang akan digunakan
    //[SerializeField] private Animator enemyAnimator;
    [SerializeField] private Rigidbody2D enemyRigidbody;

    [Header("Knockback Settings")]
    [SerializeField] private float knockbackForce = 10f;
    [SerializeField] private float knockbackDuration = 0.2f;

    private int currentHealth;
    private bool isDead = false;
    // Ubah event agar menerima parameter 'int'
    public event System.Action<int> OnDeath;

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

        if (enemyRigidbody == null)
            enemyRigidbody = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int amount, Transform attackerTransform)
    {
        if (isDead) return;

        currentHealth -= amount;
        Debug.Log($"{gameObject.name} took {amount} damage. Remaining health: {currentHealth}");

        Knockback(attackerTransform);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        Debug.Log($"{gameObject.name} has died.");

        // Panggil event OnDeath dan kirimkan nilai 'scoreValue'
        OnDeath?.Invoke(scoreValue);

        if (TryGetComponent<EnemyAI>(out var ai))
            ai.enabled = false;

        if (TryGetComponent<Collider2D>(out var col))
            col.enabled = false;

        enemyRigidbody.linearVelocity = Vector2.zero;
        enemyRigidbody.simulated = false;

        Destroy(gameObject, 2f);
    }

    public bool IsDead()
    {
        return isDead;
    }

    private void Knockback(Transform attacker)
    {
        StartCoroutine(KnockbackCoroutine(attacker));
    }

    private IEnumerator KnockbackCoroutine(Transform attacker)
    {
        if (enemyRigidbody != null)
        {
            Vector2 direction = (transform.position - attacker.position).normalized;
            enemyRigidbody.AddForce(direction * knockbackForce, ForceMode2D.Impulse);

            EnemyAI ai = null;
            if (TryGetComponent<EnemyAI>(out ai))
            {
                ai.enabled = false;
            }

            yield return new WaitForSeconds(knockbackDuration);

            if (ai != null)
            {
                ai.enabled = true;
            }

            enemyRigidbody.linearVelocity = Vector2.zero;
        }
    }

    public void FloatForSeconds(float duration, Rigidbody2D rb)
    {
        StartCoroutine(FloatCoroutine(duration, rb));
    }

    private IEnumerator FloatCoroutine(float duration, Rigidbody2D rb)
    {
        if (rb != null)
        {
            rb.gravityScale = -0.2f;
            yield return new WaitForSeconds(duration);
            rb.gravityScale = 1f;
        }
    }
}
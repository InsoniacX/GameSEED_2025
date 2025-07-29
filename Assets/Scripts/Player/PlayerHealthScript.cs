using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private float invincibilityDuration = 1.0f;

    private int currentHealth;
    private bool isInvincible = false;
    private Rigidbody2D playerBody;
    private SpriteRenderer spriteRenderer;
    private PlayerMovementScript movementScript;
    private Animator playerAnimation;

    private void Awake()
    {
        currentHealth = maxHealth;
        playerBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        movementScript = GetComponent<PlayerMovementScript>();
        playerAnimation = GetComponent<Animator>();
    }

    public void TakeDamage(int damage, Transform source)
    {
        if (isInvincible || currentHealth <= 0)
            return;

        currentHealth -= damage;

        // Knockback dipindahkan ke movement
        if (movementScript != null)
        {
            movementScript.ApplyKnockback(source.position, 7f);
        }

        // Flash red
        StartCoroutine(DamageFlash());

        // Cek mati
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(InvincibilityFrames());
        }
    }

    private IEnumerator InvincibilityFrames()
    {
        isInvincible = true;

        float elapsed = 0f;
        while (elapsed < invincibilityDuration)
        {
            spriteRenderer.color = new Color(1, 0.5f, 0.5f); // merah muda
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.1f);
            elapsed += 0.2f;
        }

        isInvincible = false;
    }

    private IEnumerator DamageFlash()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        playerAnimation.SetTrigger("Hurt");
        spriteRenderer.color = Color.white;
    }

    private void Die()
    {
        playerAnimation.SetBool("IsDead", true);

        Debug.Log("Player has died.");
        playerBody.linearVelocity = Vector2.zero;
        playerBody.simulated = false;

        playerAnimation.SetTrigger("Die"); // bisa diganti efek mati/animasi

        StartCoroutine(RespawnAfterDelay(2f));
        //gameObject.SetActive(false);
    }

    private IEnumerator RespawnAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Respawn();
    }

    private void Respawn()
    {
        playerAnimation.SetBool("IsDead", false);
        currentHealth = maxHealth;
        transform.position = Vector2.zero; // atau posisi checkpoint
        playerBody.simulated = true;
        gameObject.SetActive(true);
        spriteRenderer.color = Color.white;
    }

    public bool IsDead()
    {
        return currentHealth <= 0;
    }

    public int GetCurrentHealth() => currentHealth;
    public int GetMaxHealth() => maxHealth;
}

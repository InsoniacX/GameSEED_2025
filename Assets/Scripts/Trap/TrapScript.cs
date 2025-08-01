using System.Collections;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    [Header("Trap Settings")]
    [SerializeField] private float activeDuration = 2f;
    [SerializeField] private float inactiveDuration = 3f;
    [SerializeField] private float riseSpeed = 5f;
    [SerializeField] private int spikeDamage = 30;
    [SerializeField] private float riseHeight = 1f;


    [Header("Knockback Settings")]
    [SerializeField] private float knockbackForce = 10f;
    [SerializeField] private Vector2 knockbackDirection = new(-1, 1);

    [Header("Components")]
    [SerializeField] private Collider2D damageCollider;
    [SerializeField] private SpriteRenderer spikeSprite;

    private Vector3 hiddenPosition;
    private Vector3 activePosition;
    private bool isActive = false;
    private float timer;

    private void Start()
    {
        hiddenPosition = transform.position;
        activePosition = hiddenPosition + Vector3.up * riseHeight;

        damageCollider.enabled = false;
        spikeSprite.enabled = false;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (!isActive && timer >= inactiveDuration)
        {
            CheckPlayerOnTop();
            StartCoroutine(ActivateTrap());
        }
        else if (isActive && timer >= activeDuration)
        {
            StartCoroutine(DeactivateTrap());
        }
    }

    private IEnumerator ActivateTrap()
    {
        isActive = true;
        timer = 0f;
        spikeSprite.enabled = true;

        while (transform.position.y < activePosition.y)
        {
            transform.position = Vector3.MoveTowards(transform.position, activePosition, riseSpeed * Time.deltaTime);
            yield return null;
        }

        damageCollider.enabled = true;
    }

    private IEnumerator DeactivateTrap()
    {
        isActive = false;
        timer = 0f;
        damageCollider.enabled = false;

        while (transform.position.y > hiddenPosition.y)
        {
            transform.position = Vector3.MoveTowards(transform.position, hiddenPosition, riseSpeed * Time.deltaTime);
            yield return null;
        }

        spikeSprite.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isActive || !collision.CompareTag("Player")) return;

        var health = collision.GetComponent<PlayerHealth>();
        if (health == null || health.IsDead()) return;

        // Player menyentuh spike saat aktif
        health.TakeDamage(spikeDamage, transform);

        if (collision.TryGetComponent<Rigidbody2D>(out var playerBody))
        {
            Vector2 force = knockbackDirection.normalized * knockbackForce;
            playerBody.linearVelocity = Vector2.zero;
            playerBody.AddForce(force, ForceMode2D.Impulse);
        }
    }

    private void CheckPlayerOnTop()
    {
        Collider2D player = Physics2D.OverlapBox(
            transform.position + Vector3.up * spikeSprite.bounds.extents.y,
            new Vector2(spikeSprite.bounds.size.x, 0.6f),
            0f,
            LayerMask.GetMask("Player") // pastikan Layer Player sudah diset
        );

        if (player != null && player.TryGetComponent<PlayerHealth>(out var health))
        {
            health.TakeDamage(9999, transform); // langsung mati
        }
    }

    // Debug visual area pemeriksaan
    private void OnDrawGizmosSelected()
    {
        if (spikeSprite != null)
        {
            Gizmos.color = Color.red;
            Vector3 pos = transform.position + Vector3.up * spikeSprite.bounds.extents.y;
            Gizmos.DrawWireCube(pos, new Vector3(spikeSprite.bounds.size.x, 0.1f, 0));
        }
    }
}

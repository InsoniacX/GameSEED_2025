using UnityEngine;

public class BubbleProjectileScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private float speed = 5f;
    [SerializeField] private int damageToBoss = 15;
    [SerializeField] private float floatDuration = 2f;

    private float direction;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetDirection(float dir)
    {
        direction = dir;
        gameObject.SetActive(true);
    }

    private void Update()
    {
        rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<EnemyHealth>(out var enemy))
        {
            switch (enemy.enemyType)
            {
                case EnemyType.Normal:
                case EnemyType.Middle:
                    enemy.FloatForSeconds(floatDuration);
                    break;
                case EnemyType.Boss:
                    enemy.TakeDamage(damageToBoss);
                    break;
            }

            gameObject.SetActive(false); // Nonaktifkan proyektil setelah mengenai musuh
        }
    }
}


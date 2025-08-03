using UnityEngine;
using System.Collections; // Tambahkan ini

public class BubbleProjectileScript : MonoBehaviour
{
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
        rb.linearVelocity = new Vector2(direction * speed, 0f);
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<EnemyHealth>(out var enemy))
        {
            switch (enemy.enemyType)
            {
                case EnemyHealth.EnemyType.Normal:
                case EnemyHealth.EnemyType.Middle:
                    enemy.FloatForSeconds(floatDuration, collision.GetComponent<Rigidbody2D>());
                    break;

                case EnemyHealth.EnemyType.Boss:
                    enemy.TakeDamage(damageToBoss, transform);
                    break;
            }

            gameObject.SetActive(false);
        }
    }
}
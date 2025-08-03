using UnityEngine;

public class BubbleGunScript : MonoBehaviour
{
    [Header("Weapon Settings")]
    public PlayerAttackScript playerAttackScript;
    public GameObject bubbleProjectilePrefab;
    public Transform shootPoint;
    public float cooldown = 1f;
    public float bubbleForce = 7f;

    private float cooldownTimer = Mathf.Infinity;

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (playerAttackScript.currentWeapon == PlayerAttackScript.WeaponType.BubbleGun &&
            Input.GetMouseButton(0) && cooldownTimer >= cooldown)
        {
            FireBubble();
        }
    }

    private void FireBubble()
    {
        cooldownTimer = 0f;

        GameObject bubble = Instantiate(bubbleProjectilePrefab, shootPoint.position, Quaternion.identity);
        Rigidbody2D rb = bubble.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            float direction = Mathf.Sign(transform.localScale.x);
            rb.linearVelocity = new Vector2(direction * bubbleForce, 0f);
        }
    }
}

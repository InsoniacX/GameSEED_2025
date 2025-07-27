using UnityEngine;

public class PlayerAttackScript : MonoBehaviour
{
    [Header("Range Attack")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform projectilePoint;
    [SerializeField] private GameObject[] projectiles;
    [SerializeField] private bool hasRangeAttack = false;

    private Animator playerAnimation;
    private PlayerScript playerMovement;
    private float cooldownTimer = Mathf.Infinity;

    [Header("Melee Attack")]
    [SerializeField] private float meleeCooldown;
    [SerializeField] private Transform meleePoint;
    [SerializeField] private float meleeRange;
    [SerializeField] private int meleeDamage;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float meleeAttackDelay;

    private void Awake()
    {
        playerAnimation = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerScript>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && cooldownTimer > attackCooldown && playerMovement.canAttack())
        {
            if (hasRangeAttack)
                RangeAttack();
            else
                MeleeAttack();

            cooldownTimer = 0;
        }

        cooldownTimer += Time.deltaTime;
    }

    public void EnableRangeAttack(bool enable)
    {
        hasRangeAttack = enable;
    }

    private void RangeAttack()
    {
        playerAnimation.SetTrigger("RangeAttack");

        int projectileIndex = FindProjectile();
        projectiles[projectileIndex].transform.position = projectilePoint.position;
        projectiles[projectileIndex].GetComponent<ProjectileScript>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private void MeleeAttack()
    {
        playerAnimation.SetTrigger("MeleeAttack");
        Invoke("DetectMeleeHit", meleeAttackDelay);
    }

    private void DetectMeleeHit()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(
            meleePoint.position,
            meleeRange,
            enemyLayer
        );
    }

    private int FindProjectile()
    {
        for (int i = 0; 1 < projectiles.Length; i++)
        {
            if (!projectiles[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

    // For Debugging purposes, to visualize the melee range in the editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        // Sesuaikan dengan bentuk hitbox (circle/rectangle)
        Gizmos.DrawWireSphere(meleePoint.position, meleeRange);
        // atau: Gizmos.DrawWireCube(attackPoint.position, new Vector2(meleeWidth, meleeHeight));
    }

}

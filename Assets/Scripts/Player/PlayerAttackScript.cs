using UnityEngine;

public class PlayerAttackScript : MonoBehaviour
{
    [Header("Melee Settings")]
    [SerializeField] private Transform meleePoint;
    [SerializeField] private float meleeRange = 1f;
    [SerializeField] private int meleeDamage = 25;
    [SerializeField] private LayerMask enemyLayer;

    [Header("Ranged Settings")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform projectilePoint;
    [SerializeField] private GameObject[] projectiles;

    private Animator playerAnimation;
    private PlayerMovementScript playerMovement;
    private float cooldownTimer = Mathf.Infinity;

    public enum WeaponType
    {
        Melee,
        MagicWand,
        BubbleGun,
    }
    public WeaponType currentWeapon = WeaponType.Melee;

    private void Awake()
    {
        playerAnimation = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovementScript>();
    }

    private void Update()
    {
        // ... (Logika ganti senjata)
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentWeapon = WeaponType.Melee;
            playerAnimation.SetInteger("WeaponID", 0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentWeapon = WeaponType.MagicWand;
            playerAnimation.SetInteger("WeaponID", 1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentWeapon = WeaponType.BubbleGun;
            playerAnimation.SetInteger("WeaponID", 2);
        }

        if (Input.GetMouseButtonDown(0) && cooldownTimer >= attackCooldown)
        {
            switch (currentWeapon)
            {
                case WeaponType.Melee:
                    MeleeAttack();
                    break;

                case WeaponType.MagicWand:
                    FireProjectile();
                    playerAnimation.SetTrigger("Attack_MagicWand");
                    cooldownTimer = 0f;
                    break;

                case WeaponType.BubbleGun:
                    FireProjectile();
                    playerAnimation.SetTrigger("Attack_BubbleGun");
                    cooldownTimer = 0f;
                    break;
            }
        }

        cooldownTimer += Time.deltaTime;
    }

    private void MeleeAttack()
    {
        playerAnimation.SetTrigger("Attack_Melee");
        cooldownTimer = 0f;

        // Deteksi musuh dalam jangkauan
        Collider2D[] enemies = Physics2D.OverlapCircleAll(meleePoint.position, meleeRange, enemyLayer);

        foreach (Collider2D enemy in enemies)
        {
            if (enemy.TryGetComponent<EnemyHealth>(out var enemyHealth))
            {
                enemyHealth.TakeDamage(meleeDamage, transform);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (meleePoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(meleePoint.position, meleeRange);
        }
    }

    private void FireProjectile()
    {
        int projectileIndex = FindProjectile();
        if (projectileIndex >= 0)
        {
            GameObject projectile = projectiles[projectileIndex];
            projectile.transform.position = projectilePoint.position;

            ProjectileScript ps = projectile.GetComponent<ProjectileScript>();
            if (ps != null)
            {
                ps.SetDirection(Mathf.Sign(transform.localScale.x));
            }
        }
    }

    private int FindProjectile()
    {
        for (int i = 0; i < projectiles.Length; i++)
        {
            if (projectiles[i] != null && !projectiles[i].activeInHierarchy)
                return i;
        }
        return -1;
    }
}
using UnityEngine;

public class PlayerAttackScript : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform projectilePoint;
    [SerializeField] private GameObject[] projectiles;

    private Animator playerAnimation;
    private PlayerScript playerMovement;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        playerAnimation = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerScript>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMovement.canAttack())
            Attack();

        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        playerAnimation.SetTrigger("Attack");
        cooldownTimer = 0;

        projectiles[FindProjectile()].transform.position = projectilePoint.position;
        projectiles[FindProjectile()].GetComponent<ProjectileScript>().SetDirection(Mathf.Sign(transform.localScale.x));

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
}

using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    [SerializeField] private float speed;
    private float direction;
    private bool hitTarget;

    private Animator projectileAnimation;
    private BoxCollider2D projectileCollider;

    private void Awake()
    {
        projectileAnimation = GetComponent<Animator>();
        projectileCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (hitTarget) return;
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hitTarget = true;
        projectileCollider.enabled = false;
        projectileAnimation.SetTrigger("Explode");
    }
    
    public void SetDirection(float _direction)
    {
        direction = _direction;
        gameObject.SetActive(true);
        hitTarget = false;
        projectileCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction)
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}

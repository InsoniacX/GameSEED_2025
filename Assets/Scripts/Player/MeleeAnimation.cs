using System.Collections;
using UnityEngine;

public class MeleeAnimation : MonoBehaviour
{
    [SerializeField] private Transform weaponTransform;
    [SerializeField] private float attackAngle = 90f;
    [SerializeField] private float attackDuration = 0.2f;
    [SerializeField] private float returnSpeed = 5f;

    private Quaternion originalRotation;
    private bool isAttacking = false;

    void Start()
    {
        originalRotation = weaponTransform.rotation;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isAttacking)
        {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        isAttacking = true;

        // Animasi serangan (mengayun ke depan)
        float elapsed = 0f;
        while (elapsed < attackDuration)
        {
            weaponTransform.Rotate(0, 0, attackAngle * Time.deltaTime / attackDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Kembali ke posisi awal
        while (Quaternion.Angle(weaponTransform.rotation, originalRotation) > 0.1f)
        {
            weaponTransform.rotation = Quaternion.Lerp(weaponTransform.rotation, originalRotation, returnSpeed * Time.deltaTime);
            yield return null;
        }

        weaponTransform.rotation = originalRotation;
        isAttacking = false;
    }
}
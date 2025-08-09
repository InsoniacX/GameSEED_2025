using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    private Image image;

    private PlayerHealth playerHealth;

    private void Start()
    {
        image = GetComponent<Image>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        if (playerHealth != null)
        {
            // Update UI health berdasarkan currentHealth dari PlayerHealthScript
            float healthPercentage = (float)playerHealth.GetCurrentHealth() / playerHealth.GetMaxHealth();
            image.fillAmount = healthPercentage;
        }
    }
}

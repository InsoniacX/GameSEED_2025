using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    private Image image;

    public float health = 100;
    
    private void Start()
    {
        image = GetComponent<Image>();
    }

    private void Update()
    {
        // Ensure the health value is clamped between 0 and 100
        health = Mathf.Clamp(health, 0, 100);
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ChangeHealth(-10); // Example: Decrease health by 10 when space is pressed
        }
    }

    public void ChangeHealth(float changeAmount)
    {
        health += changeAmount;
        image.fillAmount = health / 100;
    }
}

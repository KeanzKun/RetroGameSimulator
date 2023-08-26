using UnityEngine;
using UnityEngine.UI;
using TMPro; // Add this for TextMeshPro

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public Image healthBarFill; // Drag the fill component of the health bar here
    public HealthSystem playerHealthSystem; // Reference to the HealthSystem
    public TextMeshProUGUI healthText; // Reference to the TextMeshPro text component

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateHealthBar(float healthPercentage)
    {
        healthBarFill.fillAmount = healthPercentage;
    }

    // called whenever the player's health changes
    public void UpdateHealthUI()
    {
        // Get the current and max health from the HealthSystem
        int currentHealth = Mathf.RoundToInt(playerHealthSystem.currentHealth);
        int maxHealth = Mathf.RoundToInt(playerHealthSystem.maxHealth);

        // Update the health text
        healthText.text = currentHealth + "/" + maxHealth;
    }
}

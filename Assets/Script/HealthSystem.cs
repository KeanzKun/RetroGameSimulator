using System.Collections;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        StartCoroutine(RegenerateHealth());
    }

    private IEnumerator RegenerateHealth()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f); // Wait for 1 second

            if (currentHealth < maxHealth)
            {
                currentHealth += 1; // Increase health by 1
                currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

                // Notify the UI to update the health bar and text
                UIManager.Instance.UpdateHealthUI();
                // Update the health bar
                UIManager.Instance.UpdateHealthBar(currentHealth / maxHealth);
            }
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Notify the UI to update the health bar
        UIManager.Instance.UpdateHealthBar(currentHealth / maxHealth);

        // Notify the UI to update the health bar and text
        UIManager.Instance.UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Handle death logic here
        Debug.Log("Character died!");
    }
}

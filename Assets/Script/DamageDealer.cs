using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public float damageAmount = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Assuming your player has the tag "Player"
        {
            HealthSystem playerHealth = other.GetComponent<HealthSystem>();
            Debug.Log("BEFORE DEDUCT: " + playerHealth);

            if (playerHealth)
            {
                playerHealth.TakeDamage(damageAmount);
                Debug.Log("AFTER DEDUCT: " + playerHealth);

            }
        }
    }
}

using System.Collections;
using UnityEngine;

public class DragonCastLeafBlade : MonoBehaviour
{
    [SerializeField] private Rigidbody nb;
    [SerializeField] private Transform leafBladeSpawnPoint;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float projectileSpeed = 10f;
    private float projectileLifetime = 5f;
    // Set the number of bullets you want to shoot
    [SerializeField] private int numberOfLeafBlades = 10;

    private bool isShooting = false;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.F) && !isShooting)
        {
            isShooting = true;
            StartCoroutine(ShootLeafBlades());
        }
    }

    IEnumerator ShootLeafBlades()
    {
        for (int i = 0; i < numberOfLeafBlades; i++)
        {
            // Update the direction to the player's current location before shooting
            Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;

            // Spawn the leafBlade and shoot towards the player
            LeafBladeObjectPooling.SpawnLeafBlade(leafBladeSpawnPoint.transform.position, leafBladeSpawnPoint.transform.rotation, directionToPlayer);

            yield return new WaitForSeconds(0.5f); // Wait for 1 second before shooting the next leaf blade
        }

        isShooting = false;
    }
}

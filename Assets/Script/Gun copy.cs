using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public float shootingInterval = 0.3f; // Time between shots

    private bool isShooting = false;

    void Update()
    {
        // Check if the Fire1 (left mouse button) is pressed down
        if (Input.GetButtonDown("Fire1"))
        {
            // Start the shooting coroutine when the button is first pressed
            if (!isShooting)
            {
                StartCoroutine(ShootBullet());
            }
        }
        // Stop shooting when the Fire1 button is released
        else if (Input.GetButtonUp("Fire1"))
        {
            StopShooting();
        }
    }

    // Coroutine to shoot bullets at a fixed interval
    IEnumerator ShootBullet()
    {
        isShooting = true;

        while (Input.GetButton("Fire1"))
        {
            // Spawn the bullet
            var bulletCloned = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

            // Give the bullet initial velocity
            bulletCloned.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;

            // Wait for the specified interval before shooting the next bullet
            yield return new WaitForSeconds(shootingInterval);
        }

        isShooting = false;
    }

    // Function to stop shooting
    void StopShooting()
    {
        if (isShooting)
        {
            StopCoroutine(ShootBullet());
            isShooting = false;
        }
    }
}

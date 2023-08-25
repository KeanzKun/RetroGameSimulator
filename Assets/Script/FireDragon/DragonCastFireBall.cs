using System.Collections;
using UnityEngine;

public class DragonCastFireBall : MonoBehaviour
{
    public Rigidbody nb;
    public Transform fireBallSpawnPoint;
    public float newton = 200.0f;

    // Set the number of bullets you want to shoot
    public int numberOfFireBalls = 10;

    void Update()
    {
        // Check if the Fire1 (left mouse button) is pressed down
        if (Input.GetKeyUp(KeyCode.F))
        {
            // Calculate the rotation offset for each bullet
            float rotationOffset = 360f / numberOfFireBalls;

            // Loop to spawn each bullet with the rotation offset
            for (int i = 0; i < numberOfFireBalls; i++)
            {
                // Calculate the rotation offset for this bullet
                float currentRotationOffset = i * rotationOffset;

                // Spawn the bullet with the rotation offset
                FireballObjectPooling.SpawnFireBall(fireBallSpawnPoint.transform.position, fireBallSpawnPoint.transform.rotation, currentRotationOffset);
            }
        }
    }
}
    
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Bullet : MonoBehaviour
{
    public float life = 3;
 
    // void Awake()
    // {
    //     if (!gameObject.CompareTag("Bullet"))
    //     {
    //     Destroy(gameObject, life);
    //     }
    // }
    // void Update()
    // {

    // }
    void OnCollisionEnter(Collision collision)
    {
        // Check if the colliding object is the cloned bullet
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // Destroy the cloned bullet when it collides with any object
            Destroy(gameObject);
        }
    }
}

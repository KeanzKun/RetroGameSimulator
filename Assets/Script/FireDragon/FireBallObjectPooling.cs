using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballObjectPooling : MonoBehaviour
{
    public float bulletSpeed = 5.0f;
    public float bulletDelay = 0.0f;
    //public BulletTutorial bullet;
    public const float BULLET_DELAY_MAX = 3.0f;
    static private List<FireballObjectPooling> fireBalls;

    // Start is called before the first frame update
    void Awake()
    {
        if (fireBalls == null)
        {
            fireBalls = new List<FireballObjectPooling>();
        }

        fireBalls.Add(this);
        
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        //set all to inactive
        gameObject.SetActive(false);
    }

    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
    }

    void OnTriggerExit(Collider col)
    {
        gameObject.SetActive(false);
        Debug.Log("BLAST REMOVED");
    }

    // private void OnCollisionEnter(Collision collision)
    // {
    //     if(collision.collider.tag == "WallTag")
    //     {
    //         BulletTutorial bullet = Instantiate(nb, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
    //     }
    // }

    static public FireballObjectPooling SpawnFireBall(Vector3 location, Quaternion landing, float rotationOffset)
    {
        foreach (FireballObjectPooling fireBall in fireBalls)
        {
            if (fireBall.gameObject.activeSelf == false)
            {
                fireBall.transform.position = location;

                // Apply the rotation offset to the original rotation
                fireBall.transform.rotation = landing * Quaternion.Euler(0f, rotationOffset, 0f);

                fireBall.gameObject.SetActive(true);
                fireBall.gameObject.GetComponent<Rigidbody>().isKinematic = false;

                return fireBall;
            }
        }

        return null;
    }
}

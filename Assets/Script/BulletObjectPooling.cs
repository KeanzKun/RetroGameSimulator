using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObjectPooling : MonoBehaviour
{
    public float bulletSpeed = 5.0f;
    public float bulletDelay = 0.0f;
    //public BulletTutorial bullet;
    public const float BULLET_DELAY_MAX = 3.0f;
    static private List<BulletObjectPooling> bullets;

    // Start is called before the first frame update
    void Awake()
    {

        if (bullets == null)
        {
            bullets = new List<BulletObjectPooling>();
        }

        bullets.Add(this);

        gameObject.GetComponent<Rigidbody>().isKinematic = false;

        //set all to inactive
        gameObject.SetActive(false);

        DontDestroyOnLoad(this.gameObject);
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

    static public BulletObjectPooling SpawnBullet(Vector3 location, Quaternion landing)
    {
        foreach (BulletObjectPooling bullet in bullets)
        {
            if (bullet.gameObject.activeSelf == false)
            {
                bullet.transform.position = location;
                bullet.transform.rotation = landing;

                bullet.gameObject.SetActive(true);
                bullet.gameObject.GetComponent<Rigidbody>().isKinematic = false;

                return bullet;
            }
        }

        return null;
    }
}

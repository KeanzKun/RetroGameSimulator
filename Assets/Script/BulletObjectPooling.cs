using System.Collections.Generic;
using UnityEngine;

public class BulletObjectPooling : MonoBehaviour
{
    public enum BulletType { Fire, Grass, Normal }
 
    [SerializeField] private GameObject fireBulletPrefab;
    [SerializeField] private GameObject grassBulletPrefab;
    public GameObject normalBulletPrefab;
    public float bulletSpeed = 5.0f;

    private static List<BulletObjectPooling> fireBullets;
    private static List<BulletObjectPooling> grassBullets;
    private static List<BulletObjectPooling> normalBullets;

    void Awake()
    {
        if (fireBullets == null)
        {
            fireBullets = new List<BulletObjectPooling>();
        }

        if (grassBullets == null)
        {
            grassBullets = new List<BulletObjectPooling>();
        }

        if (normalBullets == null)
        {
            normalBullets = new List<BulletObjectPooling>();
        }

        if (this.gameObject.CompareTag("Fire_Bullet"))
        {
            fireBullets.Add(this);
        }
        else if (this.gameObject.CompareTag("Grass_Bullet"))
        {
            grassBullets.Add(this);
        }
        else if (this.gameObject.CompareTag("Normal_Bullet"))
        {
            normalBullets.Add(this);
        }

        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        gameObject.SetActive(false);
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
    }

    void OnTriggerExit(Collider col)
    {
        gameObject.SetActive(false);
        Debug.Log("BLAST REMOVED");
    }

    static public BulletObjectPooling SpawnBullet(BulletType type, Vector3 location, Quaternion rotation)
    {
        List<BulletObjectPooling> bulletList;
        GameObject bulletPrefab;

        switch (type)
        {
            case BulletType.Fire:
                bulletList = fireBullets;
                bulletPrefab = Instance.fireBulletPrefab;
                break;
            case BulletType.Grass:
                bulletList = grassBullets;
                bulletPrefab = Instance.grassBulletPrefab;
                break;
            case BulletType.Normal:
            default:
                bulletList = normalBullets;
                bulletPrefab = Instance.normalBulletPrefab;
                break;
        }

        foreach (BulletObjectPooling bullet in bulletList)
        {
            if (!bullet.gameObject.activeSelf)
            {
                bullet.transform.position = location;
                bullet.transform.rotation = rotation;
                bullet.gameObject.SetActive(true);
                bullet.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                return bullet;
            }
        }

        // If no bullets are available in the pool, instantiate a new one
        BulletObjectPooling newBullet = Instantiate(bulletPrefab, location, rotation).GetComponent<BulletObjectPooling>();
        bulletList.Add(newBullet);
        return newBullet;
    }

    private static BulletObjectPooling Instance
    {
        get
        {
            return FindObjectOfType<BulletObjectPooling>();
        }
    }
}

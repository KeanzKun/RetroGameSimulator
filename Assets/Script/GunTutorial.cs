using System.Collections;
using UnityEngine;

public class GunTutorial : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public float newton = 200.0f;

    private bool isReloading = false;
    private const int MAX_AMMO = 10; // Each gun can fire 10 bullets
    [SerializeField] private int ammo = MAX_AMMO;
    [SerializeField] private float reloadTime = 1f;

    void Update()
    {
        // Check if the Fire1 (left mouse button) is pressed down
        if (Input.GetKeyDown(KeyCode.Mouse0) && ammo > 0 && !isReloading)
        {
            Debug.Log("SHOTS FIRED");
            ammo -= 1;
            BulletObjectPooling.SpawnBullet(bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);

            if (ammo <= 0)
            {
                DestroyGun(); // Destroy the gun when out of ammo
            }
        }
        else if (Input.GetKeyUp(KeyCode.R))
        {
            if (!isReloading)
            {
                isReloading = true;
                StartCoroutine(ReloadGun());
            }
        }
    }

    public bool IsReloading()
    {
        return isReloading;
    }

    public int BulletCount()
    {
        return ammo;
    }

    IEnumerator ReloadGun()
    {
        Debug.Log("RELOADING");
        yield return new WaitForSeconds(reloadTime);
        ammo = MAX_AMMO;
        isReloading = false;
    }

    void DestroyGun()
    {
        GunInventory inventory = FindObjectOfType<GunInventory>();
        if (inventory)
        {
            inventory.RemoveGun(gameObject);
        }
    }
}

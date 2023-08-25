using System.Collections;
using UnityEngine;

public class GunTutorial : MonoBehaviour
{
    public Rigidbody nb;
    public Transform bulletSpawnPoint;
    public float newton = 200.0f;

    private bool isReloading = false;
    [SerializeField] private int ammo = 5;
    [SerializeField] private float reloadTime = 1f;


    void Update()
    {
        // Check if the Fire1 (left mouse button) is pressed down

        if (Input.GetKeyDown(KeyCode.Mouse0) && ammo > 0 && !isReloading)
        {
            // Rigidbody cloned;
            // cloned = Instantiate(nb, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            // cloned.AddForce(Vector3.forward * newton);
            // Debug.Log("Fired");
            Debug.Log("SHOTS FIRED");
            ammo -= 1;
            BulletObjectPooling.SpawnBullet(bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);

        }
        else if (Input.GetKeyUp(KeyCode.R) || ammo == 0)
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
        //yield on a new YieldInstruction that waits for 5 seconds.
        Debug.Log("RELOADING");
        yield return new WaitForSeconds(reloadTime);
        ammo = 5;
        isReloading = false;
    }

}

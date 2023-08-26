using System.Collections;
using UnityEngine;

public class BulletBox : MonoBehaviour
{
    public GameObject[] gunTypes;
    private float respawnTime = 20f;
    private bool isDropping = false; // Add this flag

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("COLLIDED BOX");
        if ((collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Bullet")) && !isDropping)
        {
            isDropping = true; // Set the flag to true
            DropGun();
            StartCoroutine(RespawnBox());
        }
    }

    void DropGun()
    {
        GameObject gunToDrop = gunTypes[Random.Range(0, gunTypes.Length)];
        Instantiate(gunToDrop, transform.position + Vector3.up, Quaternion.identity);
    }

    IEnumerator RespawnBox()
    {
        gameObject.SetActive(false);
        yield return new WaitForSeconds(respawnTime);
        isDropping = false; // Reset the flag
        gameObject.SetActive(true);
    }
}

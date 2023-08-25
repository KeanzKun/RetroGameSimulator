using UnityEngine;

public class GunPickup : MonoBehaviour
{
    public GameObject gunPrefab; // The actual gun prefab that will be added to the inventory.

private void OnTriggerEnter(Collider other)
{
    GunInventory inventory = other.GetComponent<GunInventory>();
    if (inventory != null && inventory.guns.Count < inventory.maxSlots)
    {
        inventory.AddGun(gunPrefab);
        Destroy(gameObject); // Destroy the pickup object.
    }
}

}

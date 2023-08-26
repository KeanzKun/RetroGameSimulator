// using UnityEngine;

// public class ItemPickup : MonoBehaviour
// {
//     public string bulletType; // "Grass" or "Fire"
//     public int bulletAmount = 1;

//     private void OnTriggerEnter(Collider other)
//     {
//         Debug.Log("TRIGGERED ITEM");
//         GunInventory inventory = other.GetComponent<GunInventory>();
        
//         if (inventory != null)
//         {
//             Debug.Log("IN IF STATEMENT ITEMPICKUP");
//             inventory.AddBullet(bulletType, bulletAmount);
//             Destroy(gameObject);
//         }
//     }
// }

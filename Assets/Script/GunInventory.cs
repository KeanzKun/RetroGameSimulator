using UnityEngine;
using System.Collections.Generic;

public class GunInventory : MonoBehaviour
{
    public int maxSlots = 3;
    public List<GameObject> guns = new List<GameObject>();
    public GameObject activeGun;
    public Transform gunContainer;
    public GameObject gunDropPrefab; // Assign the gun drop prefab in the inspector.

    public void AddGun(GameObject gunPrefab)
    {
        if (guns.Count < maxSlots)
        {
            GameObject pickedGun = Instantiate(gunPrefab, gunContainer.transform);
            pickedGun.SetActive(false); // Initially set to inactive.
            guns.Add(pickedGun);
            if (guns.Count == 1)
            {
                SetActiveGun(pickedGun);
            }
        }
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchGun(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchGun(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchGun(2);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            DropGun();
        }
    }


    public void SetActiveGun(GameObject gun)
    {
        if (activeGun != null)
        {
            activeGun.SetActive(false);
        }
        activeGun = gun;
        activeGun.SetActive(true);
    }

    public void SwitchGun(int index)
    {
        if (index < 0 || index >= maxSlots)
        {
            return; // Invalid index.
        }

        if (index < guns.Count)
        {
            SetActiveGun(guns[index]);
        }
        else
        {
            // If there's no gun in the selected slot, deactivate the current gun.
            if (activeGun != null)
            {
                activeGun.SetActive(false);
                activeGun = null;
            }
        }
    }

    public void DropGun()
    {
        Debug.Log("ENTERED DROP");
        if (activeGun != null)
        {
            Debug.Log("ENTERED FIRST IF");

            GunProperties gunProperties = activeGun.GetComponent<GunProperties>();
            if (gunProperties)
            {
                if (gunProperties.gunDropPrefab)
                {
                    Debug.Log("ENTERED SECOND IF");

                    // Instantiate the corresponding drop prefab at the player's position.
                    Vector3 dropPosition = transform.position + transform.forward * 1.5f + transform.up * 0.5f;
                    GameObject droppedGun = Instantiate(gunProperties.gunDropPrefab, dropPosition, Quaternion.identity);

                    Debug.Log("DROPPED GUN: " + droppedGun);
                    // If you want the gun to have some forward momentum when dropped, you can add a Rigidbody to the gunDropPrefab and apply force here.
                    Rigidbody rb = droppedGun.GetComponent<Rigidbody>();
                    Debug.Log("DROPPED GUN RB: " + rb);
                    if (rb != null)
                    {
                        Debug.Log("ENTERED THIRD IF");

                        rb.AddForce(transform.forward * 5f, ForceMode.Impulse); // Adjust the force value as needed.
                    }
                    Debug.Log("EXITED THIRD IF");
                }
                else
                {
                    Debug.Log("gunDropPrefab is not assigned in GunProperties.");
                }
            }
            else
            {
                Debug.Log("GunProperties component is missing from the activeGun.");
            }
            Debug.Log("EXITED SECOND IF");

            // Remove the active gun from the inventory list and destroy its GameObject.
            guns.Remove(activeGun);
            Destroy(activeGun);

            // If there are other guns in the inventory, set the next one as active. Otherwise, set activeGun to null.
            if (guns.Count > 0)
            {
                Debug.Log("ENTERED gun count IF");

                SetActiveGun(guns[0]);
            }
            else
            {
                Debug.Log("ENTERED gun count ELSE");

                activeGun = null;
            }

            Debug.Log("Exiting FIRST IF");
        }
        Debug.Log("EXITED FIRST IF");

    }


}

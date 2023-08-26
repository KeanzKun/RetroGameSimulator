using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI; // Add this at the top

public class GunInventory : MonoBehaviour
{
    public Image[] gunSlots; // Drag the three Image components here
    public Text[] gunNames;  // Drag the three Text components here if you added them
    public Image[] gunSlotHighlights; // Drag the highlight Image components here
    public int maxSlots = 3;
    public List<GameObject> guns = new List<GameObject>();
    public GameObject activeGun;
    public Transform gunContainer;
    public GameObject gunDropPrefab; // Assign the gun drop prefab in the inspector.


    void Start()
    {
        // Hide all gun slots and highlights at the start
        foreach (Image slot in gunSlots)
        {
            slot.gameObject.SetActive(false);
        }
        foreach (Image highlight in gunSlotHighlights)
        {
            highlight.gameObject.SetActive(false);
        }
    }

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

        Debug.Log("Adding gun: " + gunPrefab.name);
        UpdateUI();
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

        UpdateUI();
    }

    public void DropGun()
    {
        if (activeGun != null)
        {

            GunProperties gunProperties = activeGun.GetComponent<GunProperties>();
            if (gunProperties)
            {
                if (gunProperties.gunDropPrefab)
                {

                    // Instantiate the corresponding drop prefab at the player's position.
                    Vector3 dropPosition = transform.position + transform.forward * 1.5f + transform.up * 0.5f;
                    GameObject droppedGun = Instantiate(gunProperties.gunDropPrefab, dropPosition, Quaternion.identity);

                    // If you want the gun to have some forward momentum when dropped, you can add a Rigidbody to the gunDropPrefab and apply force here.
                    Rigidbody rb = droppedGun.GetComponent<Rigidbody>();
                    if (rb != null)
                    {

                        rb.AddForce(transform.forward * 5f, ForceMode.Impulse); // Adjust the force value as needed.
                    }
                }
                else
                {
                    Debug.Log("GunProperties is missing from the activeGun.");
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

                SetActiveGun(guns[0]);
            }
            else
            {

                activeGun = null;
            }


        }


        UpdateUI();


    }

    //destroy if ran out of bullet
    public void RemoveGun(GameObject gunToRemove)
    {
        if (guns.Contains(gunToRemove))
        {
            if (gunToRemove == activeGun)
            {
                activeGun = null;
            }
            guns.Remove(gunToRemove);
            Destroy(gunToRemove);
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        for (int i = 0; i < gunSlots.Length; i++)
        {
            if (i < guns.Count && guns[i] != null)
            {
                GunProperties properties = guns[i].GetComponent<GunProperties>();

                gunSlots[i].gameObject.SetActive(true); // Show the gun slot
                gunSlots[i].sprite = properties ? properties.gunImage : null; // Set the gun image
                gunSlots[i].color = Color.white; // Or any color to indicate the slot is filled

                if (gunNames.Length > i)
                {
                    gunNames[i].text = properties ? properties.gunName : "Unknown";
                }
            }
            else
            {
                gunSlots[i].gameObject.SetActive(false); // Hide the gun slot
                if (gunNames.Length > i)
                {
                    gunNames[i].text = "";
                }
            }
        }

        // Update highlights
        for (int i = 0; i < gunSlotHighlights.Length; i++)
        {
            if (i < guns.Count)
            {
                if (guns[i] == activeGun)
                {
                    gunSlotHighlights[i].gameObject.SetActive(true); // Highlight the active gun
                }
                else
                {
                    gunSlotHighlights[i].gameObject.SetActive(false); // De-highlight other guns
                }
            }
            else
            {
                gunSlotHighlights[i].gameObject.SetActive(false); // Hide the highlight if there's no gun
            }
        }
    }


}

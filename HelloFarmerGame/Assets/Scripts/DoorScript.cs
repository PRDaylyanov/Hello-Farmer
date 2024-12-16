using System;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public Vector3 openPosition; // The position to move the door to when it's opened
    private string requiredItem = "Key"; // The item name required to open the door

    private bool isDoorOpen = false; // Track the door's state

    void Start()
    {
        // Ensure the door starts at its initial position
        if (openPosition == Vector3.zero)
        {
            openPosition = transform.position; // Default open position if not set
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the player is interacting with the door
        if (other.CompareTag("Player") && !isDoorOpen)
        {
            if (HasRequiredItem())
            {
                OpenDoor();
            }
            else
            {
                Debug.Log("You need a key to open this door!");
            }
        }
    }

    private bool HasRequiredItem()
    {
        if (InventorySystem.Instance != null)
        {
            Debug.Log($"Checking for required item: {requiredItem}");
            Debug.Log($"Current inventory: {string.Join(", ", InventorySystem.Instance.itemNames)}");

            foreach (var item in InventorySystem.Instance.itemNames)
            {
                if (string.Equals(item, requiredItem, StringComparison.OrdinalIgnoreCase))
                {
                    Debug.Log($"Found required item: {item}");
                    return true;
                }
            }
        }
        else
        {
            Debug.LogError("InventorySystem Instance is not found!");
        }
        Debug.Log("Required item not found.");
        return false;
    }


    private void OpenDoor()
    {
        isDoorOpen = true;
        // Move the door to the open position
        transform.position = openPosition;
        Debug.Log("Door is now open!");
    }
}
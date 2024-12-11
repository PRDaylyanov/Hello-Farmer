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
        // Check the inventory for the required item
        if (InventorySystem.Instance != null)
        {
            return InventorySystem.Instance.itemNames.Contains(requiredItem);
        }
        else
        {
            Debug.LogError("InventorySystem Instance is not found!");
            return false;
        }
    }

    private void OpenDoor()
    {
        isDoorOpen = true;
        // Move the door to the open position
        transform.position = openPosition;
        Debug.Log("Door is now open!");
    }
}
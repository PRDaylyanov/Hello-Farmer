using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public bool playerInRange;

    public string ItemName;

    public string GetItemName()
    {
        return ItemName;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInRange && SelectionManager.Instance.onTarget)
        {
            if (!InventorySystem.Instance.CheckIfFull())
            {
                Debug.Log($"Adding {ItemName} to inventory.");
                InventorySystem.Instance.AddToInventory(ItemName);

                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Inventory is full!");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
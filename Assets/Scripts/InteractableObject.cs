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
        // Check to see if the mouse is clicked and on the item directly
        if (Input.GetKeyDown(KeyCode.Mouse0) && playerInRange && SelectionManager.Instance.onTarget)
        {
            if (!InventorySystem.Instance.CheckIfFull())
            {
                InventorySystem.Instance.AddToInventory(ItemName);

                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Full");
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
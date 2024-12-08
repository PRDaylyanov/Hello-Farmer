using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem Instance { get; set; }

    public GameObject inventoryScreenUI;

    public List<GameObject> slotedItems = new List<GameObject>();
    public List<string> itemNames = new List<string>();

    private GameObject itemToAdd;
    private GameObject addedSlot;

    public bool isOpen;
    public bool isFull;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        isOpen = false;
        isFull = false;
        FillSlotList();
    }

    private void FillSlotList()
    {
        foreach (Transform child in inventoryScreenUI.transform)
        {
            if (child.CompareTag("Slot"))
            {
                slotedItems.Add(child.gameObject);
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && !isOpen)
        {

            Debug.Log("i is pressed");
            inventoryScreenUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            isOpen = true;

        }
        else if (Input.GetKeyDown(KeyCode.I) && isOpen)
        {
            inventoryScreenUI.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            isOpen = false;
        }
    }

    public void AddToInventory(string itemName)
    {
        addedSlot = FindEmptySlot();

        itemToAdd = (GameObject)Instantiate(Resources.Load<GameObject>(itemName), addedSlot.transform.position, addedSlot.transform.rotation);
        itemToAdd.transform.SetParent(addedSlot.transform);

        slotedItems.Add(itemToAdd);

    }

    private GameObject FindEmptySlot()
    {
        foreach (GameObject slot in slotedItems)
        {
            if (slot.transform.childCount == 0)
            {
                return slot;
            }
        }

        return new GameObject();
    }

    public bool CheckIfFull()
    {
        int counter = 0;

        foreach (GameObject slot in slotedItems)
        {
            if (slot.transform.childCount > 0)
            {
                counter += 1;
            }


        }

        if (counter == 5)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
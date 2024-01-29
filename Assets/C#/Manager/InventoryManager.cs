using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    [SerializeField] InventoryItemSO[] inventoryItems;


    Dictionary<string, InventoryItem> currentItems;

    public Dictionary<string, InventoryItem>.ValueCollection CurrentInventoryItems { get { return currentItems.Values; } }


    public static InventoryManager inventoryManager;

    private void Awake()
    {
        inventoryManager = this;

        GameEvents.OnSpwnInventoryItem += OnSpwnInventoryItem;
        GameEvents.OnAddInventoryItem += OnAddInventoryItem;

        Setup();
    }

    private void OnDestroy()
    {
        inventoryManager = null;

        GameEvents.OnSpwnInventoryItem -= OnSpwnInventoryItem;
        GameEvents.OnAddInventoryItem -= OnAddInventoryItem;
    }

    private void OnSpwnInventoryItem(InventoryItem inventoryItem)
    {
        var x = UnityEngine.Random.Range(-1f, 1.1f);
        var z = UnityEngine.Random.Range(2, 3.1f);
        var pos = new Vector3(x, 2, z);

        //In an actual game we will check the object in the pool before creatinginstantiating a new one
        var itemObject = Instantiate(inventoryItem.InventoryItemSO.itemPrefabObject, pos, Quaternion.identity).GetComponent<InventoryItemObject>();
        itemObject.Setup(inventoryItem);
        inventoryItem.ItemRemovedFromInventory();
    }

    private void OnAddInventoryItem(InventoryItem inventoryItem)
    {
        inventoryItem.ItemAddedBackInInventory();
    }


    private void Setup()
    {
        currentItems = new Dictionary<string, InventoryItem>();
        for (int i = 0; i < inventoryItems.Length; i++)
        {
            var item = new InventoryItem(Guid.NewGuid().ToString(), inventoryItems[i]);
            currentItems[item.ItemID] = item;
        }
    }

    private void AddNewItem(InventoryItemSO inventoryItemSO)
    {
        //this can be used when a new item needs to be added runtime
        var newItem = new InventoryItem(Guid.NewGuid().ToString(), inventoryItemSO);
        currentItems[newItem.ItemID] = newItem;

        //Then call the refresh logic for updating the UI Item List
    }
}

public class InventoryItem
{
    readonly string itemID;
    readonly InventoryItemSO inventoryItemSO;

    int currentCount;

    public string ItemID { get { return itemID; } }
    public InventoryItemSO InventoryItemSO { get { return inventoryItemSO; } }
    public int CurrentCount { get { return currentCount; } }
    public bool IsFull { get { return currentCount == inventoryItemSO.maxCapacity; } }
    public InventoryItemUIElement InventoryItemUIElement { get; set; }

    public InventoryItem(string itemID, InventoryItemSO inventoryItemSO)
    {
        this.itemID = itemID;
        this.inventoryItemSO = inventoryItemSO;
        currentCount = inventoryItemSO.maxCapacity;
    }

    public void ItemRemovedFromInventory()
    {
        currentCount--;
        InventoryItemUIElement.RefreshCoutUI();
    }
    public void ItemAddedBackInInventory()
    {
        currentCount++;
        InventoryItemUIElement.RefreshCoutUI();
    }
}
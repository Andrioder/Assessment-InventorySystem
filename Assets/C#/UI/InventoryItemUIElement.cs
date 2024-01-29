using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryItemUIElement : MonoBehaviour
{
    [SerializeField] Image itemIcon;
    [SerializeField] TextMeshProUGUI itemNameText, itemCapacityText;

    InventoryItem inventoryItem;

    public void Setup(InventoryItem inventoryItem)
    {
        this.inventoryItem = inventoryItem;
        this.inventoryItem.InventoryItemUIElement = this;


        itemIcon.sprite = inventoryItem.InventoryItemSO.iconUISprite;
        itemNameText.text = inventoryItem.InventoryItemSO.itemName;
        RefreshCoutUI();

    }

    public void RefreshCoutUI()
    {
        itemCapacityText.text = $"{inventoryItem.CurrentCount}/{inventoryItem.InventoryItemSO.maxCapacity}";
    }


    public void OnItemClick()
    {

        if (inventoryItem.CurrentCount <= 0)
            return;

        GameEvents.OnSpwnInventoryItem(inventoryItem);
    }
}

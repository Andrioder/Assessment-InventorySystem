using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] RectTransform invetoryPanel;
    [SerializeField] GameObject iventoryItemElementPrefabObject;
    [SerializeField] Transform inventoryItemHolder;


    private void Start()
    {
        GameEvents.OnToggleInventory += OnToggleInventory;

        Init();
    }

    private void OnDestroy()
    {
        GameEvents.OnToggleInventory -= OnToggleInventory;
    }

    private void OnToggleInventory(bool v)
    {
        invetoryPanel.DOAnchorPosX(v ? 0 : -1500, .25f).SetEase(Ease.Linear);
    }



    private void Init()
    {
        var currentItems = InventoryManager.inventoryManager.CurrentInventoryItems;
        foreach (var item in currentItems)
        {
            SpwnItemElement(item);
        }
    }



    private void SpwnItemElement(InventoryItem inventoryItem)
    {
        var ele = Instantiate(iventoryItemElementPrefabObject).GetComponent<InventoryItemUIElement>();
        ele.Setup(inventoryItem);
        ele.transform.SetParent(inventoryItemHolder, false);
    }

}

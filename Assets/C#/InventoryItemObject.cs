using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class InventoryItemObject : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    Transform myTransform;
    Outline outline;

    InventoryItem inventoryItem;

    private void Awake()
    {
        myTransform = transform;
        outline = GetComponent<Outline>();
    }

    public void Setup(InventoryItem inventoryItem)
    {
        this.inventoryItem = inventoryItem;

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (inventoryItem.IsFull)
            return;

        myTransform.DOScale(Vector3.zero, .5f).SetEase(Ease.OutBounce).OnComplete(() =>
        {
            //In an actual game we will return the object back to its pool -- ObjectPooling
            Destroy(this.gameObject);
        });
        GameEvents.OnAddInventoryItem?.Invoke(inventoryItem);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        outline.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        outline.enabled = false;
    }
}

using UnityEngine;

[CreateAssetMenu(fileName = "InvetoryItem", menuName = "Inventory/Item")]
public class InventoryItemSO : ScriptableObject
{
    public string itemName;
    public int maxCapacity;
    public Sprite iconUISprite;
    public GameObject itemPrefabObject;

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvents
{
    public static System.Action<bool> OnToggleInventory;
    public static System.Action<InventoryItem> OnSpwnInventoryItem;
    public static System.Action<InventoryItem> OnAddInventoryItem;
}

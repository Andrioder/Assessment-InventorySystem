using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{

    bool isInventoryOpen;

    public void OnTabKeyAction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isInventoryOpen = !isInventoryOpen;
            GameEvents.OnToggleInventory?.Invoke(isInventoryOpen);
        }
    }
}

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    
    public UnityEvent OnInteract;
    private bool submitPressed = false;
    

    

    public void Ineract(InputAction.CallbackContext context)
    {
        InventoryManager.Instance.TryCollectItems();

        OnInteract?.Invoke();

          
    }

    

    

    public void Inventory(InputAction.CallbackContext context)
    {
        FindFirstObjectByType<UIInventoryManager>().ToogleInventory();
    }

    public void SubmitPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            submitPressed = true;
        }
        else if (context.canceled)
        {
            submitPressed = false;
        }
    }

    

}

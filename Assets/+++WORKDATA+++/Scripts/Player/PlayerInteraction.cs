using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    public UnityEvent OnInteract;
    public void Ineract(InputAction.CallbackContext context)
    {
        InventoryManager.Instance.TryCollectItems();

        OnInteract?.Invoke();
    }

    public void Inventory(InputAction.CallbackContext context)
    {
        FindFirstObjectByType<UIInventoryManager>().ToogleInventory();
    }
}

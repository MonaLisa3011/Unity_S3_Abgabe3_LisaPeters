using UnityEngine;

public class UIInventoryManager : MonoBehaviour
{
    [SerializeField] private UIElementItem prefabElementItem;
    [SerializeField] private Transform parentItem;

    [SerializeField] private GameObject panelInventoryUI;

    private bool isInventoryOpen = false;

    private void Start()
    {
        panelInventoryUI.SetActive(false);
    }

    public void ToogleInventory()
    {
        if (isInventoryOpen)
        {
            HideInventory();
            isInventoryOpen = false;
        }
        else
        {
            ShowInventory();
            isInventoryOpen = true;
        }
    }

    public void HideInventory()
    {
        panelInventoryUI.SetActive(false);
    }

    public void ShowInventory()
    {
        panelInventoryUI.SetActive(true);

        foreach (Transform oldItemElementUI in parentItem)
        {
            Destroy(oldItemElementUI.gameObject);
        }
        foreach (SO_Item item in InventoryManager.Instance.ItemsInInventory)
        {
            UIElementItem newItemElementUI = Instantiate(prefabElementItem, parentItem);
            newItemElementUI.SetContent(item.itemName, item.itemSprite);
        }

    }
}

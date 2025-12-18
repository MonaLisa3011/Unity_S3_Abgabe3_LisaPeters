using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public List<SO_Item> ItemsInInventory = new List<SO_Item>();

  
    public List<CollectableItem> collectableItems = new List<CollectableItem>();

    [SerializeField] private SO_Item[] allPossibleItems;
    private Dictionary<string, SO_Item> allItems = new Dictionary<string, SO_Item>();

    List<string> collectedItemIDs = new List<string>();

    public static InventoryManager Instance { private set; get; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        foreach (SO_Item newDicEntry in allPossibleItems)
        {
            allItems.Add(newDicEntry.ItemID, newDicEntry);
        }
        foreach (string itemId in SaveManager.Instance.SaveState.inventoryItems)
        {
            ItemsInInventory.Add(allItems[itemId]);
        }

        foreach (string collectedItemID in SaveManager.Instance.SaveState.collectedItemIDs)
        {
            collectedItemIDs.Add(collectedItemID);
        }

        foreach (CollectableItem collectableItemInScene in FindObjectsByType<CollectableItem>(FindObjectsInactive.Include, FindObjectsSortMode.None))
        {
            if (collectedItemIDs.Contains(collectableItemInScene.GUID))
            {
                Destroy(collectableItemInScene.gameObject);
            }
        }


    }


    public void TryCollectItems()
    {
        for (int i = 0; i < collectableItems.Count; i++)
        {
            collectedItemIDs.Add(collectableItems[i].GUID);

            //AddItem(item.itemData); 
            //Destroy(item.gameObject);

            AddItem(collectableItems[i].itemData);
            Destroy(collectableItems[i].gameObject);
        }
    }


    public void AddItem(SO_Item newItem)
    {
        ItemsInInventory.Add(newItem);
        if (newItem.moreHealth > 0)
        {
            GameObject.FindGameObjectWithTag("VogelMann").GetComponent<HitPoints>().currentHealth += newItem.moreHealth;
           

        }
        QuestManager.Instance.OnItemCollected();
        SaveInventory();

    }

    public void RemoveItem(SO_Item itemToRemove)
    {
        if (ItemsInInventory.Contains(itemToRemove))
        {
            ItemsInInventory.Remove(itemToRemove);

            SaveInventory();
        }
        else
        {
            Debug.LogWarning(itemToRemove.itemName + "No such item in Inventory");
        }
    }



    public float GetBonusSpeed()
    {
        float bonusSpeed = 0;
        for (int i = 0; i < ItemsInInventory.Count; i++)
        {
            bonusSpeed += ItemsInInventory[i].bonusSpeed;
        }
        return bonusSpeed;
    }

    public int ItemCountInInventory(SO_Item itemData)
    {
        int counter = 0;
        foreach (SO_Item item in ItemsInInventory)
        {
            if (item == itemData)
            {
                counter++;
            }
        }
        return counter;
    }

    void SaveInventory()
    {
        SaveManager.Instance.SaveState.inventoryItems = new string[ItemsInInventory.Count];

        int index = 0;
        foreach (SO_Item item in ItemsInInventory)
        {
            SaveManager.Instance.SaveState.inventoryItems[index] = item.ItemID;

            index++;
        }
        SaveManager.Instance.SaveState.collectedItemIDs = collectedItemIDs.ToArray();

        SaveManager.Instance.SaveGame();
    }
}

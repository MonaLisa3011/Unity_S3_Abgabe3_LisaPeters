using System;
using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    public SO_Item itemData;

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private SpriteRenderer highlightRenderer;
    

    public string GUID;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        spriteRenderer.sprite = itemData.itemSprite;
        highlightRenderer.sprite = itemData.highlightSprite;
        highlightRenderer.gameObject.SetActive(false);

    }

    [ContextMenu("GenerateGUID")]
    void CreateGUID()
    {
        GUID = Guid.NewGuid().ToString();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Player is near" + gameObject.name);
        if (other.CompareTag("VogelMann"))
        {
            InventoryManager.Instance.collectableItems.Add(this);
            highlightRenderer.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Player let area of " + gameObject.name);
        if (other.CompareTag("VogelMann"))
        {
            InventoryManager.Instance.collectableItems.Remove(this);
            highlightRenderer.gameObject.SetActive(false);
        }
    }
}

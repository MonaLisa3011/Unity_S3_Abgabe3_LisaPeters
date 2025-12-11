using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIElementItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textItemName;
    [SerializeField] private Image imageItemSprite;
    public void SetContent(string itemname, Sprite itemSprite)
    {
        textItemName.text = itemname;
        imageItemSprite.sprite = itemSprite;
    }
}

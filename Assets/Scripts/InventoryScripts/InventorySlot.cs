using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Sprite Sprite;

    public Image Icon;

    public DescriptionPanel DescriptionPanelScript;

    public int ButtonIndex;

    public ItemModel ItemModel;

    public void UpdateSlot(bool active)
    {
        ItemModel = Items.ItemModels[ButtonIndex];
        Icon.sprite = active ? Sprite : null;
    }

    public void ShowDescription()
    {
        DescriptionPanelScript.ShowDescription(ItemModel, ButtonIndex);
    }
}

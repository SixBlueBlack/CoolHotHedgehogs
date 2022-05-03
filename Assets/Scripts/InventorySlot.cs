using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Sprite Sprite;

    public Image Icon;

    public void UpdateSlot(bool active)
    {
        Icon.sprite = active ? Sprite : null;
    }
}

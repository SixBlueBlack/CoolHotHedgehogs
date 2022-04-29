using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Sprite sprite;

    public Image icon;

    public void UpdateSlot(bool active)
    {
        icon.sprite = active ? sprite : null;
    }
}

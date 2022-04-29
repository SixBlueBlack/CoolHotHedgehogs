using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DescriptionPanel : MonoBehaviour
{
    public GameObject Panel;

    public void ShowDescription()
    {
        if (Items.HasItems[0] is false) return;
        var texts = transform.GetComponentsInChildren<Text>();
        var images = transform.GetComponentsInChildren<Image>();
        Panel.SetActive(true);
        texts[0].text = Items.ItemModels[0].Title;
        texts[1].text = Items.ItemModels[0].Description;
        images[1].sprite = Items.ItemModels[0].Sprite;
    }
}

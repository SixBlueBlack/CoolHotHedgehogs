using UnityEngine;
using UnityEngine.UI;

public class DescriptionPanel : MonoBehaviour
{
    public GameObject Panel;

    private int buttonIndex;

    private ItemModel itemModel;

    public GameObject ItemPrefab;

    private Transform player;

    private Inventory inventory;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        inventory = FindObjectOfType<Inventory>();
    }

    public void ShowDescription(ItemModel itemModel, int buttonIndex)
    {
        if (Items.HasItems[buttonIndex] is false) return;
        var texts = transform.GetComponentsInChildren<Text>();
        var images = transform.GetComponentsInChildren<Image>();
        Panel.SetActive(true);
        texts[0].text = itemModel.Title;
        texts[1].text = itemModel.Description;
        images[1].sprite = itemModel.Sprite;
        this.itemModel = itemModel;
        this.buttonIndex = buttonIndex;
    }

    public void Use()
    {
        Panel.SetActive(false);
        if (itemModel.Title == "Health Potion")
            player.GetComponent<Player>().Heal(20);
        player.gameObject.GetComponent<Items>().DeleteItem(buttonIndex);
        inventory.UpdateUi();
    }

    public void Drop()
    {
        Debug.Log("Drop");
        var item = Instantiate(ItemPrefab, player.position, Quaternion.identity);
        item.GetComponent<Item>().itemModel = itemModel;
        player.gameObject.GetComponent<Items>().DeleteItem(buttonIndex);
        inventory.UpdateUi();
        Panel.SetActive(false);
    }
}

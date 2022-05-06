using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemModel itemModel;

    private bool isCollided;

    private GameObject player;

    private OutlineScript outlineScript;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        outlineScript = transform.GetComponent<OutlineScript>();
    }

    void Update()
    {
        PickUpItem();
    }

    private void PickUpItem()
    {
        if (!isCollided || !Input.GetKeyDown(KeyCode.E)) return;
        if (player.GetComponent<Items>().FindEmptySlot() == -1) return;
        player.GetComponent<Items>().AddItem(itemModel);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (!hitInfo.CompareTag("Player") || outlineScript is null) return;
        isCollided = true;
        Debug.Log(outlineScript);
        outlineScript.IsOutlined = true;
    }

    void OnTriggerExit2D(Collider2D hitInfo)
    {
        if (!hitInfo.CompareTag("Player")) return;
        isCollided = false;
        outlineScript.IsOutlined = false;
    }
}

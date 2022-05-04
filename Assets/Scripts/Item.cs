using UnityEngine;

public class Item : MonoBehaviour
{
    public int Index;

    private static ItemModel itemModel;

    public Sprite Sprite;

    private bool isCollided;

    public GameObject Player;

    private OutlineScript outlineScript;

    void Start()
    {
        itemModel = new ItemModel("Health Potion", "Just heal yourself", Sprite);
        outlineScript = transform.GetComponent<OutlineScript>();
    }

    void Update()
    {
        if (!isCollided || !Input.GetKeyDown(KeyCode.E)) return;
        Player.GetComponent<Items>().AddItem(Index, itemModel);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (!hitInfo.CompareTag("Player")) return;
        isCollided = true;
        outlineScript.IsOutlined = true;
    }

    void OnTriggerExit2D(Collider2D hitInfo)
    {
        isCollided = false;
        outlineScript.IsOutlined = false;
    }
}

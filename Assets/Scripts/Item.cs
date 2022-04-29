using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int index;
    public static ItemModel ItemModel;
    public Sprite[] Sprites;

    void Start()
    {
        ItemModel = new ItemModel("Health Potion", "Just heal yourself", Sprites[0]);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.name != "Player") return;// || !Input.GetKeyDown(KeyCode.E)) return;
        //Debug.Log(Input.GetKeyDown(KeyCode.E));
        hitInfo.GetComponent<Items>().AddItem(index, ItemModel);//Если наехал игрок, то он сможет подобрать предмет
        Destroy(gameObject); //Удаление объекта с карты
    }
}

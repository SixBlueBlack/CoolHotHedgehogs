using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int index;
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.name != "Player") return;// || !Input.GetKeyDown(KeyCode.E)) return;
        //Debug.Log(Input.GetKeyDown(KeyCode.E));
        hitInfo.GetComponent<Items>().AddItem(index);//Если наехал игрок, то он сможет подобрать предмет
        Destroy(gameObject); //Удаление объекта с карты
    }
}

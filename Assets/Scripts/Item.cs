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
        hitInfo.GetComponent<Items>().AddItem(index);//���� ������ �����, �� �� ������ ��������� �������
        Destroy(gameObject); //�������� ������� � �����
    }
}

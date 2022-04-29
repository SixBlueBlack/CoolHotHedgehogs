using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject InventoryPanel;
    public bool IsInventoryOpened;
    public GameObject player;


    public Transform inventorySlots;

    private InventorySlot[] slots;

    void Start()
    {
        slots = inventorySlots.GetComponentsInChildren<InventorySlot>();
    }

    void Update()
    {
        if (!Input.GetKeyDown(KeyCode.I) || PlayerScript.IsDead || PauseScript.IsPaused) return;
        UpdateUI();
        if (!IsInventoryOpened)
            InventoryPanel.SetActive(true);
        else
            InventoryPanel.SetActive(false);
        IsInventoryOpened = !IsInventoryOpened;
        //Time.timeScale = 0;
    }
    void UpdateUI()
    {
        for (var i = 0; i < slots.Length; i++) //Проверка всех предметов
        {
            var active = Items.hasItems[i];

            slots[i].UpdateSlot(active);
        }
    }
}

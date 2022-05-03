using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject InventoryPanel;

    public bool IsInventoryOpened;

    public GameObject DescriptionPanel;

    public Transform InventorySlots;

    private InventorySlot[] Slots;

    void Start()
    {
        Slots = InventorySlots.GetComponentsInChildren<InventorySlot>();
    }

    void Update()
    {
        if (!Input.GetKeyDown(KeyCode.I) || PlayerScript.IsDead || PauseScript.IsPaused) return;
        UpdateUi();
        InventoryPanel.SetActive(!IsInventoryOpened);
        DescriptionPanel.SetActive(false);
        IsInventoryOpened = !IsInventoryOpened;
        //Time.timeScale = 0;
    }
    void UpdateUi()
    {
        for (var i = 0; i < Slots.Length; i++)
        {
            var active = Items.HasItems[i];

            Slots[i].UpdateSlot(active);
        }
    }
}

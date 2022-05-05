using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject InventoryPanel;

    public static bool IsOpen;

    public GameObject DescriptionPanel;

    public Transform InventorySlots;

    private InventorySlot[] Slots;

    void Start()
    {
        Slots = InventorySlots.GetComponentsInChildren<InventorySlot>();
    }

    void Update()
    {
        if (!Input.GetKeyDown(KeyCode.I) || Player.IsDead || PauseScript.IsPaused) return;
        UpdateUi();
        InventoryPanel.SetActive(!IsOpen);
        DescriptionPanel.SetActive(false);
        IsOpen = !IsOpen;
        Time.timeScale = IsOpen ? 0 : 1;
    }

    public void UpdateUi()
    {
        for (var i = 0; i < Slots.Length; i++)
        {
            var active = Items.HasItems[i];

            Slots[i].UpdateSlot(active);
        }
    }
}

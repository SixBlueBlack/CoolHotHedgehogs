using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static bool IsOpen { get; set; }

    public GameObject InventoryPanel;
    public GameObject DescriptionPanel;
    public Transform InventorySlots;
    private InventorySlot[] Slots;

    void Start()
    {
        Slots = InventorySlots.GetComponentsInChildren<InventorySlot>();
    }

    private void ManageInventoryUi()
    {
        InventoryPanel.SetActive(!IsOpen);
        DescriptionPanel.SetActive(false);
        IsOpen = !IsOpen;
        Time.timeScale = IsOpen ? 0 : 1;
    }

    private static bool CheckOpenConditions() => !Input.GetKeyDown(KeyCode.I) || Player.IsDead || PauseScript.IsPaused;

    void Update()
    {
        if (CheckOpenConditions()) return;
        UpdateSlotsUi();
        ManageInventoryUi();
    }

    public void UpdateSlotsUi()
    {
        for (var i = 0; i < Slots.Length; i++)
        {
            var active = Items.HasItems[i];

            Slots[i].UpdateSlot(active);
        }
    }
}

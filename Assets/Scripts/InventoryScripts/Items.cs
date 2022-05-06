using UnityEngine;

public class Items : MonoBehaviour
{
    public static ItemModel[] ItemModels = new ItemModel[6];

    public static bool[] HasItems = { false, false, false, false, false, false };

    public void AddItem(ItemModel itemModel)
    {
        var index = FindEmptySlot();
        ItemModels[index] = itemModel;
        HasItems[index] = true;
    }

    public void DeleteItem(int index)
    {
        ItemModels[index] = null;
        HasItems[index] = false;
    }

    public int FindEmptySlot()
    {
        for (var i = 0; i < HasItems.Length; i++)
            if (!HasItems[i]) return i;
        return -1;
    }
}

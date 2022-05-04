using UnityEngine;

public class Items : MonoBehaviour
{
    public static ItemModel[] ItemModels = new ItemModel[6];

    public static bool[] HasItems = { false, false, false, false, false, false };

    public void AddItem(int index, ItemModel itemModel)
    {
        ItemModels[index] = itemModel;
        HasItems[index] = true;
    }
}
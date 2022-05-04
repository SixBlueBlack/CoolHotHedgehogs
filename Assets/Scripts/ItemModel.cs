using UnityEngine;

public class ItemModel
{
    public string Title { get; }

    public string Description { get; }

    public Sprite Sprite { get; }

    public ItemModel(string title, string description, Sprite sprite)
    {
        Title = title;
        Description = description;
        Sprite = sprite;
    }
}
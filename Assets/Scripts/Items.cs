using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    public static bool[] hasItems = new bool[] { false, false, false, false, false, false };

    private int currItem = 0;
    public int defence = 0;

    public Sprite[] sprites;


    public void AddItem(int index)
    {
        hasItems[index] = true;
    }
}
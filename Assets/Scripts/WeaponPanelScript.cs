using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponPanelScript : MonoBehaviour
{
    public Image IconImage;

    public int LastWeaponIndex;

    // Start is called before the first frame update
    void Start()
    {
        LastWeaponIndex = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if (LastWeaponIndex != PlayerWeaponScript.CurrentWeaponIndex)
            IconImage.sprite = PlayerWeaponScript.Weapons[PlayerWeaponScript.CurrentWeaponIndex].Sprite;
        LastWeaponIndex = PlayerWeaponScript.CurrentWeaponIndex;
    }
}

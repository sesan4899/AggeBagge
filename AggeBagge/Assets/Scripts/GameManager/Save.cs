using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save
{
    public Save()
    {

    }

    public void SavePlayerInfo(string[] items, string[] equipment)
    {

        for (int i = 0; i < items.Length; i++)
        {
            PlayerPrefs.SetString("InvSlot" + i, items[i]);
        }

        for(int i = 0; i < equipment.Length; i++)
        {
            PlayerPrefs.SetString("EquipSlot" + i, equipment[i]);
        }
    }
}

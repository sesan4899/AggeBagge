using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Load
{

    public Load()
    {

    }

    public void LoadPlayerInfo(out string[] invSlot, out string[] equipSlot)
    {

        invSlot = new string[12];
        equipSlot = new string[6];


        for (int i = 0; i < 12; i++)
        {
            invSlot[i] = PlayerPrefs.GetString("InvSlot" + i);
        }

        for (int i = 0; i < 6; i++)
        {
            equipSlot[i] = PlayerPrefs.GetString("EquipSlot" + i);
        }

    }
}

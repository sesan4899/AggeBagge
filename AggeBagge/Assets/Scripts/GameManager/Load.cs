using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Load
{

    public Load()
    {

    }

    public void LoadPlayerInfo(out string[] invSlot, out string[] equipSlot, out float xPos, out float yPos, out int hp, out int wave, out int killCount)
    {

        xPos = PlayerPrefs.GetFloat("XPos");
        yPos = PlayerPrefs.GetFloat("YPos");
        hp = PlayerPrefs.GetInt("Hp");
        wave = PlayerPrefs.GetInt("Wave");
        killCount = PlayerPrefs.GetInt("KillCount");




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

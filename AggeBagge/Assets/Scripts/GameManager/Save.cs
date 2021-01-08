using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save
{
    public Save()
    {

    }

    public void SavePlayerInfo(string[] items, string[] equipment, float xPos, float yPos, int hp, int wave, int killCount)
    {

        PlayerPrefs.SetFloat("XPos", xPos);
        PlayerPrefs.SetFloat("YPos", yPos);
        PlayerPrefs.SetInt("Hp", hp);
        PlayerPrefs.SetInt("Wave", wave);
        PlayerPrefs.SetInt("KillCount", killCount);



        for (int i = 0; i < items.Length; i++)
        {
            PlayerPrefs.SetString("InvSlot" + i, items[i]);
        }

        for(int i = 0; i < equipment.Length; i++)
        {
            PlayerPrefs.SetString("EquipSlot" + i, equipment[i]);
        }

        PlayerPrefs.Save();
    }
}

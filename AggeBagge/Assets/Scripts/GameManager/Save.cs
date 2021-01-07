using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save
{
    public Save()
    {

    }

    public void SavePlayerInfo(int playerLevel, float playerExp, bool hasPlayerAndObject, string playerName)
    {
        PlayerPrefs.SetInt("PlayerLevel", playerLevel);
        PlayerPrefs.SetFloat("PlayerExp", playerExp);
        //PlayerPrefs.SetInt("PlayerHasObject", hasPlayerAndObject == true ? 1 : 0); //Can't use bool, have to convert

        if(hasPlayerAndObject)
        {
            PlayerPrefs.SetInt("PlayerHasObject", 1);
        }
        else
        {
            PlayerPrefs.SetInt("PlayerHasObject", 0);

        }
        PlayerPrefs.SetString("PlayerName", playerName);


    }
}

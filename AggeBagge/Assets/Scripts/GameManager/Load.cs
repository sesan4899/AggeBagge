using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Load
{

    public Load()
    {

    }

    public void LoadPlayerInfo(out int playerLevel, out float playerExp, out bool hasPlayerAKey, out string playerName)
    {
        playerLevel = PlayerPrefs.GetInt("PlayerLevel");
        playerExp = PlayerPrefs.GetFloat("PlayerExp");
        //hasPlayerAKey = PlayerPrefs.GetInt("PlayerHasObject") == 1 ? true : false;
        if (hasPlayerAKey = PlayerPrefs.GetInt("PlayerHasObject") == 1)
        {
            hasPlayerAKey = true;
        }
        else
        {
            hasPlayerAKey = false;
        }
        playerName = PlayerPrefs.GetString("PlayerName");

    }
}

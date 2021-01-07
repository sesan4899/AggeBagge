using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAndLoadMain : MonoBehaviour
{

    public int playerLevel;
    public float playerExp;
    public bool hasPlayerObject;
    public string playerName;

    private Save save;
    private Load load;



    void Start()
    {
        load = new Load();

        load.LoadPlayerInfo(out playerLevel, out playerExp, out hasPlayerObject, out playerName);
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.S))
        {
            playerLevel = 2;
            playerExp = 3;
            hasPlayerObject = true;
            playerName = "AGGE";


            save = new Save();
            save.SavePlayerInfo(playerLevel, playerExp, hasPlayerObject, playerName);
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCheckPoint : MonoBehaviour
{
    CheckPoint lastCheckPoint;
    public GameObject player;


    #region Singleton
    public static GameCheckPoint instance;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);

        }
        else
            Destroy(this);

    }

    #endregion

    void Update()
    {
        if(Input.GetKey(KeyCode.R))
        {
            KillPlayerAndRespawn();
        }
    }

    void KillPlayerAndRespawn()
    {
        player.transform.position = lastCheckPoint.transform.position;
    }

    public void UpdateLastCheckPoint(CheckPoint checkPoint)
    {

        lastCheckPoint = checkPoint;
    }



}

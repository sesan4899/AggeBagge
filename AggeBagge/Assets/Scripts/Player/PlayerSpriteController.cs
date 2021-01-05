using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteController : MonoBehaviour
{
    private PlayerController myPlayerController;
    void Start()
    {
        myPlayerController = gameObject.GetComponentInParent<PlayerController>();
    }


    public void FootStep()
    {
        myPlayerController.myAudioManager.FootStep.Play();
    }

    public void EndRoll()
    {
        myPlayerController.isRolling = false;
    }
}

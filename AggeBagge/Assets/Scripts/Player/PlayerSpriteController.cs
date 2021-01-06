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
        myPlayerController.disableInput = false;
    }

    public void EndAttack()
    {
        myPlayerController.attack1 = false;
        myPlayerController.attack2 = false;
        myPlayerController.attack3 = false;
        myPlayerController.disableInput = false;
        myPlayerController.comboTimeCounter = myPlayerController.comboTime;
        myPlayerController.damageCollision = false;
    }

    public void EnableDamageCollision()
    {
        myPlayerController.damageCollision = true;
    }
    
}

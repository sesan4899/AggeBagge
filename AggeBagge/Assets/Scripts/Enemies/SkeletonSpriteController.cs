using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonSpriteController : MonoBehaviour
{
    public  SkeletonAIController myController;
    void Start()
    {
    }

    public void EndAttack()
    {
        myController.attack1 = false;
        myController.attack2 = false;
        myController.attacking = false;
        myController.damageCollision = false;
    }

    public void EnableDamageCollision()
    {
        myController.damageCollision = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    public float speed;
    public float damage;
    public float attackSpeed;
    public float knockbackForce;
    public float maxhp;
    public float potionPower;

    void Update()
    {
        Vector3 theScale = transform.localScale;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position = transform.position - Vector3.right * speed * Time.deltaTime;

            
            theScale.x = -1;
            transform.localScale = theScale;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position = transform.position + Vector3.right * speed * Time.deltaTime;

            theScale.x = 1;
            transform.localScale = theScale;
        }

    }

    public void GetEquipmentStats(float dmg, float atkSpeed, float knockback, float hp, float moveSpeed, float potionHp)
    {
        damage += dmg;
        attackSpeed += atkSpeed;
        knockbackForce += knockback;
        maxhp += hp;
        speed += moveSpeed;
        potionPower += potionHp;

        //if (dmg < 0)
        //    dmg = 0;
        //if (atkSpeed < 0)
        //    atkSpeed = 0;
        //if (knockback < 0)
        //    knockback = 0;
        //if (hp < 0)
        //    hp = 0;
        //if (moveSpeed < 0)
        //    moveSpeed = 0;
    }

}

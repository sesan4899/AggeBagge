using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUsePotion : MonoBehaviour
{
    public KeyCode DrinkPotionKey;
    public float recoverSpeed;

    int recoverHp = 0;

    float counter;
    PlayerHealthController playerHp;

    void Start()
    {
        playerHp = GetComponent<PlayerHealthController>();
        counter = recoverSpeed;
    }

    void Update()
    {
        if (Input.GetKeyDown(DrinkPotionKey))
        {
            if(EquipmentManager.instance.currentEquipment[5] != null)
            {
                recoverHp = EquipmentManager.instance.currentEquipment[5].hp;
                EquipmentManager.instance.UsePotion();
            }
        }

        if(recoverHp > 0 && playerHp.health < playerHp.maxHealth)
        {
            counter -= Time.deltaTime;

            if(counter <= 0)
            {
                playerHp.health ++;

                if(playerHp.health > playerHp.maxHealth)
                {
                    playerHp.health = playerHp.maxHealth;
                }

                counter = recoverSpeed;
                recoverHp--;
            }

        }
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HUD : MonoBehaviour
{
    public GameObject playerGO;
    public Slider healthSlider;

    public Text damage;
    public Text wave;
    public Text enemies;
    public Text hp;

    public Image consumable;

    PlayerController player;
    PlayerHealthController playerHp;

    void Start()
    {
        player = playerGO.GetComponent<PlayerController>();
        playerHp = playerGO.GetComponent<PlayerHealthController>();

        healthSlider.value = playerHp.maxHealth;
    }

    void Update()
    {
        //UI Info Text
        damage.text = "Damage: " + player.damage;
        wave.text = "Wave: " + WaveManager.instance.wave;
        enemies.text = "Enemies: " + WaveManager.instance.enemy.Count;


        //HP Bar
        hp.text = "HP: " + playerHp.health + " / " + playerHp.maxHealth;

        healthSlider.maxValue = playerHp.maxHealth;
        healthSlider.value = playerHp.health;


        //Consumable
        if(EquipmentManager.instance.currentEquipment[5] != null)
        {
            consumable.sprite = EquipmentManager.instance.currentEquipment[5].icon;
            consumable.gameObject.SetActive(true);
        }
        else
        {
            consumable.gameObject.SetActive(false);
        }
    }


}

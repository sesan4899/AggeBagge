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
    public Text kill;
    public Text hp;

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
        damage.text = "Damage: " + player.damage;
        hp.text = "HP: " + playerHp.health + " / " + playerHp.maxHealth;

        healthSlider.maxValue = playerHp.maxHealth;
        healthSlider.value = playerHp.health;
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    public int maxHealth;
    public int health;

    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Damaged");
        health -= damage;

        if(health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        //die
    }
}

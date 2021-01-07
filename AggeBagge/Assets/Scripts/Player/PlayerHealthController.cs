using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public int maxHealth;
    public int health;
    public bool stunned = false;
    public float knockBackForce;
    public float knockBackTime;
    public float knockBackTimeCounter;
    private Rigidbody2D myRigidbody;
    public Animator myAnimator;
    private GameObject enemy;
    public bool dead;
    private PlayerController myPlayer;

    void Start()
    {
        health = maxHealth;
        myRigidbody = GetComponent<Rigidbody2D>();
        myPlayer = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        Clocks();
        Animation();
    }

    private void Clocks()
    {
        if (knockBackTimeCounter > 0)
        {
            knockBackTimeCounter -= Time.deltaTime;
            stunned = true;

            if (enemy.transform.position.x > transform.position.x)
            {
                myRigidbody.velocity = new Vector3(-knockBackForce, knockBackForce, 0f);
            }
            else
            {
                myRigidbody.velocity = new Vector3(knockBackForce, knockBackForce, 0f);
            }

        }
        else
        {
            stunned = false;
        }
    }

    private void Animation()
    {
        myAnimator.SetBool("Dead", dead);
    }
    public void TakeDamage(int damage, GameObject enemyReference)
    {
        Debug.Log("Lol");
        if (!dead)
        {
            Debug.Log("Loll");
            health -= damage;
            knockBackTimeCounter = knockBackTime;
            enemy = enemyReference;
            myRigidbody.velocity = new Vector2(0f, 0f);

            if (health <= 0)
            {
                dead = true;
                myPlayer.DisableInput();
            }
        }
        else
        {
            myRigidbody.velocity = new Vector2(0f, 0f);
        }
    }
}

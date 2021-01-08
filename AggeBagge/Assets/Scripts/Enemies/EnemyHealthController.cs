using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    public int maxHealth;
    public int health;
    public bool stunned = false;
    public float knockBackForce;
    public float knockBackTime;
    public float knockBackTimeCounter;
    private Rigidbody2D myRigidbody;
    public Animator myAnimator;
    private GameObject player;
    public bool dead;

    void Start()
    {
        health = maxHealth;
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Clocks();
        Animation();
    }

    private void Clocks()
    {
        if(knockBackTimeCounter > 0)
        {
            knockBackTimeCounter -= Time.deltaTime;
            stunned = true;

            if (player.transform.position.x > transform.position.x)
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
    public void TakeDamage(int damage, GameObject playerReference)
    {
        if(!dead)
        {
            health -= damage;
            knockBackTimeCounter = knockBackTime;
            player = playerReference;
            myRigidbody.velocity = new Vector2(0f, 0f);

            if (health <= 0)
            {
                dead = true;
                Debug.Log("!");
                ItemList.instance.DropItem(transform.position);
            }
        }
    }
}

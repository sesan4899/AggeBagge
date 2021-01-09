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
    public AudioManager myAudioManager;
    void Start()
    {
        health = maxHealth;
        myRigidbody = GetComponent<Rigidbody2D>();
        myAudioManager = FindObjectOfType<AudioManager>();
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
    public void TakeDamage(int damage, GameObject playerReference, float knockBack)
    {
        if(!dead)
        {
            health -= damage;
            knockBackForce = knockBack;
            knockBackTimeCounter = knockBackTime;
            player = playerReference;
            myRigidbody.velocity = new Vector2(0f, 0f);
            myAudioManager.BloodHit.Play();

            if (health <= 0)
            {
                dead = true;
                WaveManager.instance.Kill(gameObject);
                ItemList.instance.DropItem(transform.position);
                myAudioManager.SkeletonDeath.Play();
            }
        }
    }
}

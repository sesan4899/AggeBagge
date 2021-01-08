using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAIController : MonoBehaviour
{
    [Header("Stats")]
    public float moveSpeed;
    public float detectionRange;
    public float attackDetectionRange;
    public float attackRange;
    public float attackMoveForce;
    public int damage;

    [Header("Behaviour")]
    public float idleTime;
    public float idleTimeCounter;
    public float walkTime;
    public float walkTimeCounter;
    public float stunTime;
    public float stunTimeCounter;

    private Animator myAnimator;
    private Rigidbody2D myRigidbody;
    private AudioManager myAudioManager;
    private EnemyHealthController myHealth;
    public GameObject player;
    public Transform attackPoint;
    public LayerMask playerLayer;



    [Header("Data")]
    public string state;
    public float distanceToPlayer = 10000;
    public bool playerRight;
    public bool moveRight;
    public bool idle = true;
    public bool stunned;
    public bool attacking;
    public bool attack1;
    public bool attack2;
    public bool damageCollision;


    void Start()
    {
        GetAllReferences();
        state = "Patrol";
    }

    // Update is called once per frame
    void Update()
    {
        StateBehaviour();
        Clocks();
        GetDistanceToPlayer();
        Animations();
        FlipSelf();
        AutomaticStateSwitch();
    }

    private void GetAllReferences()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myHealth = GetComponent<EnemyHealthController>();
        myAudioManager = FindObjectOfType<AudioManager>();
        myAnimator = transform.GetComponentInChildren<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");

    }

    private void FlipSelf()
    {
        if (!stunned && !attacking)
        {
            if (myRigidbody.velocity.x > 0f)
            {
                transform.localScale = new Vector3(3f, 3f, 3f);
            }

            if (myRigidbody.velocity.x < 0f)
            {
                transform.localScale = new Vector3(-3f, 3f, 3f);
            }
        }
    }


    private void Animations()
    {
        if (myAnimator != null)
        {
            myAnimator.SetFloat("Speed", Mathf.Abs(myRigidbody.velocity.x));
            myAnimator.SetBool("Knocked", stunned);
            myAnimator.SetBool("Attack1", attack1);
            myAnimator.SetBool("Attack2", attack2);
        }
    }

    private void Clocks()
    {
        if (idleTimeCounter > 0f)
        {
            idleTimeCounter -= Time.deltaTime;

            if (idleTimeCounter <= 0f)
            {
                idle = false;

            }
        }

        if (walkTimeCounter > 0f)
        {
            walkTimeCounter -= Time.deltaTime;

            if (walkTimeCounter <= 0f)
            {
                idle = true;
            }
        }
    }

    private void GetDistanceToPlayer()
    {
        distanceToPlayer = (player.transform.position - transform.position).magnitude;

        if (player.transform.position.x - transform.position.x > 0f)
        {
            playerRight = true;
        }
        else
        {
            playerRight = false;
        }
    }

    private void AutomaticStateSwitch()
    {
        if (myHealth.stunned && !stunned)
        {
            state = "Stunned";
        }

        if (myHealth.dead)
        {
            state = "Dead";
        }

    }

    private void StateBehaviour()
    {
        switch (state)
        {
            case "Patrol":
                Patrol();
                break;

            case "Pursue":
                Pursue();
                break;

            case "Stunned":
                Stunned();
                break;

            case "Dead":
                Dead();
                break;

            default:
                break;
        }
    }

    private void Patrol()
    {
        if (idle && idleTimeCounter <= 0f)
        {
            idleTime = Random.Range(1, 5);
            idleTimeCounter = idleTime;
        }
        if (!idle && walkTimeCounter <= 0f)
        {
            walkTime = Random.Range(1, 5);
            walkTimeCounter = walkTime;
        }

        if (idle)
        {
            myRigidbody.velocity = new Vector2(0f, 0f);
        }
        else
        {
            if (moveRight)
            {
                myRigidbody.velocity = new Vector3(moveSpeed, myRigidbody.velocity.y, 0f);
            }
            else
            {
                myRigidbody.velocity = new Vector3(-moveSpeed, myRigidbody.velocity.y, 0f);
            }
        }



        if (distanceToPlayer < detectionRange)
        {
            state = "Pursue";
        }
    }

    private void Pursue()
    {

        if (playerRight && !attacking)
        {
            myRigidbody.velocity = new Vector3(moveSpeed, myRigidbody.velocity.y, 0f);
        }
        if(!playerRight && !attacking)
        {
            myRigidbody.velocity = new Vector3(-moveSpeed, myRigidbody.velocity.y, 0f);
        }

        if(attacking)
        {
            myRigidbody.velocity = new Vector2(0f, 0f);
        }

        if(distanceToPlayer < attackDetectionRange)
        {
            attacking = true;

            if(attacking)
            {
 
                myRigidbody.velocity = new Vector2(0f, myRigidbody.velocity.y);

                int combo = Random.Range(1, 3);

                    switch (combo)
                    {
                        case 1:
                            attack1 = true;
                            break;
                        case 2:
                            attack2 = true;
                            break;

                        default:
                            break;
                    }
          

                if (attack1 || attack2)
                {
                    if (transform.localScale.x > 0f)
                    {
                        myRigidbody.velocity = new Vector2(attackMoveForce, myRigidbody.velocity.y);
                    }
                    else
                    {
                        myRigidbody.velocity = new Vector2(-attackMoveForce, myRigidbody.velocity.y);
                    }

                }

                if (damageCollision)
                {
                    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

                    foreach (Collider2D enemy in hitEnemies)
                    {
                        enemy.GetComponent<PlayerHealthController>().TakeDamage(damage, gameObject);
                        damageCollision = false;
                    }
                }
            }
        }

        if (distanceToPlayer > detectionRange)
        {
            state = "Patrol";
        }
    }

    private void Stunned()
    {

        if(!stunned && stunTimeCounter <= 0f)
        {
            stunTimeCounter = stunTime;
        }
        stunned = true;
        stunTimeCounter -= Time.deltaTime;

        if (stunTimeCounter <= 0f)

        {
            stunned = false;
            state = "Pursue";
        }
    }

    private void Dead()
    {
        myRigidbody.velocity = new Vector2(0f, 0f);
    }
}

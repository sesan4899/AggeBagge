using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Stats")]
    public float moveSpeed;
    public float jumpForce;
    public float jumpTime;
    public float rollForce;
    public float rollTime;
    public float attackMoveForce;
    public float comboTime;
    public float attackRange;
    public int damage;
    public float knockBackForce;
    public float knockBackTime;


    [Header("References")]
    public Transform groundCheck;
    public LayerMask whatIsGround;
    public LayerMask enemyLayer;
    private Rigidbody2D myRigidbody;
    public AudioManager myAudioManager;
    private Animator myAnimator;
    public Transform attackPoint;

    [Header("State-Debugging")]
    public bool isDead;
    public bool isGrounded;
    public bool stoppedJumping;
    public bool isRolling;
    public bool disableInput;
    public bool attack1;
    public bool attack2;
    public bool attack3;
    public bool damageCollision;


    [Header("Data")]
    public Vector2 groundCheckSize;
    private float jumpTimeCounter;
    public float rollTimeCounter;
    public float comboTimeCounter;
    public int combo;
    public float knockBackTimeCounter;





    void Start()
    {
        GetAllReferences();
    }

    // Update is called once per frame
    void Update()
    {
        Animations();
        CheckGround();
        Clocks();
    }

    private void FixedUpdate()
    {
        if (!isDead)
        {
            Movement();
            Jump();
            Roll();
            Attack();
        }
    }

    private void GetAllReferences()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAudioManager = FindObjectOfType<AudioManager>();
        myAnimator = transform.GetComponentInChildren<Animator>();

        rollTimeCounter = rollTime;
    }

    private void Animations()
    {
        //Animations
        if (myAnimator != null)
        {
            myAnimator.SetFloat("Speed", Mathf.Abs(myRigidbody.velocity.x));
            myAnimator.SetBool("Grounded", isGrounded);
            myAnimator.SetBool("Rolling", isRolling);
            myAnimator.SetBool("Attack1", attack1);
            myAnimator.SetBool("Attack2", attack2);
            myAnimator.SetBool("Attack3", attack3);
            /*
            myAnimator.SetBool("isWallSliding", isWallSliding);
            myAnimator.SetBool("isDead", isDead);
            */
        }
    }

    private void CheckGround()
    {
        //Checks Ground
        isGrounded = Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0f, whatIsGround);
    }

    private void Clocks()
    {
        if (rollTimeCounter > 0f)
        {
            rollTimeCounter -= Time.deltaTime;
        }

        if (comboTimeCounter > 0f)
        {
            comboTimeCounter -= Time.deltaTime;
        }

        if (comboTimeCounter <= 0f)
        {
            combo = 0;
        }
    }

    private void Movement()
    {
        ///Add velocity according to input
        if (!disableInput)
        {
            myRigidbody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, myRigidbody.velocity.y);
        }

        if (myRigidbody.velocity.x > 0f && !disableInput)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

        if (myRigidbody.velocity.x < 0f && !disableInput)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }

    private void Jump()
    {
        //Initial Jump
        if (Input.GetButton("Jump") && isGrounded && stoppedJumping && jumpTimeCounter > 0f && !disableInput)
        {
            myAudioManager.Jump.Play();
            myRigidbody.velocity = new Vector3(myRigidbody.velocity.x, jumpForce, 0f);
            stoppedJumping = false;
        }

        //Super Jump
        if (Input.GetButton("Jump") && !stoppedJumping && jumpTimeCounter > 0f)
        {
            myRigidbody.velocity = new Vector3(myRigidbody.velocity.x, jumpForce, 0f);
            jumpTimeCounter -= Time.deltaTime;
        }

        //Reset jump
        if (!Input.GetButton("Jump"))
        {
            jumpTimeCounter = 0;
            stoppedJumping = true;
        }

        // Always updates ground bool
        if (isGrounded && stoppedJumping)
        {
            jumpTimeCounter = jumpTime;
        }

    }

    private void Roll()
    {

        if (Input.GetButton("Roll") && isGrounded && rollTimeCounter <= 0f && !disableInput)
        {
            isRolling = true;
            rollTimeCounter = rollTime;
            myAudioManager.Roll.Play();
        }

        if (isRolling)
        {
            if (transform.localScale.x > 0f)
            {
                myRigidbody.velocity = new Vector2(rollForce, myRigidbody.velocity.y);
            }
            else
            {
                myRigidbody.velocity = new Vector2(-rollForce, myRigidbody.velocity.y);
            }

            DisableInput();

        }
    }

    private void Attack()
    {
        if (Input.GetButton("Attack") && !disableInput)
        {
            myRigidbody.velocity = new Vector2(0f, myRigidbody.velocity.y);
            DisableInput();
            combo++;




            switch (combo)
            {
                case 1:
                    attack1 = true;
                    myAudioManager.Swing1.Play();
                    break;
                case 2:
                    attack2 = true;
                    myAudioManager.Swing2.Play();
                    break;
                case 3:
                    attack3 = true;
                    myAudioManager.Swing3.Play();
                    combo = 0;
                    break;
                default:
                    break;
            }
        }

        if (attack1 || attack2 || attack3)
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
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

            foreach (Collider2D enemy in hitEnemies)
            {
                if (enemy.gameObject.tag == "Enemy")
                {
                    enemy.GetComponent<EnemyHealthController>().TakeDamage(damage, gameObject, knockBackForce);
                    damageCollision = false;
                }

                if (enemy.gameObject.tag == "Box")
                {
                   enemy.GetComponent<BoxScript>().TakeDamage(gameObject);
                    damageCollision = false;
                }
            }
        }
    }

        public void GetEquipmentStats(int dmg, float atkSpeed, float knockback, int hp, float moveSpeed, float potionHp)
        {
            damage += dmg;
            knockBackForce += knockback;
            moveSpeed += moveSpeed;

            GetComponent<PlayerHealthController>().maxHealth += hp;
        }


        public void DisableInput()
        {
            disableInput = true;
        }

        void OnDrawGizmos()
        {
            if (attackPoint == null)
            {
                return;
            }
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(attackPoint.position, attackRange);
        }
    
}
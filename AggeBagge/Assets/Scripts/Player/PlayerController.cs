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

    [Header("References")]
    public Transform groundCheck;
    public LayerMask whatIsGround;
    private Rigidbody2D myRigidbody;
    public AudioManager myAudioManager;
    private Animator myAnimator;

    [Header("State-Debugging")]
    public bool isDead;
    public bool isGrounded;
    public bool stoppedJumping;
    public bool isRolling;

    [Header("Data")]
    public Vector2 groundCheckSize;
    private float jumpTimeCounter;
    public float rollTimeCounter;




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
        if(rollTimeCounter > 0f)
        {
            rollTimeCounter -= Time.deltaTime;
        }
    }

    private void Movement()
    {
        ///Add velocity according to input
        if (!isRolling)
        {
            myRigidbody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, myRigidbody.velocity.y);
        }

        if (myRigidbody.velocity.x > 0f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

        if (myRigidbody.velocity.x < 0f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }

    private void Jump()
    {
        //Initial Jump
        if (Input.GetButton("Jump") && isGrounded && stoppedJumping && jumpTimeCounter > 0f && !isRolling)
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

        if (Input.GetButton("Roll") && isGrounded && rollTimeCounter <= 0f && !isRolling)
        {
            isRolling = true;
            rollTimeCounter = rollTime;
        }

        if(isRolling)
        {
            if (transform.localScale.x > 0f)
            {
                myRigidbody.velocity = new Vector2(rollForce, myRigidbody.velocity.y);
            }
            else
            {
                myRigidbody.velocity = new Vector2(-rollForce, myRigidbody.velocity.y);
            }

        }
    }
}
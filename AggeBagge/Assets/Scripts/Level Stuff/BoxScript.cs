using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    public float knockForce;
    public bool dead;
    public GameObject player;
    private Rigidbody2D myRigidbody;
    public float attackRange;
    public bool damageCollision;
    public float damageThreshold;
    public LayerMask enemyLayer;
    public int damage;
    public AudioManager myAudioManager;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAudioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {

        if(myRigidbody.velocity.magnitude > damageThreshold)
        {
            damageCollision = true;
        }
        else
        {
            damageCollision = false;
        }


        if (damageCollision)
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayer);

            foreach (Collider2D enemy in hitEnemies)
            {
                if (enemy.gameObject.tag == "Enemy")
                {
                    enemy.GetComponent<EnemyHealthController>().TakeDamage(damage, gameObject);
                }
            }
        }
    }

    public void TakeDamage(GameObject playerReference)
    {
        if (!dead)
        {
            player = playerReference;
            Vector2 direction;
            myAudioManager.BoxHit.Play();

            if (player.transform.position.x < transform.position.x)
            {
                direction = new Vector2(knockForce*2, knockForce);
                myRigidbody.AddForce(direction, ForceMode2D.Impulse);
                myRigidbody.AddTorque(1, ForceMode2D.Impulse);
            }
            if (player.transform.position.x > transform.position.x)
            {
                direction = new Vector2(-knockForce*2, knockForce);
                myRigidbody.AddForce(direction, ForceMode2D.Impulse);
                myRigidbody.AddTorque(1, ForceMode2D.Impulse);
            }
        }
    }

    private void Destroyed()
    {
        dead = true;
        gameObject.SetActive(false);
    }

    void OnDrawGizmos()
    {

        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, attackRange);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    public float knockForce;
    public bool dead;
    public GameObject player;
    private Rigidbody2D myRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(GameObject playerReference)
    {
        if (!dead)
        {
            player = playerReference;
            Vector2 direction;

            if(player.transform.position.x < transform.position.x)
            {
                direction = new Vector2(knockForce*2, knockForce);
                myRigidbody.AddForce(direction, ForceMode2D.Impulse);
            }
            if (player.transform.position.x > transform.position.x)
            {
                direction = new Vector2(-knockForce*2, knockForce);
                myRigidbody.AddForce(direction, ForceMode2D.Impulse);
            }
        }
    }

    private void Destroyed()
    {
        dead = true;
        gameObject.SetActive(false);
    }
}

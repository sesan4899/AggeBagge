using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    public float speed;
    
    void Start()
    {
        
    }

    void Update()
    {
        Vector3 theScale = transform.localScale;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position = transform.position - Vector3.right * speed * Time.deltaTime;

            
            theScale.x = -1;
            transform.localScale = theScale;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position = transform.position + Vector3.right * speed * Time.deltaTime;

            theScale.x = 1;
            transform.localScale = theScale;
        }

    }
}

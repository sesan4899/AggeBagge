using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraPan : MonoBehaviour
{
    public GameObject cmCameraPan;
    public GameObject cam;

    public float startSpeed;
    public float maxSpeed;
    public float acceleration;
    public float deacceleration;

    float speed;

    bool isDeaccelerating;
    GameObject player = null;

    void Start()
    {
        speed = startSpeed;
    }


    void Update()
    {
        transform.position = transform.position - new Vector3(speed * Time.deltaTime, 0, 0);

        if(!isDeaccelerating && speed < maxSpeed)
            speed = speed + acceleration * Time.deltaTime;
        else if(isDeaccelerating && speed > startSpeed)
            speed = speed - deacceleration * Time.deltaTime;

        if(player != null)
        {
            CinemachineVirtualCamera cm = cmCameraPan.GetComponent<CinemachineVirtualCamera>();

            float cm_height = 2f * cm.m_Lens.OrthographicSize;
            float cm_width = cm_height * cm.m_Lens.Aspect;

            if (player.transform.position.x >= transform.position.x - cm_width)
            {
                cm.Priority = 0;
                Destroy(gameObject, 5);
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = collision.gameObject;
            isDeaccelerating = true;
        }
    }
}

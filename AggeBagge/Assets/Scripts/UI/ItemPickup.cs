using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemPickup : MonoBehaviour
{
    public Item item;
    private bool isTouching;

    void Update()
    {
        if(isTouching)
            if (Input.GetKeyDown(KeyCode.F))
                PickUp();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            isTouching = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isTouching = false;
        }
    }

    void PickUp()
    {

        bool isThereSpace = InventoryManager.instance.Add(item);



        if(isThereSpace)
        {
            Destroy(gameObject);
        }
    }
}

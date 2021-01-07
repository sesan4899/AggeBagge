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

    void PickUp()
    {
        bool isThereSpace = InventoryManager.instance.Add(item);

        if(isThereSpace)
        {
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            isTouching = true;

            item.SetDescription();
            PopUpUI.instance.DisplayGroundItemInfo(item.name, item.itemDescription, new Vector2(Screen.width / 2, Screen.height / 12));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isTouching = false;

            PopUpUI.instance.DestroyGroundItemInfo();
        }
    }
}

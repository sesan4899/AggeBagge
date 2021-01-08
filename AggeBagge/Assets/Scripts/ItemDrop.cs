using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemDrop : MonoBehaviour
{
    public Item item;
    private bool isTouching;
    float alpha = 1;
    float aliveTime;

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = item.icon;

        aliveTime = ItemList.instance.itemAliveTime;
        Destroy(gameObject, aliveTime);
    }

    void Update()
    {
        aliveTime -= Time.deltaTime;

        if (isTouching)
            if (Input.GetKeyDown(KeyCode.F))
                PickUp();

        if(aliveTime < 5)
        {
            SpriteRenderer sprite = GetComponent<SpriteRenderer>();
            alpha -= 0.2f * Time.deltaTime;
            sprite.material.color = new Color(1, 1, 1, alpha);
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

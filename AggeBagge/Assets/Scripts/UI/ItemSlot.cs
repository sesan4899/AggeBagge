using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    public Image icon;
    public Sprite equipmentIcon = null;
    Item item;

    public UnityEvent onLeft;
    public UnityEvent onRight;
    public UnityEvent onMiddle;

   
    //Adds itemIcon in slot
    public void AddItem (Item newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
    }

    //Clears the slot
    public void ClearSlot()
    {
        item = null;

        if(equipmentIcon == null)
        {
            icon.sprite = null;
            icon.enabled = false;

        }
        else
            icon.sprite = equipmentIcon;
    }

    //Equip or DeEquip an item
    public void UseItem()
    {
        if (item != null)
        {
            if (equipmentIcon == null)
                item.Equip();
            else
                item.DeEquip();
        }
    }

    //Drop item
    public void DropItem()
    {
        if(item != null)
        {
            Vector3 playerPos = GameObject.FindWithTag("Player").transform.position;
            Instantiate(item.objectPrefab, playerPos + new Vector3(0, 0.8f, 0), Quaternion.identity).GetComponent<ItemDrop>().item = item;

            InventoryManager.instance.Remove(item);
        }
    }

    //Clicking on an item
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            UseItem();
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (equipmentIcon == null)
                DropItem();
            else
                UseItem();
        }
    }

    

    public void OnCursorEnter()
    {
        if (item != null)
        {
            item.SetDescription();

            PopUpUI.instance.DisplayItemInfo(item.name, item.itemDescription, new Vector2(transform.position.x + 185, transform.position.y + 20));
        }
    }
    public void OnCursorExit()
    {
        if (item != null)
            PopUpUI.instance.DestroyItemInfo();
    }


}

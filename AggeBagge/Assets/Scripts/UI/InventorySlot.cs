using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    public Image icon;
    public Sprite equipmentIcon = null;
    Item item;

    public UnityEvent onLeft;
    public UnityEvent onRight;
    public UnityEvent onMiddle;

    public void AddItem (Item newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
    }

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

    public void DropItem()
    {
        if(item != null)
        {
            Vector3 playerPos = GameObject.FindWithTag("Player").transform.position;
            Instantiate(item.objectPrefab, playerPos, Quaternion.identity);

            InventoryManager.instance.Remove(item);
        }
    }

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

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton

    public static EquipmentManager instance;

    void Awake ()
    {
        instance = this;
    }

    #endregion

    public Item[] currentEquipment;
    InventoryManager inventory;


    void Start ()
    {
        inventory = InventoryManager.instance;

        int numberOfSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Item[numberOfSlots];
    }

    public void Equip(Item newItem)
    {
        int slotIndex = (int)newItem.Equipslot;

        Item oldItem = null;

        if(currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);
        }

        currentEquipment[slotIndex] = newItem;
    }

    public void DeEquip(Item equippedItem)
    {
        int slotIndex = (int)equippedItem.Equipslot;

        currentEquipment[slotIndex] = null;
    }

}

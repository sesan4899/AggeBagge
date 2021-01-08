using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton

    public static EquipmentManager instance;
    public GameObject player;
    void Awake ()
    {
        instance = this;

        inventory = InventoryManager.instance;

        int numberOfSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Item[numberOfSlots];
    }

    #endregion

    public Item[] currentEquipment;
    InventoryManager inventory;

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    void Start ()
    {
        //inventory = InventoryManager.instance;

        //int numberOfSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        //currentEquipment = new Item[numberOfSlots];
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
        StatsChange(newItem, oldItem);

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

    public void DeEquip(Item equippedItem)
    {
        int slotIndex = (int)equippedItem.Equipslot;

        currentEquipment[slotIndex] = null;

        Item noItem = null;
        StatsChange(noItem, equippedItem);

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

    public void StatsChange(Item newItem, Item oldItem)
    {
        float dmg = 0;
        float atkSpeed = 0;
        float knockback = 0;
        float hp = 0;
        float moveSpeed = 0;
        float potionHp = 0;

        if(newItem != null)
        {
            dmg = newItem.damage;
            atkSpeed = newItem.attackSpeed;
            knockback = newItem.knockbackForce;
            moveSpeed = newItem.speed;

            if (newItem.Equipslot != EquipmentSlot.Consumable)
                hp = newItem.hp;
            else
                potionHp = newItem.hp;
        }

        if (oldItem != null)
        {
            dmg -= oldItem.damage;
            atkSpeed -= oldItem.attackSpeed;
            knockback -= oldItem.knockbackForce;
            moveSpeed -= oldItem.speed;

            if(oldItem.Equipslot != EquipmentSlot.Consumable)
                hp -= oldItem.hp;
            else
                potionHp -= oldItem.hp;
        }

        player.GetComponent<PlayerTest>().GetEquipmentStats(dmg, atkSpeed, knockback, hp, moveSpeed, potionHp);

    }


}

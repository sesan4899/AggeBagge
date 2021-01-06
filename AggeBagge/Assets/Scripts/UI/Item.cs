using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name;
    public Sprite icon;
    public EquipmentSlot Equipslot;
    public GameObject objectPrefab;

    [Header("Stat Modifiers")]
    public float damage;
    public float KnockbackForce;
    public float attackSpeed;
    public float speed;
    public float hp;

    public virtual void Equip()
    {
        EquipmentManager.instance.Equip(this);
        InventoryManager.instance.Remove(this);
    }
    public virtual void DeEquip()
    {
        EquipmentManager.instance.DeEquip(this);
        InventoryManager.instance.Add(this);
    }

}

public enum EquipmentSlot {Head, Chest, Feet, Weapon, Shield, Consumable, Consumable2, Consumable3}
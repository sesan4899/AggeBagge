using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    [Header("Item Type")]
    new public string name;
    public Sprite icon;
    
 
    public int Tier = 0;
    public EquipmentSlot Equipslot;
    public GameObject objectPrefab;
    [System.NonSerialized] public string itemDescription;

    [Header("Stat Modifiers")]
    public float damage;
    public float knockbackForce;
    public float attackSpeed;
    public float speed;
    public float hp;

    public void SetDescription()
    {
        if(Equipslot == EquipmentSlot.Chest || Equipslot == EquipmentSlot.Head || Equipslot == EquipmentSlot.Shield)
            itemDescription = "+ " + hp + " Hp";

        else if (Equipslot == EquipmentSlot.Feet)
            itemDescription = "+ " + hp + " Hp\n" + "+ " + speed + " Speed";

        else if (Equipslot == EquipmentSlot.Weapon)
            itemDescription = "+ " + damage + " Damage\n" + "+ " + knockbackForce + " KnockBack";

        else
            itemDescription = "Recover " + hp + " Hp";
        
    }

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

public enum EquipmentSlot {Head, Chest, Feet, Weapon, Shield, Consumable}
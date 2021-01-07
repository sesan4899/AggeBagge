using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemList : MonoBehaviour
{
    public List<Item> item = new List<Item>();
    
    public void GetItemFromName(string itemName, bool equip)
    {
        for(int i = 0; i < item.Count; i++)
        { 
            if(itemName == item[i].name)
            {
                if(equip)
                {
                    item[i].Equip();
                    //EquipmentManager.instance.Equip(item[i]);
                }
                else
                {
                    InventoryManager.instance.Add(item[i]);
                }
            }
        }
    }
}

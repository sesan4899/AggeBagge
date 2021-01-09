using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemList : MonoBehaviour
{
    #region Singleton

    public static ItemList instance; 

    void Awake()
    {
        instance = this;
    }

    #endregion

    public List<Item> item = new List<Item>();
    public float itemAliveTime;

    [Header("Tier Rarity")]
    public float tier0Rarity;
    public float tier1Rarity;
    public float tier2Rarity;
    public float tier3Rarity;

 
    public bool getItemFromName(string itemName, bool equip)
    {
        for(int i = 0; i < item.Count; i++)
        {

            if (itemName == item[i].name)
            {

                if (equip)
                    EquipmentManager.instance.Equip(item[i]);
                else
                    InventoryManager.instance.Add(item[i]);

                return true;
            }

        }

        return false;
    }

    public void DropItem(Vector3 dropPos)
    {
        bool[] addtier = new bool[4];

        List<Item> dropList = new List<Item>();
        int percent = Random.Range(1, 100);


        if (percent <= tier3Rarity)
        {
            addtier[0] = true;
            addtier[1] = true;
            addtier[2] = true;
            addtier[3] = true;

        }
        else if (percent <= tier2Rarity)
        {
            addtier[0] = true;
            addtier[1] = true;
            addtier[2] = true;
        }
        else if (percent <= tier1Rarity)
        {
            addtier[0] = true;
            addtier[1] = true;
        }
        else if (percent <= tier0Rarity)
        {
            addtier[0] = true;
        }


        //Checks the tier of all items, to see which items to add to the drop list
        for(int i = 0; i < item.Count; i++)
        {
            for(int t = 0; t < addtier.Length; t++)
            {
                if (addtier[t] == true)
                {
                    if (item[i].tier == t)
                    {
                        dropList.Add(item[i]);
                    }
                }
            }
        }

        if(dropList.Count > 0)
        {
            int index = Random.Range(0, dropList.Count);
            Instantiate(dropList[index].objectPrefab, dropPos, Quaternion.identity).GetComponent<ItemDrop>().item = dropList[index];
        }
    }

}

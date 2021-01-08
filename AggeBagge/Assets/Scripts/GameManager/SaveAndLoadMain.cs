using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAndLoadMain : MonoBehaviour
{
    public bool doNotLoad;
    public bool reset;

    public string[] invSlot;
    public string[] equipSlot;


    //public List<string> items = new List<string>();
    public string[] invItems = null;
    public string[] currentEquipment = null;


    private Save save;
    private Load load;


    void Start()
    {
        load = new Load();

        if(!doNotLoad)
        {
            if(!reset)
                load.LoadPlayerInfo(out invSlot, out equipSlot);

            for (int i = 0; i < invSlot.Length; i++)
            {
                if(invSlot[i] != null)
                {

                    if(reset)
                    {
                        invSlot[i] = null;
                        return;
                    }
                    else
                    {
                        bool equip = false;
                        bool foundItem = ItemList.instance.getItemFromName(invSlot[i], equip);

                        if (!foundItem)
                        {
                            invSlot[i] = null;
                        }

                    }

                }
                
            }

            for (int i = 0; i < equipSlot.Length; i++)
            {
                if (equipSlot[i] != null)
                {
                    if (reset)
                    {
                        equipSlot[i] = null;
                        return;
                    }
                    else
                    {
                        bool equip = true;
                        bool foundItem = ItemList.instance.getItemFromName(equipSlot[i], equip);

                        if (!foundItem)
                        {
                            equipSlot[i] = null;
                        }

                    }
                }
            }

        }
        
    }


    public void SaveAndExit()
    {
        currentEquipment = new string[6];
        invItems = new string[12];


        for (int i = 0; i < InventoryManager.instance.items.Count; i++)
        {
            if (InventoryManager.instance.items[i] != null)
                invItems[i] = InventoryManager.instance.items[i].name;

            //items.Add(InventoryManager.instance.items[i].name);
        }

        for (int i = 0; i < EquipmentManager.instance.currentEquipment.Length; i++)
        {

            if (EquipmentManager.instance.currentEquipment[i] != null)
                currentEquipment[i] = EquipmentManager.instance.currentEquipment[i].name;
            
        }


        save = new Save();
        save.SavePlayerInfo(invItems, currentEquipment);

        Application.Quit();
    }



}

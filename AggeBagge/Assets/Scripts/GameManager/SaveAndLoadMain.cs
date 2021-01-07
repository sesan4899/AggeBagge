using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAndLoadMain : MonoBehaviour
{

    public string[] invSlot;
    public string[] equipSlot;


    public List<string> items = new List<string>();
    public string[] currentEquipment;


    private Save save;
    private Load load;


    void Start()
    {
        load = new Load();

        load.LoadPlayerInfo(out invSlot, out equipSlot);

        for (int i = 0; i < invSlot.Length; i++)
        {
            if(invSlot[i] != null)
            {
                bool equip = false;
                GetComponent<ItemList>().GetItemFromName(invSlot[i], equip);
            }
        }

        for (int i = 0; i < equipSlot.Length; i++)
        {
            if (invSlot[i] != null)
            {
                bool equip = true;
                GetComponent<ItemList>().GetItemFromName(equipSlot[i], equip);
            }
        }
    }


    public void SaveAndExit()
    {
        currentEquipment = new string[6];

        for(int i = 0; i < InventoryManager.instance.items.Count; i++)
        {
            if (InventoryManager.instance.items[i] != null)
                items.Add(InventoryManager.instance.items[i].name);
        }

        for(int i = 0; i < EquipmentManager.instance.currentEquipment.Length; i++)
        {

            if (EquipmentManager.instance.currentEquipment[i] != null)
                currentEquipment[i] = EquipmentManager.instance.currentEquipment[i].name;
            
        }


        save = new Save();
        save.SavePlayerInfo(items, currentEquipment);

        Application.Quit();
    }



}

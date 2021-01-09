using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAndLoadMain : MonoBehaviour
{

    public bool doNotLoad;
    public bool reset;
    public GameObject playerGO;

    [Header("Loaded")]
    public string[] invSlot;
    public string[] equipSlot;

    public float xPos;
    public float yPos;
    public int hp;
    public int wave;
    public int killCount;

    [Header("Saved")]
    public string[] invItems = null;
    public string[] currentEquipment = null;


    Save save;
    Load load;

    PlayerHealthController playerHp;

    void Start()
    {
        playerHp = playerGO.GetComponent<PlayerHealthController>();


        if (reset)
        {
            PlayerPrefs.DeleteAll();
        }

        load = new Load();

        if(!doNotLoad)
        {
            load.LoadPlayerInfo(out invSlot, out equipSlot, out xPos, out yPos, out hp, out wave, out killCount);
            
            playerHp.health = hp;


            if (hp <= 0)
                playerHp.health = playerHp.maxHealth;
            
            else
            {
               playerGO.transform.position = new Vector2(xPos, yPos);
                for (int i = 0; i < invSlot.Length; i++)
                {
                    if(invSlot[i] != null)
                    {
                        bool equip = false;
                        bool foundItem = ItemList.instance.getItemFromName(invSlot[i], equip);

                        if (!foundItem)
                        {
                            invSlot[i] = null;
                        }

                    }
                
                }

                for (int i = 0; i < equipSlot.Length; i++)
                {
                    if (equipSlot[i] != null)
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

    void Update()
    {
        if(playerHp.health <= 0)
        {
            PlayerPrefs.DeleteAll();
        }
    }

    public void SaveGame()
    {
        
        float xPos = playerGO.transform.position.x;
        float yPos = playerGO.transform.position.y;
        int currentHealth = playerHp.health;
        int wave = GetComponent<WaveManager>().wave;
        int killCount = 0;

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
        save.SavePlayerInfo(invItems, currentEquipment, xPos, yPos, currentHealth, wave, killCount);

    }

    public void Quit()
    {
        Application.Quit();

    }



}

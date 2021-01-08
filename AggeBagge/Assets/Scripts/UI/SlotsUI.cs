using UnityEngine;

public class SlotsUI : MonoBehaviour
{
    public Transform invSlotParent;
    public Transform equipSlotParent;
    public GameObject inventoryHUD;
    public GameObject equipmentHUD;
    public KeyCode openInventoryKey;
    InventoryManager inventory;
    EquipmentManager equipment;

    ItemSlot[] invSlots;
    ItemSlot[] equipSlots;

    void Start()
    {
        inventory = InventoryManager.instance;
        equipment = EquipmentManager.instance;

        //Call UpdateUI whenever onItemChangedCallback is triggered
        inventory.onItemChangedCallback += UpdateUI;
        equipment.onItemChangedCallback += UpdateUI;

        invSlots = invSlotParent.GetComponentsInChildren<ItemSlot>();
        equipSlots = equipSlotParent.GetComponentsInChildren<ItemSlot>();
    }

    void Update()
    {
        if(Input.GetKeyDown(openInventoryKey))
        {
            inventoryHUD.SetActive(!inventoryHUD.activeSelf);
            equipmentHUD.SetActive(!equipmentHUD.activeSelf);

            PopUpUI.instance.DestroyItemInfo();
        }
    }

    void UpdateUI()
    {
        for(int i = 0; i < invSlots.Length; i++)
        {
            if(i < inventory.items.Count)
            {
                invSlots[i].AddItem(inventory.items[i]);
            }
            else
            {
                invSlots[i].ClearSlot();
            }
        }

        for (int i = 0; i < equipSlots.Length; i++)
        {
            if (equipment.currentEquipment[i] != null)
            {
                equipSlots[i].AddItem(equipment.currentEquipment[i]);
            }
            else
            {
                equipSlots[i].ClearSlot();
            }
        }

    }

}

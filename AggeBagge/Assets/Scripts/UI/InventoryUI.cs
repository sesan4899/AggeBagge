using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform slotParent;
    public GameObject inventoryUI;
    public KeyCode openInventoryKey;
    Inventory inventory;

    InventorySlot[] slots;
    
    void Start()
    {
        inventory = Inventory.instance;

        //Call UpdateUI whenever onItemChangedCallback is triggered
        inventory.onItemChangedCallback += UpdateUI;

        slots = slotParent.GetComponentsInChildren<InventorySlot>();
    }

    void Update()
    {
        if(Input.GetKeyDown(openInventoryKey))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
    }

    void UpdateUI()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if(i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}

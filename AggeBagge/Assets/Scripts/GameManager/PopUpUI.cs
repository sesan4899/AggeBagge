using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpUI : MonoBehaviour
{
    #region Singleton
    public static PopUpUI instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of inventory found!");
            return;
        }

        instance = this;
    }

    #endregion

    public List<Item> itemList = new List<Item>();

    public Transform canvas;
    public GameObject itemInfoPrefab;
    GameObject currentItemInfo = null;
    GameObject currentGroundItemInfo = null;

    public void DisplayItemInfo(string itemName, string itemDescription, Vector2 pos)
    {
        if (currentItemInfo != null)
        {
            Destroy(currentItemInfo.gameObject);
        }

        currentItemInfo = Instantiate(itemInfoPrefab, pos, Quaternion.identity, canvas);
        currentItemInfo.GetComponent<ItemInfo>().SetUp(itemName, itemDescription);
    }

    public void DisplayGroundItemInfo(string itemName, string itemDescription, Vector2 pos)
    {
        if (currentGroundItemInfo != null)
        {
            Destroy(currentGroundItemInfo.gameObject);
        }

        currentGroundItemInfo = Instantiate(itemInfoPrefab, pos, Quaternion.identity, canvas);
        currentGroundItemInfo.GetComponent<ItemInfo>().SetUp(itemName, itemDescription);
    }

    public void DestroyItemInfo()
    {
        if(currentItemInfo != null)
        {
            Destroy(currentItemInfo.gameObject);
        }
    }

    public void DestroyGroundItemInfo()
    {
        if (currentGroundItemInfo != null)
        {
            Destroy(currentGroundItemInfo.gameObject);
        }
    }

}

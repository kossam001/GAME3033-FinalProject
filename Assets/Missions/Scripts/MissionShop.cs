using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionShop : Shop
{
    [SerializeField] private MissionSlot mission;
    [Tooltip("Mission content panel")]
    [SerializeField] private GameObject missionPanel;

    private List<ItemSlot> itemSlots = new List<ItemSlot>();

    // Start is called before the first frame update
    void Start()
    {
        InitShop();
    }

    private void InitShop()
    {
        // itemTypeToInventoryTable = new Dictionary<ItemType, GameObject>();
        // itemTypeToSlotTable = new Dictionary<ItemType, List<ShopSlot>>();

        //for (int i = 0; i < inventoryType.Count; i++)
        //{
        //    itemTypeToInventoryTable[inventoryType[i]] = inventoryPanel[i];
        //    itemTypeToSlotTable[inventoryType[i]] = new List<ShopSlot>();
        //}

        // 

        // Sort the items into seeds and towers and organize them into the correct inventories
        foreach (ItemTable missionTable in masterItemTables)
        {
                SetupSlot(missionTable.GetItem(Random.Range(0, missionTable.items.Length)), missionPanel, itemSlots);
        }
    }

    public override void SellItem(Item item)
    {
        
    }
}

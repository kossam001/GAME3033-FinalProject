using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Shop : MonoBehaviour
{
    [Tooltip("Reference to master item tables")]
    [SerializeField]
    protected List<ItemTable> masterItemTables;

    [Header("Shop")]
    [Tooltip("Inventory panels")]
    [SerializeField] private List<GameObject> inventoryPanel;
    [Tooltip("Item slot template")]
    [SerializeField] protected ItemSlot itemTemplate;
    [Tooltip("Inventory type")]
    [SerializeField] private List<ItemType> inventoryType;

    private Dictionary<ItemType, GameObject> itemTypeToInventoryTable;
    private Dictionary<ItemType, List<ItemSlot>> itemTypeToSlotTable;
    private List<List<ItemSlot>> itemSlots;

    // Start is called before the first frame update
    void Start()
    {
        InitShop();
    }

    private void InitShop()
    {
        itemTypeToInventoryTable = new Dictionary<ItemType, GameObject>();
        itemTypeToSlotTable = new Dictionary<ItemType, List<ItemSlot>>();

        for (int i = 0; i < inventoryType.Count; i++)
        {
            itemTypeToInventoryTable[inventoryType[i]] = inventoryPanel[i];
            itemTypeToSlotTable[inventoryType[i]] = new List<ItemSlot>();
        }

        // Sort the items into seeds and towers and organize them into the correct inventories
        foreach (Item item in masterItemTables[0].items)
        {
            if (item.Sellable)
                SetupSlot(item, itemTypeToInventoryTable[item.Type], itemTypeToSlotTable[item.Type]);
        }
    }

    protected void SetupSlot(Item item, GameObject parentPanel, List<ItemSlot> slots)
    {
        GameObject newObject = Instantiate(itemTemplate.gameObject, parentPanel.transform);
        ShopSlot slot = newObject.GetComponent<ShopSlot>();
        slot.shop = this;
        slots.Add(slot);

        slot.shop = this;
        slot.SetContents(item, 1);
    }

    public virtual void SellItem(Item item)
    {
        InventoryController.Instance.AddToInventory(item);
    }
}

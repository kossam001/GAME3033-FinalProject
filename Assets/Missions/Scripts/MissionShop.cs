using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionShop : Shop
{
    [Tooltip("Mission content panel")]
    [SerializeField] private GameObject missionPanel;

    private List<ItemSlot> itemSlots = new List<ItemSlot>();

    // Start is called before the first frame update
    void Start()
    {
        InitShop();
    }

    protected override void InitShop()
    {
        // Sort the items into seeds and towers and organize them into the correct inventories
        foreach (ItemTable missionTable in masterItemTables)
        {
                SetupSlot(missionTable.GetItem(Random.Range(0, missionTable.items.Length)), missionPanel, itemSlots);
        }
    }

    public override void SelectItem(Item item)
    {
        Mission mission = item as Mission;
        GameManager.Instance.missionData = mission;
        SceneController.Instance.LoadScene(mission.stageName);
    }
}

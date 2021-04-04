using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MissionSlot : ShopSlot
{
    [SerializeField] private Mission mission;

    public override void ShowInfo()
    {
        Mission mission = ItemInSlot as Mission;

        string itemInfo = "";
        itemInfo += mission.Name + "\n\n";
        itemInfo += mission.Description + "\n\n";
        itemInfo += "Difficulty: " + mission.diffculty + "\n\n";
        itemInfo += "Reward: " + mission.reward;

        descriptionUI.GetComponentInChildren<TMP_Text>().text = itemInfo;

        descriptionUI.SetActive(true);
    }
}

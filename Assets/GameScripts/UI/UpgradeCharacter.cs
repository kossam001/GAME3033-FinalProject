using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeCharacter : MonoBehaviour
{
    public void UpgradeHealth(int amount)
    {
        if (InventoryController.Instance.money < 100) return;

        InventoryController.Instance.SetMoneyAmount(InventoryController.Instance.money - 100);
        InventoryController.Instance.statSheet.health += amount;

        GameManager.Instance.SaveGame();
    }

    public void UpgradeAttack(int amount)
    {
        if (InventoryController.Instance.money < 100) return;

        InventoryController.Instance.SetMoneyAmount(InventoryController.Instance.money - 100);
        InventoryController.Instance.statSheet.attack += amount;

        GameManager.Instance.SaveGame();
    }

    public void UpgradeDefense(int amount)
    {
        if (InventoryController.Instance.money < 100) return;

        InventoryController.Instance.SetMoneyAmount(InventoryController.Instance.money - 100);
        InventoryController.Instance.statSheet.defense += amount;

        GameManager.Instance.SaveGame();
    }
}

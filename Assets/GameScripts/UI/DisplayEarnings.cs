using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayEarnings : MonoBehaviour
{
    private void Start()
    {
        if (InventoryController.Instance == null) return;

        InventoryController.Instance.OnEarningsChanged += UpdateEarningsText;
        UpdateEarningsText();
    }

    private void UpdateEarningsText()
    {
        GetComponent<TMP_Text>().text = "Your Earnings: " + InventoryController.Instance.money;
    }
        

    private void OnDisable()
    {
        if (InventoryController.Instance != null)
            InventoryController.Instance.OnEarningsChanged -= UpdateEarningsText;
    }
}

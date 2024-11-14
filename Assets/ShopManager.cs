using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI Repairprice;
    [SerializeField]
    Button RepairB;
    [SerializeField]
    float RepairPrice;

    private void OnEnable()
    {   
        if (PlayerStats.Instance.Maxhealth - PlayerStats.Instance.health != 0) 
        {
            RepairB.interactable = true;
            RepairPrice = (PlayerStats.Instance.Maxhealth - PlayerStats.Instance.health) * 2;
            Repairprice.text = "$" + RepairPrice.ToString();
        } else
        {
            RepairB.interactable = false;
            Repairprice.text = "$" + 0;
        }
    }

    private void UpdateAfterPurchase()
    {
        Repairprice.text = "$" + 0;
        RepairB.interactable = false;
    }

    private void RepairButton()
    {
        if (RepairPrice <= PlayerStats.Instance.Money)
        {
            PlayerStats.Instance.SpendMoney(RepairPrice);
            PlayerStats.Instance.Heal();
            PlayerStats.Instance.Money -= RepairPrice;
            UpdateAfterPurchase();
        }
    }
}

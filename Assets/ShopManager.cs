using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI RepairPriceText;
    [SerializeField]
    Button RepairB;
    [SerializeField]
    float RepairPrice;
    [SerializeField]
    TextMeshProUGUI CurrencyAmmount;

    private void Awake()
    {
        CurrencyUpdate();
    }

    private void OnEnable()
    {   
        if (PlayerStats.Instance.Maxhealth - PlayerStats.Instance.health != 0) 
        {
            RepairB.interactable = true;
            RepairPrice = (PlayerStats.Instance.Maxhealth - PlayerStats.Instance.health) * 2;
            RepairPriceText.text = "$" + RepairPrice.ToString();
        } else
        {
            RepairB.interactable = false;
            RepairPriceText.text = "$" + 0;
        }
    }

    private void CurrencyUpdate()
    {
        CurrencyAmmount.text = "$"+PlayerStats.Instance.Money.ToString();
    }

    private void UpdateAfterPurchase()
    {
        RepairPriceText.text = "$" + 0;
        RepairB.interactable = false;
        CurrencyUpdate();
    }

    public void RepairButton()
    {
        if (RepairPrice <= PlayerStats.Instance.Money)
        {
            PlayerStats.Instance.SpendMoney(RepairPrice);
            PlayerStats.Instance.Heal();
            UpdateAfterPurchase();
        }
    }

    public void ImprovedArmor()
    {
        if (PlayerStats.Instance.Money >= 50f)
        {
            PlayerStats.Instance.SpendMoney(50f);
            PlayerStats.Instance.IncreaseHealthAmmount();
            UpdateAfterPurchase();
            CurrencyUpdate();
        }
    }

    public void ImprovedAmmo()
    {
        if (PlayerStats.Instance.Money >= 50f)
        {
            PlayerStats.Instance.SpendMoney(50f);
            PlayerStats.Instance.IncreaseAmmoDamager();
            CurrencyUpdate();
        }
    }
}

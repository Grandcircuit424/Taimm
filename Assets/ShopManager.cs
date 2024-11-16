using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{ 
    public static ShopManager Instance;

    [SerializeField]
    TextMeshProUGUI RepairPriceText;
    [SerializeField]
    Button RepairB;
    [SerializeField]
    float RepairPrice;
    [SerializeField]
    TextMeshProUGUI CurrencyAmmount;

    [SerializeField]
    Button ArmorUpgrade;

    [SerializeField]
    Button WeaponUpgrade;

    [SerializeField]
    TextMeshProUGUI ArmorUpgradeText;

    [SerializeField]
    TextMeshProUGUI WeaponUpgradeText;

    private void Awake()
    {
        CurrencyUpdate();
        Instance = this;
    }

    public void RepairText()
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

    public void CurrencyUpdate()
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

            ArmorUpgrade.interactable = false;
            ArmorUpgradeText.text = "Bought";

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

            WeaponUpgrade.interactable = false;
            WeaponUpgradeText.text = "Bought";

            CurrencyUpdate();
        }
    }

    public void BuyShockwave()
    {
        if (PlayerStats.Instance.Money >= 30f)
        {
            PlayerStats.Instance.SpendMoney(30f);
            PlayerStats.Instance.BuyShockwave();

            CurrencyUpdate();
            UIManager.Instance.UpdateEquipment();
        }
    }

    public void BuyMedkits()
    {
        if (PlayerStats.Instance.Money >= 30f)
        {
            PlayerStats.Instance.SpendMoney(30f);
            PlayerStats.Instance.BuyMedkit();

            CurrencyUpdate();
            UIManager.Instance.UpdateEquipment();
        }
    }
}

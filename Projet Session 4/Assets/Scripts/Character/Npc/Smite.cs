using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Smite : Npc
{
    [SerializeField] private int upgradeArmorCost = 30;
    [SerializeField] private Text armorCostText = null;
    [SerializeField] private int upgradeWeaponCost = 30;
    [SerializeField] private Text weaponCostText = null;
    [SerializeField] private int priceIncrease = 10;

    void Start()
    {
        armorCostText.text = upgradeArmorCost.ToString() + "g";
        weaponCostText.text = upgradeWeaponCost.ToString() + "g";
    }

    public  void UpgradeArmor()
    {
        if(listener.Gold >= upgradeArmorCost)
        {
            listener.DecreaseGold(upgradeArmorCost);
            listener.Health.AddModifier(25);
            listener.IncreaseArmorStack(1);
            upgradeArmorCost += priceIncrease;
            armorCostText.text = upgradeArmorCost.ToString();
        }
    }

    public void UpgradeWeapon()
    {
        if (listener.Gold >= upgradeWeaponCost)
        {
            listener.DecreaseGold(upgradeWeaponCost);
            listener.PowerPhysical.AddModifier(10);
            listener.PowerMagical.AddModifier(10);
            upgradeWeaponCost += priceIncrease;
            weaponCostText.text = upgradeWeaponCost.ToString();
        }
    }
}

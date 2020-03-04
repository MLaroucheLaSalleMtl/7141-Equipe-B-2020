using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopKeeper : Npc
{
    [Header("ShopKeeper")]
    [SerializeField] private ShopInventory inventory = null;
    [SerializeField] private int rerollCost = 30;
    [SerializeField] private Text rerollCostText = null;

    void Start()
    {
        rerollCostText.text = rerollCost.ToString() + "g";
    }


    public void ActivateRerollShop()
    {
        if (GameObject.Find("Player").GetComponent<Player>().Gold >= rerollCost)
        {
            inventory.RerollShop();
            GameObject.Find("Player").GetComponent<Player>().DecreaseGold(rerollCost);
        }
    }
}

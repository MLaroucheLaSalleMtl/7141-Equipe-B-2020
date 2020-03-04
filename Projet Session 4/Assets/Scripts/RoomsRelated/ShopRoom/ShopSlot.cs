using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSlot : MonoBehaviour
{
    private ShopInventory inventory;

    void Start()
    {
        inventory = GetComponentInParent<ShopInventory>();
    }

    public void DestroyItem()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}

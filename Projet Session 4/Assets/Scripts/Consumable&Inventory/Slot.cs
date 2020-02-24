using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    private InventoryComponant inventory;

    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryComponant>();
    }

    void Update()
    {
        if (transform.childCount <= 0)
        {
            inventory.isFull = false;
        }
    }
    public void DropItem()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<ConsumableComponant>().DropConsumable();
            Destroy(child.gameObject);
        }
    }
}

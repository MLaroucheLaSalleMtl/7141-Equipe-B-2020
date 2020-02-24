using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : MonoBehaviour
{
    #region Variables & Attributs
    private InventoryComponant inventory = null;
    public GameObject consumableButton;

    [HideInInspector] public float cooldown;
    [HideInInspector] public int charges;
    [HideInInspector] public float cooldownCountdown ;
    private bool firstUse = true;
    #endregion


    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryComponant>();
    }

    public void AssignRetainedAttributs(float Cooldown, int Charges, float CooldownCountdown)
    {
        firstUse = false;
        cooldown = Cooldown;
        charges = Charges;
        cooldownCountdown = CooldownCountdown;
    }
    private void GiveRetainedAttributs(ConsumableComponant consumable)
    {
        consumable.Cooldown = cooldown;
        consumable.CooldownCountdown = cooldownCountdown;
        consumable.Charges = charges;
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            if (inventory.isFull == false && collider.GetComponent<Player>().canInteract == true) 
            {
                inventory.isFull = true;
                GameObject clone = Instantiate(consumableButton, inventory.slot.transform, false);
                if(!firstUse)
                GiveRetainedAttributs(clone.GetComponent<ConsumableComponant>());
                Destroy(gameObject);
            }
        }
    }
}

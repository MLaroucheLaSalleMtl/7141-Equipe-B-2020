using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wish : MonoBehaviour
{
    private Player listener;
    [SerializeField] private GameObject chest = null;

    void Start()
    {
        listener = GetComponentInParent<AncientSpirit>().listener;
    }

    public void IncreaseHealth(float Amount)
    {
        listener.HealthMaximum.AddModifier(Amount);
        listener.AddHealth(listener.HealthMaximum.GetValue());

        Destroy(GetComponentInParent<AncientSpirit>().gameObject);
    }

    public void IncreaseGold(int Amount)
    {
        listener.IncreaseGold(Amount);

        Destroy(GetComponentInParent<AncientSpirit>().gameObject);
    }

    public void IncreaseLevel()
    {
        listener.LevelUp();
        listener.ExperienceCurrent = 0;

        Destroy(GetComponentInParent<AncientSpirit>().gameObject);
    }
    public void IncreaseMana(float Amount)
    {
        listener.ManaMaximum.AddModifier(Amount);
        listener.AddMana(listener.ManaMaximum.GetValue());

        Destroy(GetComponentInParent<AncientSpirit>().gameObject);
    }
    public void IncreaseArmorStack()
    {
        listener.IncreaseArmorStack(1);

        Destroy(GetComponentInParent<AncientSpirit>().gameObject);
    }

    public void IncreaseNumberOfCharge(int Amount)
    {
        if (listener.GetComponent<InventoryComponant>().slot.gameObject.GetComponentInChildren<ConsumableComponant>() != null)
        {
        listener.GetComponent<InventoryComponant>().slot.gameObject.GetComponentInChildren<ConsumableComponant>().MaximumCharges += Amount;
        listener.GetComponent<InventoryComponant>().slot.gameObject.GetComponentInChildren<ConsumableComponant>().RechargeConsumable();
        Destroy(GetComponentInParent<AncientSpirit>().gameObject);
        }

    }

    public void CreateChest()
    {
        Instantiate(chest, new Vector3(transform.position.x,0.8f,transform.position.z), Quaternion.identity);
        Instantiate(chest, new Vector3(-transform.position.x/2, 0.8f, transform.position.z), Quaternion.identity);

        Destroy(GetComponentInParent<AncientSpirit>().gameObject);
    }
}

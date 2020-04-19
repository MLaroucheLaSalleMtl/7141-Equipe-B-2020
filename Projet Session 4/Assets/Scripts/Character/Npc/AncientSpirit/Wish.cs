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
        listener.Health.AddModifier(Amount);
        listener.Health.IncreaseCurrentValue(listener.Health.GetBaseValue());

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
        listener.Mana.AddModifier(Amount);
        listener.Mana.IncreaseCurrentValue(listener.Mana.GetBaseValue());

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
        Instantiate(chest, new Vector3(GetComponentInParent<AncientSpirit>().gameObject.transform.position.x,0.5f, GetComponentInParent<AncientSpirit>().gameObject.transform.position.z), Quaternion.identity);

        Destroy(GetComponentInParent<AncientSpirit>().gameObject);
    }
}

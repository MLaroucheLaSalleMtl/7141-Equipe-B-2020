using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Consumable : MonoBehaviour
{
    #region Variables & Attributs
    private InventoryComponant inventory = null;
    public GameObject interactionSprite;

    [Header("Effect")]
    public GameObject consumableButton;

    [Header("Shop Related")]
    [SerializeField] private bool locked = false;
    [SerializeField] private int cost = 0;
    [SerializeField] private Text costText = null;
    [SerializeField] private string shopName = null;
    [SerializeField] private Text nameText = null;



    [HideInInspector] public float cooldown;
    [HideInInspector] public int charges;
    [HideInInspector] public float cooldownCountdown ;
    private float safetyCountdown = 1f;
    private bool firstUse = true;
    #endregion


    void Start()
    {
        if(locked != false)
        {
            costText.text = cost.ToString();
        }
        nameText.text = shopName;
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryComponant>();
    }

    void Update()
    {
        safetyCountdown = Mathf.Clamp(safetyCountdown -= Time.deltaTime, 0, Mathf.Infinity);
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
        consumable.CurrentCharges = charges;
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.CompareTag("Player") && safetyCountdown == 0 && locked == false)
        {
            if (interactionSprite != null)
                interactionSprite.SetActive(true);

            if (inventory.isFull == false) 
            {
                inventory.isFull = true;
                GameObject clone = Instantiate(consumableButton, inventory.slot.transform, false);
                if(!firstUse)
                GiveRetainedAttributs(clone.GetComponent<ConsumableComponant>());
                Destroy(gameObject);
            }
            else if (inventory.isFull == true && collider.GetComponent<Player>().isInteracting == true) // Switch Consumable
            {
                GameObject buttonInSlot = GameObject.Find("InventorySlot").transform.GetChild(0).gameObject;
                buttonInSlot.GetComponent<ConsumableComponant>().DropConsumable();
            }
        }
        else if (collider.CompareTag("Player") && locked)
        {
            if (interactionSprite != null)
            interactionSprite.SetActive(true);

            if (collider.GetComponent<Player>().Gold >= cost)
            {
                collider.GetComponent<Player>().DecreaseGold(cost);
                locked = false;
            }
            else
            {
                return;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(interactionSprite != null)
            interactionSprite.SetActive(false);
    }
}

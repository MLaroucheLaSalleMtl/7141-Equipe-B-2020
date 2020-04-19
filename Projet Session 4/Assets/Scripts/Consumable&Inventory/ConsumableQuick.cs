using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ConsumableQuick : MonoBehaviour
{
    [Header("Collectable Related")]
    [SerializeField] private UnityEvent effect = null;
    private Player target = null;
    private InventoryRelic inventoryRelic = null;

    [Header("Shop Related")]
    [SerializeField] private bool locked = false;
    [SerializeField] private int cost = 0;
    [SerializeField] private Text costText = null;
    [SerializeField] private string shopName = null;
    [SerializeField] private Text nameText = null;
    private float safetyCountdown = 1f;

    private PopupManager popupManager;

    void Start()
    {
        popupManager = GameObject.Find("PopupManager").GetComponent<PopupManager>();

        inventoryRelic = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryRelic>();
        if (locked != false)
        {
            costText.text = cost.ToString();
        }
            nameText.text = shopName;
    }
    void Update()
    {
        safetyCountdown = Mathf.Clamp(safetyCountdown -= Time.deltaTime, 0, Mathf.Infinity);
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.CompareTag("Player") && locked == false && safetyCountdown == 0)
        {
            target = collider.GetComponent<Player>();
            effect.Invoke();
        }
        else if (collider.CompareTag("Player") && locked)
        {
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

    public void Healing(float Amount)
    {
        if (target.Health.GetCurrentValue() != target.Health.GetBaseValue())
        {
            target.GetComponent<Player>().Health.IncreaseCurrentValue(Amount);
            Destroy(gameObject);
        }
    }
    public void IncreaseBarrier(float Amount)
    {
        if (target.Barrier.GetCurrentValue() != target.Barrier.GetBaseValue())
        {
            target.GetComponent<Player>().Barrier.IncreaseCurrentValue(Amount);
            Destroy(gameObject);
        }
    }
    public void IncreaseExperience(float Amount)
    {
        target.GetComponent<Player>().IncreaseExperience(Amount);
        Destroy(gameObject);

    }
    public void IncreaseMana(float Amount)
    {
        if (target.Mana.GetCurrentValue() != target.Mana.GetBaseValue())
        {
            target.GetComponent<Player>().Mana.IncreaseCurrentValue(Amount);
            Destroy(gameObject);
        }
    }

    public void AddRelicToInventory(string godName)
    {
        foreach (Relic relic in inventoryRelic.relics)
        {
            if(godName == relic._name && relic.isLocked == true)
            {
                relic.UnlockTheRelic();
                relic.lockedVisual.SetActive(false);
                popupManager.InitializeBox(relic);
                Destroy(gameObject);
                break;
            }
        }
    }


}

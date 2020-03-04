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

    [Header("Shop Related")]
    [SerializeField] private bool locked = false;
    [SerializeField] private int cost = 0;
    [SerializeField] private Text costText = null;
    [SerializeField] private string shopName = null;
    [SerializeField] private Text nameText = null;
    private float safetyCountdown = 1f;


    void Start()
    {
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
        if (target.HealthCurrent != target.HealthMaximum.GetValue())
        {
            target.GetComponent<Player>().AddHealth(Amount);
            Destroy(gameObject);
        }
    }
    public void IncreaseBarrier(float Amount)
    {
        if (target.BarrierCurrent != target.BarrierMaximum.GetValue())
        {
            target.GetComponent<Player>().AddBarrier(Amount);
            Destroy(gameObject);
        }
    }
    public void IncreaseExperience(float Amount)
    {
        target.GetComponent<Player>().GainExperience(Amount);
        Destroy(gameObject);

    }
    public void IncreaseMana(float Amount)
    {
        if (target.ManaCurrent != target.ManaMaximum.GetValue())
        {
            target.GetComponent<Player>().AddMana(Amount);
            Destroy(gameObject);
        }
    }
}

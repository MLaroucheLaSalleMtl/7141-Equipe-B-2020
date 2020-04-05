﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ConsumableComponant : MonoBehaviour
{
    #region Variables & Attributs
    private Actor caster = null;
    [Header("Consumable Properties")]
    [SerializeField] private GameObject consumable = null;
    [SerializeField] private int maximumCharges = 0;
    private int currentCharges = 0;
    [SerializeField] private Cooldown cooldown = null;
   // [SerializeField] private float cooldown = 0;
  //  private float cooldownCountdown = 0;
    [Header("Consumable UI & Others")]
    [SerializeField] protected Text txtCharges = null;
    [SerializeField] private Image imgCooldown = null;
    private Transform dropPosition = null;

    [Header("Effect")]
    public UnityEvent useConsumableEffect;

    public int CurrentCharges { get => currentCharges; set => currentCharges = value; }
    public Cooldown Cooldown { get => cooldown; set => cooldown = value; }
    public int MaximumCharges { get => maximumCharges; set => maximumCharges = value; }
    public Actor Caster { get => caster; set => caster = value; }
    #endregion

    #region Unity's Methods

    void Awake()
    {
        currentCharges = maximumCharges;
    }

    void Start()
    {
        txtCharges.text = currentCharges.ToString();
        caster = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        dropPosition = caster.transform.GetChild(1).transform;
    }

    void Update()
    {
        cooldown.StartCooldown();
        imgCooldown.fillAmount = cooldown.GetCountdownValue() / cooldown.GetBaseValue();

        if (Input.GetKeyDown(KeyCode.Z))
        {
            DropConsumable();
        }
    }
    #endregion

    #region Methods 

    #region Consumable Actions

    public void DropConsumable()
    {
        if (caster == null) return;
        GameObject clone = Instantiate(consumable, dropPosition.position, Quaternion.identity);
        clone.GetComponent<Consumable>().AssignRetainedAttributs(cooldown.GetBaseValue(), currentCharges, cooldown.GetCountdownValue());
        Destroy(gameObject);
    }
    
    
    public void UseConsumable()
    {
        if (!cooldown.IsFinish() || currentCharges == 0)
            return;
        useConsumableEffect.Invoke();
        cooldown.ResetCountdown();
        currentCharges--;
        txtCharges.text = currentCharges.ToString();
    }

    public void RechargeConsumable()
    {
        currentCharges = maximumCharges;
        txtCharges.text = currentCharges.ToString();

    }

    #endregion

   /* public void StartCooldown()
    {
        if (cooldownCountdown != cooldown) { return; }
        cooldownCountdown = Mathf.Clamp(cooldownCountdown -= Time.deltaTime, 0, cooldown);
        imgCooldown.fillAmount = cooldownCountdown / cooldown;
    }*/


    #endregion
}

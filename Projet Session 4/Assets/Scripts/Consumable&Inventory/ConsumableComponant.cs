using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ConsumableComponant : MonoBehaviour
{
    #region Variables & Attributs
    [Header("Consumable Properties")]
    protected Player caster = null;
    [SerializeField] private GameObject consumable = null;
    private int currentCharges = 0;
    [SerializeField] private int maximumCharges = 0;
    [SerializeField] protected Text txtCharges = null;
    [SerializeField] private float cooldown = 0;
    [SerializeField] private Image imgCooldown = null;
    [SerializeField] private int manaCost = 0;
    private float cooldownCountdown = 0;
    private Transform dropPosition = null;

    [Header("Effect")]
    public UnityEvent useConsumableEffect;

    public int CurrentCharges { get => currentCharges; set => currentCharges = value; }
    public float Cooldown { get => cooldown; set => cooldown = value; }
    public float CooldownCountdown { get => cooldownCountdown; set => cooldownCountdown = value; }
    public int MaximumCharges { get => maximumCharges; set => maximumCharges = value; }
    #endregion

    #region Unity's Methods

    void Awake()
    {
        currentCharges = maximumCharges;
        cooldownCountdown = cooldown;
    }

    void Start()
    {
        txtCharges.text = currentCharges.ToString();
        caster = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        dropPosition = caster.transform.GetChild(1).transform;
    }

    void Update()
    {
        StartCooldown();       
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
        clone.GetComponent<Consumable>().AssignRetainedAttributs(cooldown, currentCharges, cooldownCountdown);
        Destroy(gameObject);
    }
    
    
    public void UseConsumable()
    {
        if (cooldownCountdown != cooldown || currentCharges == 0)
            return;
        useConsumableEffect.Invoke();
        cooldownCountdown = 0;
        currentCharges--;
        txtCharges.text = currentCharges.ToString();
    }
    public void UseSkill()
    {
        if (cooldownCountdown != cooldown || caster.Mana.GetCurrentValue() < manaCost)
            return;
            useConsumableEffect.Invoke();
            cooldownCountdown = 0;
        caster.Mana.DecreaseCurrentValue(manaCost);
    }
    public void RechargeConsumable()
    {
        currentCharges = maximumCharges;
        txtCharges.text = currentCharges.ToString();

    }

    #endregion

    public void StartCooldown()
    {
        if (cooldownCountdown == cooldown) { return; }
        cooldownCountdown = Mathf.Clamp(cooldownCountdown += Time.deltaTime, 0, cooldown);
        imgCooldown.fillAmount = cooldownCountdown / cooldown;
        if(imgCooldown.fillAmount == 1)
        {
            imgCooldown.fillAmount = 0;
        }
    }


    #endregion
}

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
    [SerializeField] private int charges = 0;
    [SerializeField] protected Text txtCharges = null;
    [SerializeField] private float cooldown = 0;
    [SerializeField] private Image imgCooldown = null;
    private float cooldownCountdown = 0;

    [Header("Effect")]
    public UnityEvent useConsumableEffect;

    public int Charges { get => charges; set => charges = value; }
    public float Cooldown { get => cooldown; set => cooldown = value; }
    public float CooldownCountdown { get => cooldownCountdown; set => cooldownCountdown = value; }
    #endregion

    #region Unity's Methods

    void Awake()
    {
        cooldownCountdown = cooldown;
    }

    void Start()
    {
        txtCharges.text = charges.ToString();
        caster = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        StartCooldown();       
        /*if (Input.GetKeyDown(KeyCode.Q))
        {
            UseConsumable();
        }*/
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
        Vector3 playerPosition = new Vector3(caster.transform.position.x, caster.transform.position.y, caster.transform.position.z + 3);
        GameObject clone = Instantiate(consumable, playerPosition, Quaternion.identity);
        clone.GetComponent<Consumable>().AssignRetainedAttributs(cooldown, charges, cooldownCountdown);
        Destroy(gameObject);
    }
    // ETRE CAPABLE DE ECHANGER
    public void UseConsumable()
    {
        if (cooldownCountdown != cooldown || charges == 0)
            return;
        useConsumableEffect.Invoke();
        cooldownCountdown = 0;
        charges--;
        txtCharges.text = charges.ToString();
    }
    public void RechargeConsumable()
    {
        charges++;
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

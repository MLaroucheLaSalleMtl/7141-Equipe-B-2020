using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : Actor
{
    #region Variables & Attributs

    #region Movement & Rotation
    Vector3 movementDirection;
    private int floorMask;
    private Rigidbody playerRigidBody;
    private float cameraRange = 100f;
    #endregion

    #region Level System
    [Header("Level Componant")]
    [SerializeField] private float experienceCurrent = 0f;
    [SerializeField] private float experienceMaximum = 100f;
    [SerializeField] private float experienceRatio = 1f;
    [SerializeField] private float leveldifference = 1.15f;
    [SerializeField] private int skillPoints = 0;
    [SerializeField] private int characteristicsPoints = 0;
    [SerializeField] private GameObject LevelUpOnPlayer = null;
    [SerializeField] private GameObject CharacteristicPointsScreen = null;
    [SerializeField] private GameObject SkillPointsScreen = null;

    public int CharacteristicsPoints { get => characteristicsPoints; set => characteristicsPoints = value; }
    public float ExperienceCurrent { get => experienceCurrent; set => experienceCurrent = value; }
    public float ExperienceMaximum { get => experienceMaximum; set => experienceMaximum = value; }
    public int SkillPoints { get => skillPoints; set => skillPoints = value; }
    #endregion

    #region Characteristics System
    [Header("Characteristic Componant")]
    [SerializeField] private Stat power = null;
    [SerializeField] private Stat vigilance = null;
    [SerializeField] private Stat mind = null;
    [SerializeField] private Stat resilience = null;
    [SerializeField] private Stat willPower = null;

    public Stat Power { get => power; set => power = value; }
    public Stat Vigilance { get => vigilance; set => vigilance = value; }
    public Stat Mind { get => mind; set => mind = value; }
    public Stat Resilience { get => resilience; set => resilience = value; }
    public Stat WillPower { get => willPower; set => willPower = value; }
    #endregion

    #region Interaction
    private Transform target;
  //  private float range = 2f;
    [HideInInspector] public bool canInteract = false;
    #endregion

    #region Skill
    [SerializeField] private GameObject DamageBoostIcon = null;
    [SerializeField] private GameObject FortitudeBoostIcon = null;
    #endregion

    #region Gold
    [SerializeField] private int gold = 100;
    public int Gold { get => gold; set => gold = value; }
    #endregion


    #endregion

    #region Unity's Methods
    void Start()
    {
        //InvokeRepeating("UpdateInteraction", 0f, 0.5f);
        DamageBoostIcon.SetActive(false);
        FortitudeBoostIcon.SetActive(false);
        floorMask = LayerMask.GetMask("Ground");
        playerRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (skillPoints > 0)
            SkillPointsScreen.SetActive(true);
        else
            SkillPointsScreen.SetActive(false);

        if (characteristicsPoints > 0)
            CharacteristicPointsScreen.SetActive(true);
        else
            CharacteristicPointsScreen.SetActive(false);

        if (skillPoints > 0 || characteristicsPoints > 0)
            LevelUpOnPlayer.SetActive(true);
        else
            LevelUpOnPlayer.SetActive(false);

        UpdateExperience();
        HealthCurrent = Regeneration(HealthCurrent, HealthMaximum, HealthRegenRatio);
        ManaCurrent = Regeneration(ManaCurrent, ManaMaximum, ManaRegenRatio);
        DashCurrent = Regeneration(DashCurrent, DashMaximum, DashRegenRatio);
        StartDashCooldown();
        StartDamageImmunityCooldown();
        Death();
        movementDirection.x = Input.GetAxisRaw("Horizontal");
        movementDirection.z = Input.GetAxisRaw("Vertical");
    }
    private void FixedUpdate()
    {
        if (CanMove)
        {
            playerRigidBody.MovePosition(playerRigidBody.position + (movementDirection * MovementSpeed.GetValue()) * DashSpeed * Time.fixedDeltaTime);
        }
    }
    #endregion

    #region Methods

    #region Characteristics

    public void IncreasePower()
    {
       
        if(characteristicsPoints > 0)
        {
            if(power.GetValue() != 0)
            {
                PowerPhysical.RemoveModifier(2.5f * power.GetValue());
                HealthRegenRatio.RemoveModifier(0.05f * power.GetValue());
                CriticalDamage.RemoveModifier(0.05f * power.GetValue());
                MovementSpeed.RemoveModifier(0.05f * power.GetValue());
            }
            characteristicsPoints--;
            power.IncreaseCharacteristic();

            PowerPhysical.AddModifier(2.5f * power.GetValue());
            HealthRegenRatio.AddModifier(0.05f * power.GetValue());
            CriticalDamage.AddModifier(0.05f * power.GetValue());
            MovementSpeed.AddModifier(0.05f * power.GetValue());
        }
    }
    public void IncreaseVigilance()
    {

        if (characteristicsPoints > 0)
        {
            if (vigilance.GetValue() != 0)
            {
                AttackSpeed.RemoveModifier(-0.005f * vigilance.GetValue());
                Evasion.RemoveModifier(1f * vigilance.GetValue());
                CriticalChance.RemoveModifier(0.5f * vigilance.GetValue());
                DashRegenRatio.RemoveModifier(0.05f * vigilance.GetValue());

            }
            characteristicsPoints--;
            vigilance.IncreaseCharacteristic();


            AttackSpeed.AddModifier(-0.005f * vigilance.GetValue());
            Evasion.AddModifier(1f * vigilance.GetValue());
            CriticalChance.AddModifier(0.5f * vigilance.GetValue());
            DashRegenRatio.AddModifier(0.05f * vigilance.GetValue());
        }
    }
    public void IncreaseMind()
    {

        if (characteristicsPoints > 0)
        {
            if (mind.GetValue() != 0)
            {
                PowerMagical.RemoveModifier(5 * mind.GetValue());
                BarrierMaximum.RemoveModifier(10f * mind.GetValue());
                DamagePenetration.RemoveModifier(1f * mind.GetValue());
                ManaMaximum.RemoveModifier(5f * mind.GetValue());

            }
            characteristicsPoints--;
            mind.IncreaseCharacteristic();

            PowerMagical.AddModifier(5 * mind.GetValue());
            BarrierMaximum.AddModifier(10f * mind.GetValue());
            DamagePenetration.AddModifier(1f * mind.GetValue());
            ManaMaximum.AddModifier(5f * mind.GetValue());
        }
    }
    public void IncreaseResilience()
    {

        if (characteristicsPoints > 0)
        {
            if (resilience.GetValue() != 0)
            {
                DamageThorn.RemoveModifier(5f * resilience.GetValue());
                ResistancePhysical.RemoveModifier(0.5f * resilience.GetValue());
                HealthMaximum.RemoveModifier(5f * resilience.GetValue());
                ResistanceDamage.RemoveModifier(0.2f * resilience.GetValue());

            }
            characteristicsPoints--;
            resilience.IncreaseCharacteristic();

            DamageThorn.AddModifier(5f * resilience.GetValue());
            ResistancePhysical.AddModifier(0.5f * resilience.GetValue());
            HealthMaximum.AddModifier(5f * resilience.GetValue());
            ResistanceDamage.AddModifier(0.2f * resilience.GetValue());
        }
    }
    public void IncreaseWillpower()
    {

        if (characteristicsPoints > 0)
        {
            if (willPower.GetValue() != 0)
            {
                CooldownReduction.RemoveModifier(-0.01f * willPower.GetValue());
                ResistanceMagical.RemoveModifier(1f * willPower.GetValue());
                ManaRegenRatio.RemoveModifier(0.05f * willPower.GetValue());
                BonusBuffRatio.RemoveModifier(0.02f * willPower.GetValue());

            }
            characteristicsPoints--;
            willPower.IncreaseCharacteristic();

            CooldownReduction.AddModifier(-0.01f * willPower.GetValue());
            ResistanceMagical.AddModifier(1f * willPower.GetValue());
            ManaRegenRatio.AddModifier(0.05f * willPower.GetValue());
            BonusBuffRatio.AddModifier(0.02f * willPower.GetValue());
        }
    }



    #endregion

    #region Level & Experience

    public void GainExperience(float Amount)
    {
        experienceCurrent += Amount * experienceRatio;
    }

    private void UpdateExperience()
    {
        if(experienceCurrent >= experienceMaximum)
        {
            experienceCurrent = 0;
            experienceMaximum *= leveldifference;
            skillPoints++;
            characteristicsPoints += 3;
            LevelUp();

        }
    }
    #endregion

    #region Input and Control
    public void Movement(InputAction.CallbackContext context)
    {
        movementDirection = context.ReadValue<Vector2>();
    }
    public void Look(InputAction.CallbackContext context)
    {
        Rotation();
    }
    public void Dash(InputAction.CallbackContext context)
    {
        if (context.started && DashCountdown >= DashCooldown.GetValue() && DashCurrent >= 1f)
        {
            StartCoroutine(UseDash());
        }
    }
    public void UseItem(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if(GetComponent<InventoryComponant>().slot.GetComponentInChildren<ConsumableComponant>() != null)
            GetComponent<InventoryComponant>().slot.GetComponentInChildren<ConsumableComponant>().UseConsumable();
        }

    }
    public void Interaction(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            StartCoroutine(Interaction());
        }
    }
    public void Skill1(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (GetComponent<InventorySkill>().slots[0].GetComponentInChildren<ConsumableComponant>() != null)
                GetComponent<InventorySkill>().slots[0].GetComponentInChildren<ConsumableComponant>().UseSkill();
        }
    }
    public void Skill2(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (GetComponent<InventorySkill>().slots[1].GetComponentInChildren<ConsumableComponant>() != null)
                GetComponent<InventorySkill>().slots[1].GetComponentInChildren<ConsumableComponant>().UseSkill();
        }
    }
    public void Skill3(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (GetComponent<InventorySkill>().slots[2].GetComponentInChildren<ConsumableComponant>() != null)
                GetComponent<InventorySkill>().slots[2].GetComponentInChildren<ConsumableComponant>().UseSkill();
        }
    }
    public void Skill4(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (GetComponent<InventorySkill>().slots[3].GetComponentInChildren<ConsumableComponant>() != null)
                GetComponent<InventorySkill>().slots[3].GetComponentInChildren<ConsumableComponant>().UseSkill();
        }
    }



    #endregion
    protected override void Rotation() // Unity tutorial sur top down shooter ( voir documentation ) ** Aller revoir ** CREDIT : UNITY TUTORIAL
    {
        // Create a ray from the mouse cursor on screen in the direction of the camera.
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Create a RaycastHit variable to store information about what was hit by the ray.
        RaycastHit floorHit;

        // Perform the raycast and if it hits something on the floor layer...
        if (Physics.Raycast(camRay, out floorHit, cameraRange, floorMask))
        {
            // Create a vector from the player to the point on the floor the raycast from the mouse hit.
            Vector3 playerToMouse = floorHit.point - transform.position;
            // Ensure the vector is entirely along the floor plane.
            playerToMouse.y = 0f;

            // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

            // Set the player's rotation to this new rotation.
            playerRigidBody.MoveRotation(newRotation);
        }
    }
    public void RotationController()
    {
        Vector3 lookDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        transform.rotation = Quaternion.LookRotation(lookDirection);
    }

    protected override void Death() { }
    #endregion

    public IEnumerator TemporaryBoost(Stat stat, float Duration, float Value, float Cooldown, GameObject boostImage)
    {
        stat.AddModifier(Value);
        boostImage.SetActive(true);
        yield return new WaitForSeconds(Duration * BonusBuffRatio.GetValue());
        stat.RemoveModifier(Value);
        boostImage.SetActive(false);
        yield return new WaitForSeconds(Cooldown * CooldownReduction.GetValue());
    }
    public IEnumerator TemporaryDebuff(Stat stat, float Duration, float Value)
    {
        stat.AddModifier(Value);
        yield return new WaitForSeconds(Duration);
        stat.RemoveModifier(Value);
    }
    public void PermanentBoost(Stat stat, float Value)
    {
        stat.AddModifier(Value);
    }

    public IEnumerator Interaction()
    {
        canInteract = true;
        yield return new WaitForSeconds(0.05f);
        canInteract = false;
    }

    public void IncreaseGold(int Amount)
    {
        gold += Amount;
    }

    public void DecreaseGold(int Amount)
    {
        gold -= Amount;
    }
}

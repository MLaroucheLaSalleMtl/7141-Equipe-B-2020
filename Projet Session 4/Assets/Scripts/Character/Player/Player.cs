using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : Actor
{
    #region Variables & Attributs

    #region Movement Properties
    private Vector3 movementDirection;
    private int floorMask;
    private float cameraRange = 100f;
    #endregion

    #region Level Properties
    [Header(" - - Level Properties - - ")]
    [SerializeField] private int skillPoints = 0;
    [SerializeField] private int characteristicsPoints = 0;
    public int levelCurrent = 1;
    private float experienceCurrent = 0f;
    private float experienceMaximum = 100f;
    private float experienceRatio = 1f;
    private float leveldifference = 1.15f;
    [SerializeField] private GameObject LevelUpOnPlayer = null;
    [SerializeField] private GameObject CharacteristicPointsScreen = null;
    [SerializeField] private GameObject SkillPointsScreen = null;

    public int LevelCurrent { get => levelCurrent; set => levelCurrent = value; }
    public int CharacteristicsPoints { get => characteristicsPoints; set => characteristicsPoints = value; }
    public float ExperienceCurrent { get => experienceCurrent; set => experienceCurrent = value; }
    public float ExperienceMaximum { get => experienceMaximum; set => experienceMaximum = value; }
    public int SkillPoints { get => skillPoints; set => skillPoints = value; }
    public float ExperienceRatio { get => experienceRatio; set => experienceRatio = value; }
    #endregion

    #region Characteristics Properties
    [Header(" - - Characteristics Properties - - ")]
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
    [HideInInspector] public bool isInteracting = false;
    #endregion

    #region Skill

    #endregion

    #endregion

    #region Unity's Methods
    void Start()
    {
        floorMask = LayerMask.GetMask("Ground");
    }
    protected override void Update()
    {
        base.Update();

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



        Death();
        movementDirection.x = Input.GetAxisRaw("Horizontal");
        movementDirection.z = Input.GetAxisRaw("Vertical");
    }
    private void FixedUpdate()
    {
        if (CanMove)
        {
            rig.MovePosition(rig.position + (movementDirection * MovementSpeed.GetBaseValue()) * DashSpeed * Time.fixedDeltaTime);
        }
    }
    #endregion

    #region Methods

    #region Characteristics

    public void IncreasePower()
    {
       
        if(characteristicsPoints > 0)
        {
            if(power.GetBaseValue() != 0)
            {
                PowerPhysical.RemoveModifier(2.5f * power.GetBaseValue());
                Health.recoveryValue.RemoveModifier(0.05f * power.GetBaseValue());
                CriticalDamage.RemoveModifier(0.05f * power.GetBaseValue());
                MovementSpeed.RemoveModifier(0.05f * power.GetBaseValue());
            }
            characteristicsPoints--;
            power.AddModifier(1);

            PowerPhysical.AddModifier(2.5f * power.GetBaseValue());
            Health.recoveryValue.AddModifier(0.05f * power.GetBaseValue());
            CriticalDamage.AddModifier(0.05f * power.GetBaseValue());
            MovementSpeed.AddModifier(0.05f * power.GetBaseValue());
        }
    }
    public void IncreaseVigilance()
    {

        if (characteristicsPoints > 0)
        {
            if (vigilance.GetBaseValue() != 0)
            {
                AttackSpeed.RemoveModifier(-0.005f * vigilance.GetBaseValue());
                Evasion.RemoveModifier(1f * vigilance.GetBaseValue());
                CriticalChance.RemoveModifier(0.5f * vigilance.GetBaseValue());
                Dash.recoveryValue.RemoveModifier(0.05f * vigilance.GetBaseValue());

            }
            characteristicsPoints--;
            vigilance.AddModifier(1);



            AttackSpeed.AddModifier(-0.005f * vigilance.GetBaseValue());
            Evasion.AddModifier(1f * vigilance.GetBaseValue());
            CriticalChance.AddModifier(0.5f * vigilance.GetBaseValue());
            Dash.recoveryValue.AddModifier(0.05f * vigilance.GetBaseValue());
        }
    }
    public void IncreaseMind()
    {

        if (characteristicsPoints > 0)
        {
            if (mind.GetBaseValue() != 0)
            {
                PowerMagical.RemoveModifier(5 * mind.GetBaseValue());
                Barrier.RemoveModifier(10f * mind.GetBaseValue());
                DamagePenetration.RemoveModifier(1f * mind.GetBaseValue());
                Mana.RemoveModifier(5f * mind.GetBaseValue());

            }
            characteristicsPoints--;
            mind.AddModifier(1);


            PowerMagical.AddModifier(5 * mind.GetBaseValue());
            Barrier.AddModifier(10f * mind.GetBaseValue());
            DamagePenetration.AddModifier(1f * mind.GetBaseValue());
            Mana.AddModifier(5f * mind.GetBaseValue());
        }
    }
    public void IncreaseResilience()
    {

        if (characteristicsPoints > 0)
        {
            if (resilience.GetBaseValue() != 0)
            {
                PowerThorn.RemoveModifier(5f * resilience.GetBaseValue());
                ResistancePhysical.RemoveModifier(0.5f * resilience.GetBaseValue());
                Health.RemoveModifier(5f * resilience.GetBaseValue());
                ResistanceDamage.RemoveModifier(0.2f * resilience.GetBaseValue());

            }
            characteristicsPoints--;
            resilience.AddModifier(1);


            PowerThorn.AddModifier(5f * resilience.GetBaseValue());
            ResistancePhysical.AddModifier(0.5f * resilience.GetBaseValue());
            Health.AddModifier(5f * resilience.GetBaseValue());
            ResistanceDamage.AddModifier(0.2f * resilience.GetBaseValue());
        }
    }
    public void IncreaseWillpower()
    {

        if (characteristicsPoints > 0)
        {
            if (willPower.GetBaseValue() != 0)
            {
                CooldownReduction.RemoveModifier(-0.01f * willPower.GetBaseValue());
                ResistanceMagical.RemoveModifier(1f * willPower.GetBaseValue());
                Mana.recoveryValue.RemoveModifier(0.05f * willPower.GetBaseValue());
                BuffEnhancement.RemoveModifier(0.02f * willPower.GetBaseValue());

            }
            characteristicsPoints--;
            willPower.AddModifier(1);


            CooldownReduction.AddModifier(-0.01f * willPower.GetBaseValue());
            ResistanceMagical.AddModifier(1f * willPower.GetBaseValue());
            Mana.recoveryValue.AddModifier(0.05f * willPower.GetBaseValue());
            BuffEnhancement.AddModifier(0.02f * willPower.GetBaseValue());
        }
    }
    #endregion

    #region Level & Experience
    public void LevelUp()
    {
        skillPoints++;
        characteristicsPoints += 3;
        levelCurrent++;
    }
    public void IncreaseExperience(float Amount)
    {
        experienceCurrent += (Amount * ExperienceRatio);

        if (experienceCurrent >= experienceMaximum)
        {
            experienceCurrent = 0;
            experienceMaximum *= leveldifference;
            LevelUp();
        }
    }

    #endregion

    #region Input and Control
    public void Fire(InputAction.CallbackContext context)
    {

        if (context.started && CanAttack == true)
        {
                StartCoroutine(AttackRootEffect(0.15f));
            UseBasicAttack();        
        }
    }
    public void Movement(InputAction.CallbackContext context)
    {
        movementDirection = context.ReadValue<Vector2>();
    }
    public void Look(InputAction.CallbackContext context)
    {
        Rotation();
    }
    public void UseDash(InputAction.CallbackContext context)
    {
        if (context.started && DashCooldown.IsFinish() && Dash.GetCurrentValue() >= 1f && canDash == true)
        {

            StartCoroutine(ActivateDash());
            DashCooldown.ResetCountdown();
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
    public void Skill1(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (GetComponent<InventorySkill>().slots[0].GetComponentInChildren<SkillComponant>() != null)
                GetComponent<InventorySkill>().slots[0].GetComponentInChildren<SkillComponant>().UseSkill();
        }
    }
    public void Skill2(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (GetComponent<InventorySkill>().slots[1].GetComponentInChildren<SkillComponant>() != null)
                GetComponent<InventorySkill>().slots[1].GetComponentInChildren<SkillComponant>().UseSkill();
        }
    }
    public void Skill3(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (GetComponent<InventorySkill>().slots[2].GetComponentInChildren<SkillComponant>() != null)
                GetComponent<InventorySkill>().slots[2].GetComponentInChildren<SkillComponant>().UseSkill();
        }
    }
    public void Skill4(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (GetComponent<InventorySkill>().slots[3].GetComponentInChildren<SkillComponant>() != null)
                GetComponent<InventorySkill>().slots[3].GetComponentInChildren<SkillComponant>().UseSkill();
        }
    }
    public void Interaction(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            StartCoroutine(Interaction());
        }
    }
    public IEnumerator Interaction()
    {
        isInteracting = true;
        yield return new WaitForSeconds(0.05f);
        isInteracting = false;
    }



    #endregion

    #region Movement & Rotation
    private void Rotation() // Unity tutorial sur top down shooter ( voir documentation ) ** Aller revoir ** CREDIT : UNITY TUTORIAL
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
            rig.MoveRotation(newRotation);
        }
    }
    public void RotationController()
    {
        Vector3 lookDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        transform.rotation = Quaternion.LookRotation(lookDirection);
    }

    #endregion

    private void Death() { }

    #endregion



}

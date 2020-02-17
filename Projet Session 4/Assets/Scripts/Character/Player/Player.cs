using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    public int CharacteristicsPoints { get => characteristicsPoints; set => characteristicsPoints = value; }
    #endregion

    #region Characteristics System
    [Header("Characteristic Componant")]
    [SerializeField] private int power = 0;
    [SerializeField] private int vigilance = 0;
    [SerializeField] private int mind = 0;
    [SerializeField] private int resilience = 0;
    [SerializeField] private int willPower = 0;

    public int Power { get => power; set => power = value; }
    public int Vigilance { get => vigilance; set => vigilance = value; }
    public int Mind { get => mind; set => mind = value; }
    public int Resilience { get => resilience; set => resilience = value; }
    public int WillPower { get => willPower; set => willPower = value; }
    #endregion

    #endregion

    #region Unity's Methods
    void Start()
    {
        floorMask = LayerMask.GetMask("Ground");
        playerRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            experienceCurrent = experienceMaximum;
            print("Level :" + LevelCurrent);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            IncreasePower();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            IncreaseVigilance();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            IncreaseMind();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            IncreaseResilience();
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            IncreaseWillpower();
        }
        UpdateExperience();
        HealthCurrent = Regeneration(HealthCurrent, HealthMaximum, HealthRegenRatio);
        ManaCurrent = Regeneration(ManaCurrent, ManaMaximum, ManaRegenRatio);
        DashCurrent = Regeneration(DashCurrent, DashMaximum, DashRegenRatio);
        StartDashCooldown();
        Death();
        movementDirection.x = Input.GetAxisRaw("Horizontal");
        movementDirection.z = Input.GetAxisRaw("Vertical");
    }
    private void FixedUpdate()
    {
        if (CanMove)
        {
            playerRigidBody.MovePosition(playerRigidBody.position + (movementDirection * MovementSpeed.GetValue()) * DashSpeed * Time.fixedDeltaTime);
            Rotation();
        }
    }
    #endregion

    #region Methods

    #region Characteristics

    public void IncreasePower()
    {
       
        if(characteristicsPoints > 0)
        {
            if(power != 0)
            {
                PowerPhysical.RemoveModifier(5 * power);
                HealthRegenRatio.RemoveModifier(0.1f * power);
                CriticalDamage.RemoveModifier(0.05f * power);
                MovementSpeed.RemoveModifier(0.1f * power);

            }
            characteristicsPoints--;
            power++;

            PowerPhysical.AddModifier(5 * power);
            HealthRegenRatio.AddModifier(0.1f * power);
            CriticalDamage.AddModifier(0.05f * power);
            MovementSpeed.AddModifier(0.1f * power);
        }
    }
    public void IncreaseVigilance()
    {

        if (characteristicsPoints > 0)
        {
            if (vigilance != 0)
            {
                AttackSpeed.RemoveModifier(-0.01f * vigilance);
                Evasion.RemoveModifier(1f * vigilance);
                CriticalChance.RemoveModifier(1f * vigilance);
                DashRegenRatio.RemoveModifier(0.05f * vigilance);

            }
            characteristicsPoints--;
            vigilance++;

            AttackSpeed.AddModifier(-0.01f * vigilance);
            Evasion.AddModifier(1f * vigilance);
            CriticalChance.AddModifier(1f * vigilance);
            DashRegenRatio.AddModifier(0.05f * vigilance);
        }
    }
    public void IncreaseMind()
    {

        if (characteristicsPoints > 0)
        {
            if (mind != 0)
            {
                PowerMagical.RemoveModifier(5 * mind);
                BarrierMaximum.RemoveModifier(5f * mind);
                DamagePenetration.RemoveModifier(1f * mind);
                ManaMaximum.RemoveModifier(5f * mind);

            }
            characteristicsPoints--;
            mind++;

            PowerMagical.AddModifier(5 * mind);
            BarrierMaximum.AddModifier(5f * mind);
            DamagePenetration.AddModifier(1f * mind);
            ManaMaximum.AddModifier(5f * mind);
        }
    }
    public void IncreaseResilience()
    {

        if (characteristicsPoints > 0)
        {
            if (resilience != 0)
            {
                DamageThorn.RemoveModifier(5f * resilience);
                ResistancePhysical.RemoveModifier(1f * resilience);
                HealthMaximum.RemoveModifier(5f * resilience);
                ResistanceDamage.RemoveModifier(0.2f * resilience);

            }
            characteristicsPoints--;
            resilience++;

            DamageThorn.AddModifier(5f * resilience);
            ResistancePhysical.AddModifier(1f * resilience);
            HealthMaximum.AddModifier(5f * resilience);
            ResistanceDamage.AddModifier(0.2f * resilience);
        }
    }
    public void IncreaseWillpower()
    {

        if (characteristicsPoints > 0)
        {
            if (willPower != 0)
            {
                CooldownReduction.RemoveModifier(-0.01f * willPower);
                ResistanceMagical.RemoveModifier(1f * willPower);
                ManaRegenRatio.RemoveModifier(0.1f * willPower);
                BonusBuffRatio.RemoveModifier(1.5f * willPower);

            }
            characteristicsPoints--;
            willPower++;

            CooldownReduction.AddModifier(-0.01f * willPower);
            ResistanceMagical.AddModifier(1f * willPower);
            ManaRegenRatio.AddModifier(0.1f * willPower);
            BonusBuffRatio.AddModifier(1.5f * willPower);
        }
    }



    #endregion

    #region Level & Experience

    public void GainExperience(float Amount)
    {
        experienceCurrent += Amount;
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

    public void Dash(InputAction.CallbackContext context)
    {
        if (context.started && DashCountdown >= DashCooldown.GetValue() && DashCurrent >= 1f)
        {
            StartCoroutine(UseDash());
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
    protected override void Death() { if (HealthCurrent <= 0) Destroy(gameObject); }
    #endregion

}

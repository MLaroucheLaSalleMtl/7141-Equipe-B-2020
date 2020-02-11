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

    protected override void Death() { if (HealthCurrent <= 0) Destroy(gameObject); }
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
    #endregion

}

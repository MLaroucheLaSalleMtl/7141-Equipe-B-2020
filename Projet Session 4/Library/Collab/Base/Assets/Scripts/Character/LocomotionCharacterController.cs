using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocomotionCharacterController : MonoBehaviour
{
    CharacterController characterController;

    [SerializeField] private float speed = 10f;
    [SerializeField] private float dashSpeed = 1f;
    [SerializeField] private float dashTime = 0f;
    private Rigidbody playerRigidBody;
    private float cameraRange = 100f;
    private int floorMask;

    private Vector3 moveDirection = Vector3.zero;
    private Animator anim;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        //floorMask = LayerMask.GetMask("Floor");
        //playerRigidBody = GetComponent<Rigidbody>();
    }


    void Update()
    {
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        Dash();
        moveDirection *= (speed * dashSpeed);

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);
        //PlayerRotation();
    }

    private void Dash()
    {
        if (Input.GetButtonDown("Dash") && dashTime == 0)
        {
            dashTime = 10f;
        }
        
        if (dashTime > 0) /* Cette valeur est multiplier au speed du moveDirection donc lorsqu'elle est plus élevé que 1
            le deplacement du joueur sera plus vite mais pour un court moment pour faire l'effet d'un Dash*/
        {
            dashSpeed = 5f;
            dashTime --;
        }
        else
        {
            dashSpeed = 1f;
        }
    }
    /*private void PlayerRotation() // Unity tutorial sur top down shooter ( voir documentation ) ** Aller revoir ** CREDIT : UNITY TUTORIAL
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

    }*/
}

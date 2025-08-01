using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]

public class SC_FPSController : MonoBehaviour
{
    /*
    
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.9f;
    public float lookXLimit = 45.0f;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0f;

    [HideInInspector]
    public bool canMove = true;
    */
    public Player PlayerSC;         //The main scriptable object that can be changeable
    public Player DefaultPlayerSC;  //Default Player Scriptable Object that the main scriptable object will instantiate from
    CharacterController characterController;
    public Camera playerCamera;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0f;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Player defaults = Instantiate(DefaultPlayerSC);
        PlayerSC = defaults;
        PlayerSC.canMove = true;
        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        print(PlayerSC.stamina);
        //We are grounded, ro recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        //Press Left shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = PlayerSC.canMove ? (isRunning ? PlayerSC.runningSpeed : PlayerSC.walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = PlayerSC.canMove ? (isRunning ? PlayerSC.runningSpeed : PlayerSC.walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);
        
        PlayerSC.isMoving = (moveDirection != new Vector3(0,0,0)) ? true : false;
        if(PlayerSC.isMoving && isRunning)
        {
            if (PlayerSC.stamina > 0)
            {
                PlayerSC.stamina -= Time.deltaTime;
                Debug.Log("Stamina is being used.");
            }
        }
        else
        {
            if (PlayerSC.stamina < 100)
            {
                PlayerSC.stamina += 1 * Time.deltaTime;
            }
        }

        if (Input.GetButton("Jump") && PlayerSC.canMove && characterController.isGrounded)
        {
            moveDirection.y = PlayerSC.jumpSpeed;
        }

        else
        {
            moveDirection.y = movementDirectionY;
        }




        //Apply gravity. Gravity is multiplied by deltaTime twice 
        if (!characterController.isGrounded)
        {
            moveDirection.y -= PlayerSC.gravity * Time.deltaTime;
        }

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        //Player and Camera rotation

        if (PlayerSC.canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * PlayerSC.lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -PlayerSC.lookXLimit, PlayerSC.lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * PlayerSC.lookSpeed, 0);

        }
    }
}

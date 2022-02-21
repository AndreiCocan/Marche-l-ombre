using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyboardPlayerMovement : MonoBehaviour
{
    //Does the player have control of the character?
    public bool CanMove { get; private set; } = true;

    [Header("Movement Parameters")]
    [SerializeField] private float walkSpeed = 3.0f;
    [SerializeField] private float runSpeed = 9.0f;
    private float currentSpeed;

    [Header("Look Parameters")]
    [SerializeField, Range(1, 10)] private float lookSpeedX = 2.0f;
    [SerializeField, Range(1, 10)] private float lookSpeedY = 2.0f;
    [SerializeField, Range(1, 180)] private float upperLookLimit = 80.0f;

    private Camera playerCamera;
    private CharacterController characterController;

    //Keyboard input as a Vector2
    private Vector2 currentInput;

    //Rotation of the camera on the X axis
    private float rotationX = 0;


    void Awake()
    {
        //Get Character controller and the main camera
        playerCamera = GetComponentInChildren<Camera>();
        characterController = GetComponent<CharacterController>();

        //Lock and set invisible the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //Set currentSpeed to default (walk) speed
        currentSpeed = walkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (CanMove)
        {
            ApplyFinalMovement();
        }
    }

    //Retrieve Sprint Input
    public void HandleSprintInput(InputAction.CallbackContext value)
    {
        //If Sprint key pressed
        if (value.ReadValueAsButton())
        {
            currentSpeed = runSpeed;
        }
        else
        {
            currentSpeed = walkSpeed;
        }
    }

    //Retrieve ZQSD Input
    public void HandleMovementInput(InputAction.CallbackContext value) 
    {
        currentInput = value.ReadValue<Vector2>();
    }

    //Retrieve Mouse Pointer Input then rotate camera on X axis and character on Y axis
    public void HandleMouseLook(InputAction.CallbackContext value)
    {
        //Retrieve Y axis Mouse Pointer Input
        rotationX -= value.ReadValue<Vector2>().y * lookSpeedY;

        //Set rotation limit
        rotationX = Mathf.Clamp(rotationX, -upperLookLimit, upperLookLimit);

        //Camera rotation on X axis
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);


        //Retrieve X axis Mouse Pointer Input and rotate character on Y axis
        transform.rotation *= Quaternion.Euler(0, value.ReadValue<Vector2>().x * lookSpeedX, 0);

    }

    private void ApplyFinalMovement() 
    {
        //Move character based on currentInput
        characterController.Move(((transform.TransformDirection(Vector3.forward) * currentInput.y) + (transform.TransformDirection(Vector3.right)) * currentInput.x) * Time.deltaTime * currentSpeed);

    }

}

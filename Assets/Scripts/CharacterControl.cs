using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;
    public float jumpHeight = 3f;
    private float normalSpeed = 7.5f;
    private float crouchSpeed = 4.0f;

    private Vector3 crouchScale = new Vector3(1, 0.5f, 1);
    private Vector3 playerScale = new Vector3(1, 1, 1);

    [HideInInspector]
    public Vector2 RunAxis;
    [HideInInspector]
    public Vector2 LookAxis;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    [HideInInspector]
    public bool canMove = true;

    [Header("ButtonAssign")]
    public GameObject walk_Btn;
    public GameObject jump_Button;
    public GameObject crouch_Button;
    public GameObject stand_button;

    private Footsteps footsteps;
    private float walk_volum_min = 0.2f;
    private float walk_volum_max = 0.6f;
    private float walk_step_distance = 0.4f;

    private void Awake()
    {
        footsteps = GetComponentInChildren<Footsteps>();
    }


    void Start()
    {
        characterController = GetComponent<CharacterController>();

        footsteps.Volum_max = walk_volum_max;
        footsteps.Volum_min = walk_volum_min;
        footsteps.step_Distance = walk_step_distance;
    }

    void Update()
    {      
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * RunAxis.y : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * RunAxis.x : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);
        moveDirection.y = movementDirectionY;
        
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
            jump_Button.SetActive(false);
        }
        else
        {
            jump_Button.SetActive(true);
        }
       
        characterController.Move(moveDirection * Time.deltaTime);

        
        if (canMove)
        {
            rotationX += -LookAxis.y * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, LookAxis.x * lookSpeed, 0);
        }
    }

    public void Jump()
    {
        moveDirection.y = jumpHeight;
    }

    public void Run()
    {
        walkingSpeed = runningSpeed;
        walk_Btn.SetActive(true);
    }

    public void Walk()
    {
        walkingSpeed = normalSpeed;
        walk_Btn.SetActive(false);
    }

    public void Crouch()
    {
        transform.localScale = crouchScale;
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
        walkingSpeed = crouchSpeed;
        stand_button.SetActive(true);
        crouch_Button.SetActive(false);
    }

    public void Stand()
    {
        transform.localScale = playerScale;
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        walkingSpeed = crouchSpeed;
        stand_button.SetActive(false);
        crouch_Button.SetActive(true);
    }
}

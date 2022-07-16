using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour

{
    private Rigidbody rb;
    private Vector3 moveDirection;
    private Vector3 gravityVector = new Vector3(0, -50f, 0);
    [SerializeField] private Transform orientation;

    [Header("Movement")]
    public float moveSpeed = 4f; 
    float movementMultiplier = 10f;

    [Header("Sprinting")]
    [SerializeField] float walkspeed = 4f;
    [SerializeField] float sprintSpeed = 6f;
    [SerializeField] float acceleration = 10f;

    
    [Header("Jumping")]
    [SerializeField] private float jumpForce = 5f;
    private bool isJumping = false;
    private bool isJumpOnCooldown = false;
    private float jumpDelay = 0f;

    [Header("Keybinds")]
    private KeyCode JUMP_KEY = KeyCode.Space;
    private KeyCode SPRINT_KEY = KeyCode.LeftShift;

    [Header("Drag")]
    [SerializeField]float groundDrag = 6f;
    [SerializeField]float airDrag = 2f;

    float horizontalMovement;
    float verticalMovement;

    [Header("Ground Detection")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float groundDistance = 0.2f;
    private bool isGrounded;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update() {
        CurrentState();
        ControlMovement();
        ControlSpeed();
        ControlDrag();
        ControlJump();
    }

    private void FixedUpdate() {
        UpdatePlayerHorizontalMovement();
        UpdatePlayerVerticalMovement();
    }
    
    private void CurrentState() {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }

    private void ControlMovement() {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");
        moveDirection = orientation.forward * verticalMovement + orientation.right * horizontalMovement;
    }

    private void ControlSpeed() {
        if (Input.GetKey(SPRINT_KEY)) {
            moveSpeed = Mathf.Lerp(moveSpeed, sprintSpeed, acceleration * Time.deltaTime);
        } else {
            moveSpeed = Mathf.Lerp(moveSpeed, walkspeed, acceleration * Time.deltaTime);
        }
    }

    private void ControlDrag() {
        if(isGrounded) {
            rb.drag = groundDrag;
        } else {
            rb.drag = airDrag;
        }
    }

    private void ControlJump() {
        if (Input.GetKey(JUMP_KEY) && isGrounded && !isJumpOnCooldown) {
            isJumping = true;
        } else if (isGrounded && isJumpOnCooldown) {
            CheckJumpCooldown();
        }
    }


    private void CheckJumpCooldown() {
        if (jumpDelay < 5f) {
            jumpDelay++;
        } else {
            isJumpOnCooldown = false;
            jumpDelay = 0f;
        }
    }

    private void UpdatePlayerHorizontalMovement() {
        if(isGrounded){
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        } else {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier * (airDrag/groundDrag), ForceMode.Acceleration);
        }
    }

    private void UpdatePlayerVerticalMovement() {
        if (!isJumpOnCooldown && isJumping) {
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            isJumping = false;
            isJumpOnCooldown = true;
        } else if(!isGrounded) {
            rb.AddForce(gravityVector, ForceMode.Acceleration);
        }
    }
}
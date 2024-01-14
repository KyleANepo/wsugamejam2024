using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.Experimental.GraphView;
using Unity.VisualScripting;
using UnityEngine.TextCore.Text;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    private float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;
    public float slideSpeed;
    public float climbSpeed;
    public float iceSpeedMultiplier;

    private float desiredMoveSpeed;
    private float lastDesiredMoveSpeed;

    //Makes the player go faster when going down steeper slopes
    public float speedIncreaseMultiplier;
    public float slopeIncreaseMultiplier;

    public float groundDrag;
    public float iceGroundDrag;
    private float savedGroundDrag;

    [Header("Jumping")]
    public float jumpForce;
    public float jumpForceBoost = 12;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [Header("Crouching")]
    public float crouchSpeed;
    public float crouchYScale;
    private float startYScale;
    public bool crouched;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    public LayerMask whatIsIce;
    public LayerMask whatIsJP;
    public bool grounded;
    bool iced;
    bool jumppad;
    public bool above;
    private bool forcedCrouch;

    [Header("Slope Handling")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    private RaycastHit aboveHit;
    private bool exitingSlope;
    public bool slopeGravityToggle;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    public MovementState state;

    public bool sliding;
    public bool climbing;
    private float startYpos;

    public enum MovementState { 
        walking, sprinting, air, crouching, sliding, climbing
    }


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;

        savedGroundDrag = groundDrag;

        startYpos = transform.position.y;
        startYScale = transform.localScale.y;
    }

    private void Update()
    {
        // ground check
        grounded = (Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround) || OnSlope());
        iced = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsIce);
        jumppad = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsJP);
        above = (Physics.SphereCast(transform.position,0.5f, Vector3.up,out aboveHit, playerHeight * 0.5f, whatIsGround));

        if (iced)
        {
            grounded = true;
            groundDrag = iceGroundDrag;
        }
        else groundDrag = savedGroundDrag;

        if (jumppad)
        {
            Debug.Log("on jumppad");
            jumpForce = jumpForceBoost + 12;
        }
        else jumpForce = 12;

        MyInput();
        SpeedControl();
        StateHandler();

        // handle drag
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // when to jump
        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }

        //Start Crouch
        if (Input.GetKeyDown(crouchKey) || crouched)
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }

        //Stop Crouch
        if (Input.GetKeyUp(crouchKey) || above)
        {
            if (!above)
            {
                transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
                if (transform.position.y < startYpos / 3f && transform.position.y > -1f)
                {
                    EmergencyMove();
                    transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
                }

            }
            else
            {
                EmergencyMove();
            }
        }



    }

    private void StateHandler()
    {


        if (sliding)
        {
            state = MovementState.sliding;
            if (OnSlope() && rb.velocity.y < 0.1f)
            {
                desiredMoveSpeed = slideSpeed;
            }
            else desiredMoveSpeed = sprintSpeed;

        }

        if (Input.GetKey(crouchKey))
        {
            state = MovementState.crouching;
            desiredMoveSpeed = crouchSpeed;

        }
        //climbing
        else if (climbing)
        {
            state = MovementState.climbing;
            desiredMoveSpeed = climbSpeed;
        }

        //Mode = Sprinting
        else if(grounded && Input.GetKey(sprintKey))
        {
            state = MovementState.sprinting;
            desiredMoveSpeed = sprintSpeed;
        }

        else if (grounded)
        {
            state = MovementState.walking;
            desiredMoveSpeed = walkSpeed;
        }

        else
        {
            state = MovementState.air;

        }
        // check if desiredMoveSpeed has changed drastically
        if (Mathf.Abs(desiredMoveSpeed - lastDesiredMoveSpeed) > 4f && moveSpeed != 0)
        {
            StopAllCoroutines();
            StartCoroutine(SmoothlyLerpMoveSpeed());
        }
        else
        {
            moveSpeed = desiredMoveSpeed;
        }
        if (iced)
        {
            desiredMoveSpeed = desiredMoveSpeed * iceSpeedMultiplier;
        }
        lastDesiredMoveSpeed = desiredMoveSpeed;


    }
    private IEnumerator SmoothlyLerpMoveSpeed()
    {
        // smoothly lerp movementSpeed to desired value
        float time = 0;
        float difference = Mathf.Abs(desiredMoveSpeed - moveSpeed);
        float startValue = moveSpeed;

        while (time < difference)
        {
            moveSpeed = Mathf.Lerp(startValue, desiredMoveSpeed, time / difference);

            if (OnSlope())
            {
                float slopeAngle = Vector3.Angle(Vector3.up, slopeHit.normal);
                float slopeAngleIncrease = 1 + (slopeAngle / 90f);

                time += Time.deltaTime * speedIncreaseMultiplier * slopeIncreaseMultiplier * slopeAngleIncrease;
            }
            else
                time += Time.deltaTime * speedIncreaseMultiplier;

            yield return null;
        }

        moveSpeed = desiredMoveSpeed;
    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // on slope
        if (OnSlope() && !exitingSlope)
        {
            rb.AddForce(GetSlopeMoveDirection(moveDirection) * moveSpeed * 20f, ForceMode.Force);

            if (rb.velocity.y > 0)
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
        }
        if (OnSlope() && sliding)
        {
            if (rb.velocity.y > 0)
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
        }

        // on ground
        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        // in air
        else if(!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);

        //Toggle Gravity
        if (slopeGravityToggle)
        {
            rb.useGravity = !OnSlope();
        }

    }

    private void SpeedControl() {
     // limiting speed on slope (MAY CHANGE/DELETE LATER)
        if (OnSlope() && !exitingSlope && rb.velocity.y > 0f)
        {
            if (rb.velocity.magnitude > moveSpeed)
                rb.velocity = rb.velocity.normalized* moveSpeed;
        }
        else if (OnSlope() && !exitingSlope && rb.velocity.y < 0f)
        {
            if (rb.velocity.magnitude > moveSpeed)
                rb.velocity = rb.velocity.normalized * moveSpeed * 1.6f;
        }
        // limiting speed on ground or in air
        else if (!OnSlope())
        {
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            // limit velocity if needed
            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }
    }

    private void Jump()
    {
        exitingSlope = true;
        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;
        exitingSlope = false;

    }

    public bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    public Vector3 GetSlopeMoveDirection(Vector3 direction)
    {
        return Vector3.ProjectOnPlane(direction, slopeHit.normal).normalized;
    }

    public void EmergencyMove()
    {
        if(transform.position.y < startYpos/3f && transform.position.y > - 1f)
        {
            transform.position += transform.forward * 3f;
            transform.position += transform.up * 1f;


        }
    }


}
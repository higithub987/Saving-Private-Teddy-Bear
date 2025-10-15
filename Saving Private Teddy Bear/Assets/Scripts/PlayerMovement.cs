using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float groundDrag;
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    public float climbSpeed;
    bool readyToJump;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask ground;
    public float groundCheckRadius = 0.1f;
    public Vector3 groundCheckOffset = new Vector3(0f, -0.5f, 0f);

    bool grounded;

    public GameManager manager; 

    public Transform orientation;
    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    [Header("Inspector Based Variables")]
    public LayerMask wallLayer;
    public GameObject cameraObj;
    public InteractablesUI interactablesui;

    [Header("Private variables")]
    private bool wallClimbing;
    private bool ToggleHold;
    private bool ClimbKeyHeld;
    private bool CanWallClimb;
    private bool isWallClimbing;
    private KeyCode InteractKey;


    private void Awake()
    {
        manager = GameObject.Find("Gamemanager").GetComponent<GameManager>();
    }
    private void Start()
    {
        isWallClimbing = false;
        ClimbKeyHeld = false;
        InteractKey = KeyCode.E;
        wallClimbing = false;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
    }

    private void Update()
    {
    // determine the position to check for ground contact
    Vector3 spherePos = transform.position + groundCheckOffset;
    float sphereRadius = groundCheckRadius;

    // use a sphere check at the feet to improve reliability and sensitivity
    grounded = Physics.CheckSphere(spherePos, sphereRadius, ground);
    Debug.DrawLine(spherePos, spherePos + Vector3.up * 0.05f, grounded ? Color.green : Color.red);
        GetInput();
        SpeedControl();

        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;

        //sets distance to see if wallclimb is possible
        Debug.DrawRay(transform.position + new Vector3(0f, playerHeight / 10, 0),
        transform.TransformDirection(cameraObj.transform.rotation * Vector3.forward) * 50f, Color.red);
        if (Physics.Raycast(transform.position + new Vector3(0f, playerHeight / 10, 0), 
            transform.TransformDirection(cameraObj.transform.rotation * Vector3.forward), 1.0f, wallLayer))
        {
            interactablesui.showWallClimb();
            CanWallClimb = true;
            Debug.Log("see wall");
        }
        else
        {
            CanWallClimb = false;
        }
        //checks to see if wallclimb is toggle or hold
        ToggleHold = manager.ToggleHold();
        
        //if toggle
        if (ToggleHold)
        {
            if (!CanWallClimb)
            {
                ClimbKeyHeld = false;
                isWallClimbing = false;
                wallClimbing = false;
            }
            Debug.Log("Is Wall Climbing " + isWallClimbing);
            //check to see if the key to press for interacting gets pressed and wallclimb is possible
            if (Input.GetKeyDown(InteractKey) && CanWallClimb && !isWallClimbing)
            {
                ClimbKeyHeld = true;
                isWallClimbing = true;
            }
            else if(Input.GetKeyDown(InteractKey) && isWallClimbing)
            {
                //if already wallclimbing and toggle again to stop, stop wallclimbing
                wallClimbing = false;
                isWallClimbing = false;
                ClimbKeyHeld = false;
                Debug.Log("run");
            }

            Debug.Log("Climb KeyHeld: " + ClimbKeyHeld);
        }

        else
        {
            if (Input.GetKey(InteractKey) && CanWallClimb)
            {
                ClimbKeyHeld = true;
            }
            else
            {
                ClimbKeyHeld = false;
            }
        }
        
        wallClimbing = ClimbKeyHeld && CanWallClimb;
    }

    private void OnDrawGizmos()
    {
        Vector3 spherePos = transform.position + groundCheckOffset;
        Gizmos.color = grounded ? Color.green : Color.cyan;
        Gizmos.DrawWireSphere(spherePos, groundCheckRadius);
    }

    private void FixedUpdate()
    {
        if (wallClimbing)
        {
            ClimbWalls();
        }
        else
        {
            MovePlayer();
        }
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        if (Input.GetKey(jumpKey) && readyToJump && grounded && !wallClimbing)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);

        else if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    private void ClimbWalls()
    {
            GetInput();
            moveDirection = orientation.up * verticalInput + orientation.right * horizontalInput;
            rb.velocity = moveDirection.normalized * moveSpeed;
            //transform.position += moveDirection.normalized * climbSpeed * Time.deltaTime;
        
    }
}

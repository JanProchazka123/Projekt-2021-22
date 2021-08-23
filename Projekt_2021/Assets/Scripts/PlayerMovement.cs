using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [Header("DO NOT TOUCH")]
    public float timeForNext = 0;
    Rigidbody rigidBody;
    public StaminaBar staminaBar;
    public bool grounded;
    private bool canStand = false;
    private float doubleTapTime;

    //Assingables
    public Transform playerCam;
    public Transform orientation;
    public GameObject parentGameObject;
    BoxCollider feet;
    //Rotation and look
    float xRotation = 0;
    [Header("Look")]
    public float sensitivity = 50f;

    //Movement
    [Header("Movement")]
    public float moveSpeed = 2000;
    public float maxeSpeed = 10;
    const float threshold = 0.01f;
    public float maxSlopeAngle = 45f;
    [Header("SlowWalk")]
    public float slowWalkSpeed = 20;
    //Crouch & Slide
    Vector3 crouchScale = new Vector3(1, 0.5f, 1);
    Vector3 playerScale;
    [Header("Crouch & Slide")]
    public float slideForce = 400;
    public float cooldownTime = 2;

    public float friction = 0.175f;
    public float slideFriction = 0.2f;
    [Header("Dash")]
    public float dashForce = 400;

    //Jumping
    [Header("Jumping")]
    public float jumpForce = 200f;

    //Input
    Vector2 inputDirection = new Vector2();
    bool crouching;

    //Sliding
    Vector3 normalVector = Vector3.up;

    void Awake() 
    {
        feet = GetComponent<BoxCollider>();
        rigidBody = GetComponent<Rigidbody>();
    }

    void Start() 
    {
        playerScale =  transform.localScale;
        LockCursor();
    }

    void LockCursor() 
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void FixedUpdate() 
    {
        Movement();
    }

    void Update() 
    {
        MyInput();
        Look();
    }

    /// <summary>
    /// Find user input. Should put this in its own class but im lazy
    /// </summary>
    void MyInput() 
    {
        inputDirection.x = Input.GetAxisRaw("Horizontal");
        inputDirection.y = Input.GetAxisRaw("Vertical");
        inputDirection.Normalize();

        if (Input.GetButton("Jump"))
        {
            Jump();
        }

        //Crouching
        
        if (staminaBar.currentStamina >= 10 && Input.GetKeyDown(KeyCode.LeftControl))
        {
            StartCrouch();
            staminaBar.UseStamina(10);
            canStand = true;
        }
        if (canStand == true && Input.GetKeyUp(KeyCode.LeftControl))  
        {
            StopCrouch();
            canStand = false;
        }
        feet.enabled = inputDirection.magnitude == 0 && !crouching;

        //Dash

        bool doubleTapD = false;
        if (staminaBar.currentStamina >= 25 && Input.GetKeyDown(KeyCode.D))
        {
            if (Time.time < doubleTapTime + .3f)
            {
                doubleTapD = true;
            }
            doubleTapTime = Time.time;
        }
        if(doubleTapD)
        {
            if (grounded) 
            {
                // dash boost forward
                rigidBody.AddForce(orientation.transform.right * dashForce);
                staminaBar.UseStamina(25);
            }
        }
        bool doubleTapA = false;
        if (staminaBar.currentStamina >= 25 && Input.GetKeyDown(KeyCode.A))
        {
            if (Time.time < doubleTapTime + .3f)
            {
                doubleTapA = true;
            }
            doubleTapTime = Time.time;
        }
        if(doubleTapA)
        {
            if (grounded) 
            {
                // dash boost forward
                rigidBody.AddForce(- orientation.transform.right * dashForce);
                staminaBar.UseStamina(25);
            }
        }
    }

    void StartCrouch() 
    {
        crouching = true;
        // squash the player
        transform.localScale = crouchScale;
        // move them up
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
        // if player is moving at a fast enough speed
        if (rigidBody.velocity.magnitude > 0.5f) 
        {
            // and on the ground
            if (grounded) 
            {
                // slide boost forward
                rigidBody.AddForce(orientation.transform.forward * slideForce);
            }
        }
    }

    void StopCrouch() 
    {
        crouching = false;
        // reset scale
        this.transform.localScale = playerScale;
        // move down
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
    }

    void Movement() 
    {
        rigidBody.AddForce(Vector3.down * 10 * Time.deltaTime);

        ApplyFriction();

        //Set max speed
        float maxSpeed = this.maxeSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            maxSpeed = slowWalkSpeed;
        }
        if (Input.GetKey(KeyCode.LeftShift) == false)
        {
            maxSpeed = maxeSpeed;
        }

        //If sliding down a ramp, add force down so player stays grounded and also builds speed
        if (crouching && grounded) 
        {
            rigidBody.AddForce(Vector3.down * Time.deltaTime * 3000);
            return;
        }

        //Some multipliers
        float multiplier = 1f, multiplierV = 1f;

        // Movement in air
        if (!grounded) 
        {
            multiplier = 0.5f;
            multiplierV = 0.5f;
        }

        // Movement while sliding
        if (grounded && crouching) multiplierV = 1f;
        if (grounded && crouching) multiplier = 1f;

        //Apply forces to move player
        // more easily adjust left/right while in the air than forward/back
        rigidBody.AddForce(orientation.transform.right * inputDirection.x * moveSpeed * Time.deltaTime * multiplier);
        rigidBody.AddForce(orientation.transform.forward * inputDirection.y * moveSpeed * Time.deltaTime * multiplier * multiplierV);

        if (rigidBody.velocity.magnitude > maxSpeed) 
        {
            rigidBody.velocity = rigidBody.velocity.normalized * maxSpeed;
        }
    }

    void Jump()
    {
        if (grounded && staminaBar.currentStamina >= 5)
        {
            grounded = false;
            rigidBody.AddForce(normalVector * jumpForce);
            staminaBar.UseStamina(5);
        }
    }

    void Look() 
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.fixedDeltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.fixedDeltaTime;

        //Find current look rotation
        Vector3 rot = playerCam.transform.localRotation.eulerAngles;
        float desiredY = rot.y + mouseX;

        //Rotate, and also make sure we dont over- or under-rotate.
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -70f, 70f);

        //Perform the rotations
        playerCam.transform.localRotation = Quaternion.Euler(xRotation, desiredY, 0);
        orientation.transform.localRotation = Quaternion.Euler(0, desiredY, 0);
    }

    void ApplyFriction() 
    {
        if (!grounded) return;

        //Slow down sliding
        if (crouching) 
        {
            rigidBody.AddForce(moveSpeed * Time.deltaTime * rigidBody.velocity.normalized * slideFriction);
            return;
        }

        Vector3 inverseVelocity = -orientation.InverseTransformDirection(rigidBody.velocity);

        if (inputDirection.x == 0)
        {
            rigidBody.AddForce(inverseVelocity.x * orientation.transform.right * moveSpeed * friction * Time.deltaTime);
        }
        if (inputDirection.y == 0)
        {
            rigidBody.AddForce(inverseVelocity.z * orientation.transform.forward * moveSpeed * friction * Time.deltaTime);
        }
        
    }
    bool IsFloorAngle(Vector3 v) 
    {
        float angle = Vector3.Angle(Vector3.up, v);
        return angle < maxSlopeAngle;
    }

    bool cancellingGrounded;

    /// <summary>
    /// Handle ground detection
    /// </summary>
    void OnCollisionStay(Collision other)
    {
        int layer = other.gameObject.layer;
        int ground = LayerMask.NameToLayer("Default");
        if (layer != ground) return;

        //Iterate through every collision in a physics update
        for (int i = 0; i < other.contactCount; i++)
        {
            Vector3 normal = other.contacts[i].normal;
            if (IsFloorAngle(normal))
            {
                grounded = true;
                normalVector = normal;
                cancellingGrounded = false;
                CancelInvoke(nameof(StopGrounded));
            }
        }

        //Invoke ground/wall cancel, since we can't check normals with CollisionExit
        if (!cancellingGrounded)
        {
            cancellingGrounded = true;
            Invoke(nameof(StopGrounded), Time.deltaTime);
        }
    }

    private void StopGrounded()
    {
        grounded = false;
    }
}
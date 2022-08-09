using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author:Christopher Cruz
public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;
    public Transform spawn;
    public bool sceneChanged;
    [Header("Player Stats")]
    [SerializeField]
    private float speed;
    public float walkSpeed;
    public float runSpeed;
    public float movementSpeed;
    public float airMovementSpeed;
    public float airdrag;
    public float jumpHeight;
    public float rbDrag;
    [SerializeField]
    private float currentJumpHeight;
    private float d;
    Rigidbody rb;
    [HideInInspector]
    public bool resetInput;
    public GravityCollider lastCollider;
    float delta;
    [Header("Physics Variables for local gravity")]
    public float GravityRotationSpeed;
    public float turnSpeed;
    public float DownwardPush;
    Vector3 GroundDir;
    Vector3 targetDir;
    Vector3 MovDirection;
    [SerializeField]
    bool isJumping;
    //public bool isGravity;
    public bool isFaux;
    [Header("Checks if player is touching ground.")]
    public bool isGrounded;
    public Transform groundCheck;
    public Transform[] groundChecks;
    public float groundDistance;
    public LayerMask groundMask;
    public GravityMechanics gravityMech;
    public GravityCollider gravityCollider;
    public GravityTrigger gravityOrbit;
    [HideInInspector]
    public UIChange cameraMouse;
    GravityTool tool;
    public GameManager gameManager;
    void Awake()
    {
        spawn = GameObject.FindGameObjectWithTag("IntialSpawnPoint").transform;
        transform.position = spawn.position;
        spawn = null;
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(this.gameObject);
        }
    }
    void Start()
    {
        //spawn = GameObject.FindGameObjectWithTag("IntialSpawnPoint").transform;
        //transform.position = spawn.position;
        gravityMech = GetComponent<GravityMechanics>();
        tool = GetComponentInChildren<GravityTool>();

        rb = GetComponent<Rigidbody>();
        
        currentJumpHeight = jumpHeight;

        d = Time.deltaTime;
        GroundDir = transform.up;
        cameraMouse = gravityMech.cameraMouseControl;
        resetInput = false;

        gravityMech.isBottom = true;
    }

    //checks player input for movement and as well second mechanic
    void FixedUpdate()
    {
        if(gameManager.playerInput == true)
        {
            //gravityMech = GetComponent<GravityMechanics>();
            if(!gravityMech.isRotate)
            {
                if(Input.GetKey(KeyCode.W) && (Input.GetKey(KeyCode.LeftShift)))
                {
                    speed = runSpeed;
                }
                else
                {
                    speed = walkSpeed;
                }
                //Movement();
                if(gravityOrbit)
                {
                    //transform.position = Vector3.Lerp()
                    Vector3 gravityUp = (transform.position - gravityOrbit.transform.position).normalized;
                    Vector3 localUp = transform.up;
                    Quaternion targetRotation = Quaternion.FromToRotation(localUp, gravityUp) * transform.rotation;

                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.1f);
                   // transform.rotation = targetRotation;

                    rb.AddForce((gravityUp * -gravityOrbit.gravity));
                }
                Movement();
            }
        }
    }

    //loop checking player inputs like shoot and jump
    void Update()
    {
        if(gameManager.playerInput == true)
        {
            //mouse click to fire gravity tool
            if(Input.GetMouseButtonDown(0) && !gravityMech.isRotate)
            {
                tool.ShootTool();
            }

            if(Input.GetButtonDown("Jump") && !isJumping && isGrounded)
            {
                //rb.velocity = Vector3.zero;
                rb.AddForce(transform.up * jumpHeight, ForceMode.Impulse);
                isJumping = true;
            }

            //Reset Gravity to Normal
            if(Input.GetKeyDown(KeyCode.F) && !gravityMech.isRotate)
            {
                if(isFaux || gravityMech.wallGravity || !isGrounded)
                {
                    gravityMech.GravityOff();
                    resetInput = true;
                }
            }
        }
    }
    void LateUpdate()
    {
        if(resetInput)
        {
            ResetInput();
        }
    }

    //Raycast used for second mechanic to check the angles of the floor in all direction
    Vector3 FloorAngleCheck()
    {
        RaycastHit HitFront;
        RaycastHit HitCenter;
        RaycastHit HitBack;
        RaycastHit HitLeft;
        RaycastHit HitRight;

        Physics.Raycast(groundChecks[0].position, -groundChecks[0].transform.up, out HitFront, 10f, groundMask);
        Physics.Raycast(groundChecks[1].position, -groundChecks[1].transform.up, out HitCenter, 10f, groundMask);
        Physics.Raycast(groundChecks[2].position, -groundChecks[2].transform.up, out HitBack, 10f, groundMask);
        Physics.Raycast(groundChecks[3].position, -groundChecks[3].transform.up, out HitLeft, 10f, groundMask);
        Physics.Raycast(groundChecks[4].position, -groundChecks[4].transform.up, out HitRight, 10f, groundMask);

        Vector3 HitDir = transform.up;

        if(HitFront.transform != null)
        {
            HitDir += HitFront.normal;
        }
        if(HitCenter.transform != null)
        {
            HitDir += HitCenter.normal;
        }
        if(HitBack.transform != null)
        {
            HitDir += HitBack.normal;
        }
        if(HitLeft.transform != null)
        {
            HitDir += HitLeft.normal;
        }
        if(HitRight.transform != null)
        {
            HitDir += HitRight.normal;
        }

        Debug.DrawLine(transform.position, transform.position + (HitDir.normalized * 5f), Color.red);

        return HitDir.normalized;
    }

    //used to rotate player when walking on mesh
    public void RotateSelf(Vector3 Direction, float d, float GravitySpd)
    {
        Vector3 LerpDir = Vector3.Lerp(transform.up, Direction, d * GravitySpd);
        transform.rotation = Quaternion.FromToRotation(transform.up, LerpDir) * transform.rotation;
    }

    //
    void RotateMesh(float d, Vector3 LookDir, float spd)
    {
        Quaternion SlerpRot = Quaternion.LookRotation(LookDir, transform.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, SlerpRot, spd * d);
    }

    //With the help of code Author/credit SlugGlove basic tutorial on walking on walls and modify it to work with my play movement controls and gravity I was able to recreate gravity on cube.
    //function that activates second mechanics
    void PlanetGravity()
    {
        
        float newJumpheight = 25;
        jumpHeight = newJumpheight;

        Quaternion lookDir = Quaternion.LookRotation(targetDir);
        float TurnSpd = turnSpeed;
            
        Vector3 SetGroundDir = FloorAngleCheck();
        GroundDir = Vector3.Lerp(GroundDir, SetGroundDir, d * GravityRotationSpeed);

        RotateSelf(GroundDir, d, GravityRotationSpeed);
        RotateMesh(d, transform.forward, turnSpeed);
            
        float Spd = speed;
        float Acceleration = 4f;

        Vector3 curVelocity = rb.velocity;
        Vector3 targetVelocity = MovDirection * Spd;
        
        MovDirection = targetDir;

        if(isGrounded)
        {
            targetVelocity -= SetGroundDir * DownwardPush;

            Vector3 dir = Vector3.Lerp(curVelocity, targetVelocity, d * Acceleration);
            rb.velocity = dir;
        } 
        else if(!isGrounded)
        {
            targetVelocity -= GroundDir * DownwardPush;

            Vector3 dir = Vector3.Lerp(curVelocity, targetVelocity, d * Acceleration);
            rb.velocity = dir;            
        }
    }
    void Movement()
    {
        //raycast that checks if the player is touching the ground
        isGrounded = Physics.CheckBox(groundCheck.position, new Vector3(1, groundDistance, 1), Quaternion.identity, groundMask);

        //player movement
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 move =  transform.right * x + transform.forward * z;

        targetDir = move;
    
        //increased drag to move smoother and faster while on the ground
        if(isGrounded)
        {
            //Debug.Log("Touching ground");
            rbDrag = 6f;
            rb.drag = rbDrag;
            rb.AddForce(move.normalized * speed * movementSpeed, ForceMode.Acceleration);
            isJumping = false;

        }

        //if in the air lowers drag to fall down faster, simulates long jumps, and restrict players air movement
        if(!isGrounded)
        {
            //Debug.Log("In the air")
            rbDrag = 1f;
            rb.drag = rbDrag;
            
            rb.AddForce(move.normalized * speed * airMovementSpeed * airdrag, ForceMode.Acceleration);
        }
        if(isFaux && !gravityMech.wallGravity)
        {
            PlanetGravity();
        }
        else
        {
            jumpHeight = currentJumpHeight;
        }
    }
    public void ResetInput()
    {
        resetInput = false;
    }

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "FauxTrigger")
        {
            isFaux = true;
            //gravityOrbit = null;

        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "FauxTrigger")
        {
            isFaux = false;
            
            
            gravityCollider.isGravity = false;
            gravityCollider.particle.Stop();
            gravityCollider.transform.gameObject.tag = "NoFaux";
            gravityCollider.isOrbit.enabled = false;
            gravityCollider.isTrigger.enabled = false;
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Floor" && transform.rotation.z !=0)
        {
            gravityMech.GravityOff();
        }
    }
    void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "FauxGravity")
        {
            gravityOrbit = null;
        }
    }

    public void playerSpawn()
    {
        spawn = GameObject.FindGameObjectWithTag("IntialSpawnPoint").transform;
        transform.position = spawn.position;
    }
}

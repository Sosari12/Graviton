using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;


public class PlayerMovement : MonoBehaviour
{
    public float curentDrag;

    [Header("Movement")]
    private float startMoveSpeed;
    public float moveSpeed;
    public float actualSpeed;
    public float speedLimit;


    public Transform orientation;
    public Transform FloorUnderCheckerF;
    public Transform FloorUnderCheckerB;
    public Transform FloorUnderCheckerR;
    public Transform FloorUnderCheckerL;

    float horizontalInput;
    float verticalInput;
    float gravity = -9.81f;
    public float groundDrag;
    

    Vector3 moveDirection;
    Vector3 moveDown;
    Vector3 moveUp;

    Rigidbody rb;

    public float jumpForce;
    public float jumpCD;
    public float airMultiplier;
    bool readyToJump = true;

    public float vel;
    public Vector3 localVel;

    [Header("WallCliming")]

    public Transform groundCheck;
    public Transform wallCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public LayerMask IgnoreMe;
    public bool isGrounded;
    private bool seeWallUnder;
    public bool seeWall;
    private bool lastStatus;
    /// VECTORS
    private Vector3 wallNormal;
    private Vector3 groundNormal;
    private Vector3 groundNormalUnder;

    private Quaternion Normalrotation;
    private Quaternion NormalFloorRotation;
    private Vector3 playerVector;
    /// RAYCAST
    private RaycastHit hit;
    private RaycastHit hitFloor;
    private RaycastHit hitFloorUnderF;
    private RaycastHit hitFloorUnderB;
    private RaycastHit hitFloorUnderR;
    private RaycastHit hitFloorUnderL;


    private bool belowHalfSpeed;
    public bool jumping;

    [Header("Sides check")]
    public float rotationX = 0;
    public float rotationZ = 0;

    public bool forward;
    public bool backwards;
    public bool left;
    public bool right;

    [Header("Climb Anim")]
    public bool climbing;
    public float ClimbTimeMax;
    float ClimbTime;
    float zapRotX;
    float zapRotZ;
    bool dodajeX;
    bool dodajeZ;
    bool gotoweX;
    bool gotoweZ;

    [Header("KeyBinds")]
    public KeyCode jumpKey = KeyCode.Space;


    [Header("Sliding")]
    bool sliding;
    public GameObject cameraPos;
    public Transform[] crouchPos;



    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        ClimbTime = ClimbTimeMax;
        startMoveSpeed = moveSpeed;
    }

    /// <summary>
    /// UPDATE
    /// </summary>


    // Update is called once per frame
    void Update()
    {
        curentDrag = rb.drag;
        actualSpeed = rb.velocity.magnitude;

        Vector3 flatVell = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        vel = flatVell.magnitude;

        localVel = transform.InverseTransformDirection(rb.velocity);
        //localVel.z = MovSpeed;
        //rigidbody.velocity = transform.TransformDirection(locVel);



        seeWallUnder = Physics.CheckSphere(groundCheck.position, 0.6f, groundMask);
        isGrounded = Physics.Raycast(transform.position, -transform.up, 2f, groundMask);
        seeWall = Physics.CheckSphere(wallCheck.position, groundDistance, groundMask);
        Ray ray = new Ray(orientation.position, orientation.forward);
        Ray rayFloor = new Ray(transform.position, -transform.up);
        Ray rayFloorUnderF = new Ray(FloorUnderCheckerF.position, -FloorUnderCheckerF.forward);
        Ray rayFloorUnderB = new Ray(FloorUnderCheckerB.position, FloorUnderCheckerB.forward);
        Ray rayFloorUnderR = new Ray(FloorUnderCheckerR.position, -FloorUnderCheckerR.forward);
        Ray rayFloorUnderL = new Ray(FloorUnderCheckerL.position, FloorUnderCheckerL.forward);

        //RaycastHit hit;

        if(Input.GetKey(KeyCode.W)) if (Physics.Raycast(rayFloorUnderF, out hitFloorUnderF)) groundNormalUnder = hitFloorUnderF.normal;
        if (Input.GetKey(KeyCode.S)) if (Physics.Raycast(rayFloorUnderB, out hitFloorUnderB)) groundNormalUnder = hitFloorUnderB.normal;
        if (Input.GetKey(KeyCode.D)) if (Physics.Raycast(rayFloorUnderR, out hitFloorUnderR)) groundNormalUnder = hitFloorUnderR.normal;
        if (Input.GetKey(KeyCode.A)) if (Physics.Raycast(rayFloorUnderL, out hitFloorUnderL)) groundNormalUnder = hitFloorUnderL.normal;

        if (Physics.Raycast(rayFloor, out hitFloor, 3f, IgnoreMe))
        {
            groundNormal = hitFloor.normal;
        }

        if(Physics.Raycast(ray, out hit, Mathf.Infinity, IgnoreMe))
        {
            wallNormal = hit.normal;
        }

        Debug.DrawLine(orientation.position, hit.point, Color.red);
        Debug.DrawRay(hit.point, hit.normal, Color.green);
        Debug.DrawLine(transform.position, hitFloor.point, Color.red);
        Debug.DrawLine(FloorUnderCheckerF.position, hitFloorUnderF.point, Color.blue);
        Debug.DrawLine(FloorUnderCheckerB.position, hitFloorUnderB.point, Color.blue);
        Debug.DrawLine(FloorUnderCheckerR.position, hitFloorUnderR.point, Color.blue);
        Debug.DrawLine(FloorUnderCheckerL.position, hitFloorUnderL.point, Color.blue);

        //jezeli sie nie wspina
        MyInput();
        if (!climbing)
        {
            if(isGrounded && !jumping && !climbing)
            {
                NormalFloorRotation = Quaternion.FromToRotation(transform.up, hitFloor.normal) * transform.rotation;
                transform.rotation = Quaternion.Slerp(transform.rotation, NormalFloorRotation, 2f * Time.deltaTime);
            }
            if (!isGrounded)
            {
                lastStatus = true;
            }

            if (isGrounded)
            {
                jumping = false;
                if (lastStatus)
                {
                    lastStatus = false;
                    CameraShaker.Instance.ShakeOnce(3f, 2f, 0f, 1f);
                }
            }
            

            //Pobieranie sterowania
            

            if(!isGrounded && !jumping && !climbing && seeWallUnder)
            {
                NormalFloorRotation = Quaternion.FromToRotation(transform.up, groundNormalUnder) * transform.rotation;
                transform.rotation = Quaternion.Slerp(transform.rotation, NormalFloorRotation, 8f * Time.deltaTime);
            }


            if(Input.GetKey(KeyCode.W) && seeWall)
            {
               // transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
                Normalrotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
                climbing = true;
                if (!belowHalfSpeed) ClimbTime = ClimbTime / 2f;
            }

            if (Input.GetKeyDown(KeyCode.Space) && readyToJump && isGrounded)
            {
                CameraShaker.Instance.ShakeOnce(3f, 2f, 0f, .5f);
                readyToJump = false;
                Jump();

                Invoke(nameof(ResetJump), jumpCD);
            }


            if (isGrounded == true && !sliding)
            {
                rb.drag = groundDrag;
                SpeedControl();
            } 
            else
            {
                rb.drag = 0f;
            }
               
        }
        else
        {
            //TurnOnWall();
            TurnOnWallNormal();
        }

        




    }
/// <summary>
/// PO UPDATE
/// </summary>



    private void TurnOnWallNormal()
    {
        if(belowHalfSpeed)transform.rotation = Quaternion.Slerp(transform.rotation, Normalrotation, 4f * Time.deltaTime);
        if(!belowHalfSpeed) transform.rotation = Quaternion.Slerp(transform.rotation, Normalrotation, 6f * Time.deltaTime);

        if (ClimbTime <= 0)
        {
            ClimbTime = ClimbTimeMax;
            climbing = false;
            transform.rotation = Normalrotation;

        }
        else
        {
            ClimbTime -= Time.deltaTime;

        }
    }


    private void FixedUpdate()
    {
        //if(!climbing)MovePlayer();
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        if (Input.GetKey(KeyCode.LeftShift)) Running();

        if (Input.GetKeyUp(KeyCode.LeftShift)) ResetRunning();

        if (Input.GetKey(KeyCode.C)) Slide();
        if (Input.GetKeyUp(KeyCode.C)) SlideReset();

    }

    private void MovePlayer()
    {
        if (!sliding)
        {
            moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        
            if (isGrounded && moveDown.y < 0)
            {
                moveDown.y = -2f;
            }
            



            if (isGrounded)
            {
                rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

            }
            else if (!isGrounded)
            {
                if (actualSpeed < speedLimit) moveDown.y += gravity * Time.deltaTime;
                rb.AddForce(moveDirection.normalized * moveSpeed * airMultiplier, ForceMode.Force);
                
            }
        }
        if (actualSpeed < speedLimit) rb.AddRelativeForce(moveDown * 10f, ForceMode.Force);

    }

    private void Jump()
    {
        jumping = true;
        //reset y velocity
        //rb.velocity = new Vector3(localVel.x, 0f, localVel.z);
        //rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        moveUp.y = 2f;

        rb.AddRelativeForce(moveUp * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    private void SpeedControl()
    {
        //Vector3 flatVel = new Vector3(localVel.x, 0f, localVel.z);
        Vector3 flatVel = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z);

        

        if (flatVel.magnitude < moveSpeed / 2f)
        {
            belowHalfSpeed = true;
        }
        else
        {
            belowHalfSpeed = false;
        }

        //limits the velocity if needed
        
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            //rb.velocity = new Vector3(limitedVel.x, localVel.y, limitedVel.z);
            //rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            rb.velocity = new Vector3(limitedVel.x, limitedVel.y, limitedVel.z);
        }
        
    }

    private void TurnOnWall()
    {
        if (gotoweX == true && gotoweZ == true)
        {
            climbing = false;
            zapRotX = rotationX;
            zapRotZ = rotationZ;
            transform.rotation = Quaternion.Euler(rotationX, 0f, rotationZ);
            gotoweX = false;
            gotoweZ = false;
        }
        else
        {
            ClimbTime -= Time.deltaTime;

            if (dodajeX == true)
            {
                if (zapRotX < rotationX) zapRotX += 2f + Time.deltaTime;
                else gotoweX = true;
            }
            else
            {
                if (zapRotX > rotationX) zapRotX -= 2f + Time.deltaTime;
                else gotoweX = true;
            }

            if (dodajeZ == true)
            {
                if (zapRotZ < rotationZ) zapRotZ += 2f + Time.deltaTime;
                else gotoweZ = true;
            }
            else
            {
                if (zapRotZ > rotationZ) zapRotZ -= 2f + Time.deltaTime;
                else gotoweZ = true;
            }

            transform.rotation = Quaternion.Euler(zapRotX, 0f, zapRotZ);
        }


    }

    private void Running()
    {
        moveSpeed = startMoveSpeed + moveSpeed / 2f;
    }

    private void ResetRunning()
    {
        moveSpeed = startMoveSpeed;
    }

    private void Slide()
    {
        sliding = true;
        rb.drag = 0;
        cameraPos.transform.position = crouchPos[1].position;
    }

    private void SlideReset()
    {
        sliding = false;
        rb.drag = groundDrag;
        cameraPos.transform.position = crouchPos[0].position;
    }

}




/*

/// --- Wspinanie na sciany ---
if (Input.GetKey(KeyCode.W) && forward && seeWall)
{
    //transform.rotation = Quaternion.Euler(rotationX, 0f, 0f);
    zapRotX = rotationX;
    zapRotZ = rotationZ;
    rotationX -= 90f;
    rotationZ = 0;
    climbing = true;

    if (rotationX < zapRotX) dodajeX = false;
    else dodajeX = true;

    if (rotationZ < zapRotZ)dodajeZ = false;
    else dodajeZ = true;
    //transform.Rotate(-90f, 0f, 0f, Space.World);

}
*/

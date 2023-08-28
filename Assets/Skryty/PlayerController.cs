using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    private float gravity = -9.81f;

    Vector3 velocity;

    public Transform groundCheck;
    public Transform wallCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public bool isGrounded;
    public bool seeWall;
    private float rotateBy = 0;

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        seeWall = Physics.CheckSphere(wallCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }


        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        //transform.localPosition = new Vector3(0f, velocity.y, 0f);

        controller.Move(velocity * Time.deltaTime);

        if(seeWall == true)
        {
            if (Input.GetKey(KeyCode.E))
            {

                transform.Rotate(-90f, 0f, 0f, Space.World);
            }
        }

    }

}

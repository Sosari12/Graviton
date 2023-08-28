using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    public float mouseSensX = 100f;
    public float mouseSensY = 100f;
    public Transform mainBody;
    float xRotation = 0f;
    float yRotation = 0f;

    [Header("Camera FOV")]
    private Camera me;
    public PlayerMovement father;
    private float playerSpeed;
    private float startSpeed;

    public float currentFov;
    public float desidedFov;
    public float limitFOV;

    private float startingFov;
    private bool aboveStart;
    private float mnoznik;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        me = GetComponent<Camera>();
        startingFov = GetComponent<Camera>().fieldOfView;
        currentFov = startingFov;
        startSpeed = father.moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensX * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensY * Time.deltaTime;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0);
        //mainBody.Rotate(Vector3.up * mouseX);
        mainBody.localRotation = Quaternion.Euler(0, yRotation, 0);


        ///--- fov ---
        ///
        playerSpeed = father.vel;
        if(playerSpeed > startSpeed)
        {
            aboveStart = true;
            mnoznik = (playerSpeed - startSpeed) / 5f;
            WidenFOV();
        }
        else
        {
            aboveStart = false;
            ResetFOV();
        }


    }

    private void ResetFOV()
    {
        if(me.fieldOfView > startingFov)
        {
            me.fieldOfView -= Time.deltaTime * 5f + mnoznik;
        }
    }

    private void WidenFOV()
    {
        if(me.fieldOfView < limitFOV)
        {
            me.fieldOfView += mnoznik * Time.deltaTime * mnoznik;
        }
    }


}

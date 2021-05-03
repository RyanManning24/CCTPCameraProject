using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public float cameraMoveSpeed = 120.0f;

    public GameObject cameraFollowObject;

    public float clampAngle = 80.0f;

    public float inputSensitivity = 150.0f;

    private float inputX;
    private float inputZ;

    private float mouseX;
    private float mouseY;

    private float finalInputX;
    private float finalInputZ;

    private float rotY = 0.0f;
    private float rotX = 0.0f;

    public Camera mainCam;
    

    public Transitions transitionsScript;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 rot = transform.localRotation.eulerAngles;

        rotY = rot.y;
        rotX = rot.x;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    // Update is called once per frame
    void Update()
    {
        
        if (transitionsScript.onRailscam && transitionsScript.transistion || transitionsScript.onTopDownCam && transitionsScript.transistion || transitionsScript.cutscene && transitionsScript.transistion)
        {

        }
        else
        {
            basicCam();
        }
        

    }

    private void LateUpdate()
    {
        if (transitionsScript.onRailscam && transitionsScript.transistion || transitionsScript.cutscene && transitionsScript.transistion)
        {
            
        }
        else
        {
            CameraUpdate();
        }
    }

    void CameraUpdate()
    {
        // set the target object to follow
        Transform target = cameraFollowObject.transform;

        //move towards the game object that is the target
        float step = cameraMoveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);

    }

    void basicCam()
    {
        BasicCamSetUp();
        if (transitionsScript.onThirdPersonCam || transitionsScript.onFirstPersonCam && transitionsScript.transistion)
        {
            //combine the inputs with the mouse input
            finalInputX = inputX + mouseX;
            finalInputZ = inputZ + mouseY;

            //calculate the rotation with that input and delta time
            rotY += finalInputX * inputSensitivity * Time.deltaTime;
            rotX += finalInputZ * inputSensitivity * Time.deltaTime;

            //clamp the camera to try to resolve clipping depending on the clampangle variable
            rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

            //apply the rotation to the camera
            Quaternion localRotation2 = Quaternion.Euler(rotX, rotY, 0.0f);
            if (transitionsScript.transistion && transitionsScript.onFirstPersonCam)
            {
                mainCam.transform.localRotation = localRotation2;
            }
            else
            {
                transform.localRotation = localRotation2;
            }

        }
        else
        {

        }
    }

    void BasicCamSetUp()
    {
        //setup the rotation of the sticks
        inputX = Input.GetAxis("RightStickHorizontal");
        inputZ = Input.GetAxis("RightStickVertical");

        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
    }


}

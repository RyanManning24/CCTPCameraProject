﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;

    public Transform cam;

    public Animator anim;

    public Transitions transitionsScript;

    private float speed = 6f;
    private float turnSmoothTime = 0.1f;

    private float turnSmoothSpeed;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transitionsScript.cutscene && transitionsScript.transistion)
        {
            //dont have any controls
            
        }
        else
        {
            playerMove();
        }



    }

    private void playerMove()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        //set up these in a vector3 so they can be normalized
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        if (Input.GetKeyDown(KeyCode.LeftShift) | Input.GetButtonDown("joystick button 8"))
        {
            
            anim.SetBool("Running", true);
            anim.SetBool("Walking", true);
            anim.SetFloat("Speed", 2);

        }
        else if (direction.magnitude >= 0.1f && !Input.GetKeyUp(KeyCode.LeftShift))
        {

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothSpeed, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            //Move the player and play the animation
            anim.SetBool("Walking", true);
            anim.SetBool("Running", false);
            anim.SetFloat("Speed", 1);
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
            
        }
        else
        {
            anim.SetBool("Walking", false);
            anim.SetBool("Running", false);
            anim.SetFloat("Speed", 0);
        }
    }



}


﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{

    #region Variables
    public CharacterController controller;

    [Header("Movement Variables")]
    private float movementSpeed;
    public float walkSpeed;
    public float sprintSpeed;
    public float crouchSpeed;
    public float gravity;
    public float jumpHeight;
    private Vector3 playerVelocity;

    private bool isRunning;
    private bool isCrouching;

    public GameObject cameraPosStanding;
    public GameObject cameraPosCrouched;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.3f;
    public LayerMask groundMask;
    bool isGrounded;

    [Header("Camera Movement")]
    public float mouseSensitivity = 100f;
    private float xRotation = 0f;

    #endregion

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        movementSpeed = walkSpeed;

        Camera.main.transform.SetParent(transform);
        Camera.main.transform.localPosition = cameraPosStanding.transform.localPosition;
        Camera.main.transform.rotation = Quaternion.identity;
    }

    void Update()
    {

        #region Player Movement

        float xMovement = Input.GetAxis("Horizontal");
        float zMovement = Input.GetAxis("Vertical");

        #region Ground Check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }
        #endregion

        Vector3 move = transform.right * xMovement + transform.forward * zMovement;

        if (move.magnitude > 1)
        {
            move /= move.magnitude;
        }

        controller.Move(move * movementSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        playerVelocity.y += gravity * Time.deltaTime;

        controller.Move(playerVelocity * Time.deltaTime);

        #endregion

        #region Camera Movement

        float mouseXAxis = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseYAxis = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseYAxis;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        Camera.main.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseXAxis);

        #endregion

        if (Input.GetKey(KeyCode.LeftShift) && !isCrouching)
        {
            isRunning = true;

            movementSpeed = sprintSpeed;
        }
        else
        {
            isRunning = false;
        }

        if (Input.GetKey(KeyCode.C) && !isRunning)
        {
            isCrouching = true;

            movementSpeed = crouchSpeed;

            Camera.main.transform.localPosition = cameraPosCrouched.transform.localPosition;
        }
        else
        {
            isCrouching = false;

            Camera.main.transform.localPosition = cameraPosStanding.transform.localPosition;
        }

        if (!isRunning && !isCrouching)
        {
            movementSpeed = walkSpeed;
        }

    }

}

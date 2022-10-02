using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [Header("Serialize fields")]
    [SerializeField] private CameraController cameraController = null;

    [Header("Movement speed")]
    [SerializeField] private float movementSpeed = 2f;
    

    [Header("Rotation speed")]
    [SerializeField] private float keyRotationSpeed = 150f;
    [SerializeField] private float mouseRotationSpeed = 300f;

    // Private variables
    private float runSpeedFactor = 0f;

    private Vector3 movement = Vector3.zero;
    private Vector3 mouseRotation = Vector3.zero;


    private void Awake()
    {
        cameraController = GameObject.FindObjectOfType<CameraController>();

        if (cameraController == null)
            Debug.LogError("No CameraController found!");
    }

    void Update()
    {
        GetAxisInput();
        HandleMoveSpeed();

        //We want to rotated the character with mouse input if right mouse button pressed
        if (Input.GetMouseButton(1))
        {
            mouseRotation.y = cameraController.MouseX * mouseRotationSpeed * Time.deltaTime;
            // Rotate the Character with Mouse
            transform.Rotate(new Vector3(0, mouseRotation.y, 0));

            // Move the character
            transform.Translate(movement * movementSpeed * runSpeedFactor * Time.deltaTime);
        }
        // rotate with Keys and block moving x
        else
        {
            // Rotate the Character with keys
            transform.Rotate(0, movement.x * keyRotationSpeed * Time.deltaTime, 0);
            
            // Move the character but dont move in x
            transform.Translate(new Vector3(0, 0, movement.z) * movementSpeed * runSpeedFactor * Time.deltaTime);
        }
    }

    private void GetAxisInput()
    {
        movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    }

    private void HandleMoveSpeed()
    {
        // while shift pressed increase the runspeed by this factor
        if (Input.GetKey(KeyCode.LeftShift))
            runSpeedFactor = 2.0f;
        else
            runSpeedFactor = 1f;
    }
}

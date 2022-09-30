using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public float runSpeedFactor = 0f;
    public float rotationSpeed = 150f;
    public float mouseRotateSpeedFactor = 2f;
    public float mouseX;
    public float mouseY;

    private void Start()
    {
    }

    void Update()
    {
        // Get Player Input to move forwards and backwards
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        
        // while shift pressed increase the runspeed by this factor
        if (Input.GetKey(KeyCode.LeftShift))
            runSpeedFactor = 2.0f;
        else
            runSpeedFactor = 1f;

        //We want to rotated the character with mouse input if right mouse button pressed
        if (Input.GetMouseButton(1))
        {
            mouseX += Input.GetAxis("Mouse Y") * rotationSpeed * mouseRotateSpeedFactor * Time.deltaTime;
            mouseX += Input.GetAxis("Mouse X") * rotationSpeed * mouseRotateSpeedFactor * Time.deltaTime;

            transform.localRotation = Quaternion.Euler(-mouseY, mouseX, 0);
            transform.Translate(move * moveSpeed * runSpeedFactor * Time.deltaTime);
        }
        // rotate with Keys and block moving x
        else
        {
            // Rotate the Character with keys
            transform.Rotate(0, move.x * rotationSpeed * Time.deltaTime, 0);
            
            // Move the character but dont move in x
            transform.Translate(new Vector3(0, 0, move.z) * moveSpeed * runSpeedFactor * Time.deltaTime);
        }
    }
}

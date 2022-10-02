using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Public")]
    public float MouseX = 0f;
    public float MouseY = 0f;

    [Header("Serialize fields")]
    [SerializeField] private Camera mainCamera = null;
    [SerializeField] private Player player = null;

    [Header("Camera offset")]
    [SerializeField] private Vector3 CameraOffset = new Vector3(0f, 1.8f, 0f);
    
    [Header("Camera distance")]
    [SerializeField] private float cameraStartDistance = 5f;
    [SerializeField] float cameraMinDistance = 0f;
    [SerializeField] float cameraMaxDistance = 35f;
    [SerializeField] private float mouseWheelSensitivity = 10f;

    [Header("Camera pan and tilt")]
    [SerializeField] private float cameraPanSpeed = 9f;
    [SerializeField] private float cameraTiltSpeed = 9f;
    [SerializeField] private float cameraTiltMin = -80f;
    [SerializeField] private float cameraTiltMax = 35f;

    // Private variables
    private float cameraDistance = 0f;
    private float mouseWheel = 0f;
    
    private float cameraTilt = 0f;
    private float cameraPan = 0f;


    private void Awake()
    {
        if (mainCamera == null)
        {
            if (GameObject.FindGameObjectWithTag("MainCamera"))
                mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            else
                Debug.LogError("No Camera with Tag MainCamera found");
        }

        mainCamera.transform.parent = gameObject.transform;
        mainCamera.transform.position = Vector3.zero;
        mainCamera.transform.rotation = Quaternion.identity;
        cameraDistance = cameraStartDistance;
    }

    void Start()
    {
        if (player == null)
        {
            if (GameObject.FindGameObjectWithTag("Player"))
                player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            else
                Debug.LogError("No Player with Tag Player found");
        }

        gameObject.transform.localRotation = player.transform.localRotation;
        cameraPan = player.transform.localEulerAngles.y;
    }


    void Update()
    {
        GetMouseInput();
        HandleCameraDistance();
        
        if (Input.GetMouseButton(0))
        {
            CameraTiltAndPan();
        }

        if (Input.GetMouseButton(1))
        {
            CameraTilt();
        }
    }

    private void LateUpdate()
    { 
        transform.position = player.transform.position + CameraOffset - transform.forward * cameraDistance;
    }

    private void GetMouseInput()
    {
        MouseX = Input.GetAxis("Mouse X");
        MouseY = Input.GetAxis("Mouse Y") ;
        mouseWheel = Input.GetAxis("Mouse ScrollWheel");
    }

    private void HandleCameraDistance()
    {
        cameraDistance -= mouseWheel * mouseWheelSensitivity;
        cameraDistance = Mathf.Clamp(cameraDistance, cameraMinDistance, cameraMaxDistance);
    }

    private void CameraTiltAndPan()
    {
        cameraPan += MouseX * cameraPanSpeed;
        cameraTilt += MouseY * cameraTiltSpeed;

        transform.localEulerAngles = new Vector3(-ClampCameraTilt(cameraTilt), cameraPan, 0);
    }

    private void CameraTilt()
    {
        cameraTilt += MouseY * cameraTiltSpeed;

        transform.localEulerAngles = new Vector3(-ClampCameraTilt(cameraTilt), transform.localEulerAngles.y, 0);
    }

    private void CameraPan()
    {
        cameraPan += MouseX * cameraPanSpeed;

        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, cameraPan, 0);
    }

    private float ClampCameraTilt(float tilt)
    {
        return cameraTilt = Mathf.Clamp(cameraTilt, cameraTiltMin, cameraTiltMax);
    }
}

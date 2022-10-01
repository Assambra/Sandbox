using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera mainCamera = null;
    [SerializeField] private Player player = null;

    [SerializeField] private Vector3 CameraOffset = new Vector3(0f, 1.8f, 0f);
    [SerializeField] private float cameraStartDistance = 5f;
    
    [SerializeField] float cameraMinDistance = 0f;
    [SerializeField] float cameraMaxDistance = 35f;
    [SerializeField] private float mouseWheelSensitivity = 10f;
    
    [SerializeField] private float mouseSensitivityX = 9f;
    [SerializeField] private float mouseSensitivityY = 9f;
    [SerializeField] private float cameraTiltMin = -80f;
    [SerializeField] private float cameraTiltMax = 35f;

    private float cameraDistance = 0f;
    private float mouseWheel = 0f;
    private float mouseX = 0f;
    private float mouseY = 0f;
    
    private float cameraTilt = 0f;
    private float cameraPan = 0f;

    private bool isOverUIElement = false;
    private bool lastStateIsOverUIElement = false;

    private bool setFirstTimePlayerRotation = false;

    //Todo update to the new Unity Input System


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
    }

    void Start()
    {
        // Todo create public property and set it from the character system / controller
        if (player == null)
        {
            if (GameObject.FindGameObjectWithTag("Player"))
                player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            else
                Debug.LogError("No Player with Tag Player found");
        }
        
        mainCamera.transform.position = Vector3.zero;
        mainCamera.transform.rotation = Quaternion.identity;

        gameObject.transform.rotation = player.transform.rotation;

        cameraDistance = cameraStartDistance;
    }


    void Update()
    {
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivityX;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivityY;
        mouseWheel = Input.GetAxis("Mouse ScrollWheel");

        cameraDistance -= mouseWheel * mouseWheelSensitivity;
        cameraDistance = Mathf.Clamp(cameraDistance, cameraMinDistance, cameraMaxDistance);


        if ((Input.GetMouseButton(0) && !isOverUIElement) || lastStateIsOverUIElement)
        {
            cameraPan += mouseX;
            cameraTilt += mouseY;

            // Needed if Player Spawn Rotation Y is not 0 then
            // first mouse left click turns the camera to (0,0,0) look direction
            if (!setFirstTimePlayerRotation)
            {
                cameraPan = player.transform.localEulerAngles.y;
                setFirstTimePlayerRotation = true;
            }
            
            cameraTilt = Mathf.Clamp(cameraTilt, cameraTiltMin, cameraTiltMax);
            transform.localEulerAngles = new Vector3(-cameraTilt, cameraPan, 0);

            lastStateIsOverUIElement = true;
        }

        if(!Input.GetMouseButton(0))
            lastStateIsOverUIElement = false;

        if (Input.GetMouseButton(1))
        {
            cameraTilt += mouseY;
            cameraTilt = Mathf.Clamp(cameraTilt, cameraTiltMin, cameraTiltMax);
            transform.localEulerAngles = new Vector3(-cameraTilt, transform.localEulerAngles.y, 0);
        }

        // Todo if the character rotates the camera should rotate with the character
    }

    private void LateUpdate()
    { 
        transform.position = player.transform.position + CameraOffset - transform.forward * cameraDistance;
    }
}

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
    

    private float lastPlayerRotation = 0f;
    private float playerRotationDifference = 0f;


    private void Awake()
    {
        cameraController = GameObject.FindObjectOfType<CameraController>();

        if (cameraController == null)
            Debug.LogError("No CameraController found!");
    }

    void Update()
    {
        lastPlayerRotation = transform.localEulerAngles.y;

        GetAxisInput();
        HandleMoveSpeed();


        if (Input.GetMouseButton(1))
        {
            transform.Translate(movement * movementSpeed * runSpeedFactor * Time.deltaTime);
        }
        else
        {
            transform.localEulerAngles += new Vector3(0, movement.x * keyRotationSpeed * Time.deltaTime, 0);
            cameraController.cameraPan += movement.x * keyRotationSpeed * Time.deltaTime;
            
            playerRotationDifference = lastPlayerRotation - transform.localEulerAngles.y;
            cameraController.transform.localEulerAngles -= new Vector3(0, playerRotationDifference);
            
            transform.Translate(new Vector3(0, 0, movement.z) * movementSpeed * runSpeedFactor * Time.deltaTime);
        }
    }

    private void GetAxisInput()
    {
        movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    }

    private void HandleMoveSpeed()
    {
        if (Input.GetKey(KeyCode.LeftShift))
            runSpeedFactor = 2.0f;
        else
            runSpeedFactor = 1f;
    }
}

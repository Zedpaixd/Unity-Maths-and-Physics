using UnityEngine;

/// <summary>
/// Usage:
///     CameraBase gameobject (with this script)
///         Camera gameobject (with camera collision script)
/// </summary>
public class CameraMovement : MonoBehaviour
{
    [Header("Camera Variables")]
    public float CameraMoveSpeed = 120f;
    public GameObject target;
    public float clampAngle = 60f;
    public float mouseSensitivity = 100f;
    public bool invertCamera = false;

    private float mouseX, mouseY;
    private float rotationX, rotationY;

    private void Start()
    {
        //local rotation
        rotationX = transform.localRotation.eulerAngles.x;
        rotationY = transform.localRotation.eulerAngles.y;
    }

    private void Update()
    {
        HandleRotationMovement();
    }

    private void LateUpdate()
    {
        HandlePositionMovement();
    }

    private void HandleRotationMovement()
    {
        //set mouse x,y
        mouseX = Input.GetAxis("Mouse Y") * -1;
        mouseY = Input.GetAxis("Mouse X");
        //final rotations
        rotationX += mouseX * mouseSensitivity * Time.deltaTime;
        rotationY += invertCamera.ToInt() * mouseY * mouseSensitivity * Time.deltaTime;

        //clamp rotation
        rotationX = Mathf.Clamp(rotationX, -clampAngle, clampAngle);

        //apply rotation
        transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0);
    }

    private void HandlePositionMovement()
    {
        float step = CameraMoveSpeed * Time.deltaTime;
        
        transform.position = Vector3.MoveTowards(transform.position,
            target.transform.position, step);

    }

}

using UnityEngine;

public class CameraMove : MouseMovementControllable
{
    [SerializeField] private MovementSettings movementSettings;
    [SerializeField] private Rigidbody playerRb;
    [SerializeField] [Range(0, 180)] private int maxVerticalCameraAngle = 90;
    [SerializeField] private float zTiltCenterSpeed;
    [SerializeField] private int maxZTilt;

    private Transform cameraTransform;
    private Vector2 currentRotation;

    private float zTilt;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
    
        Cursor.lockState = CursorLockMode.Locked;
    }

    public override void OnCameraMove(Vector2 mouseInputs)
    {
        mouseInputs *= movementSettings.Sensitivity;

        currentRotation.x += mouseInputs.x;
        currentRotation.y = Mathf.Clamp(currentRotation.y + mouseInputs.y, -maxVerticalCameraAngle, maxVerticalCameraAngle);

        playerRb.MoveRotation(Quaternion.Euler(new Vector3(0, currentRotation.x, 0)));

        zTilt = Mathf.Clamp(zTilt - mouseInputs.x, -maxZTilt, maxZTilt);
        zTilt = Mathf.Lerp(zTilt, 0, Time.deltaTime * zTiltCenterSpeed);
        cameraTransform.localRotation = Quaternion.Euler(new Vector3(-currentRotation.y, 0f, zTilt));
    }
}
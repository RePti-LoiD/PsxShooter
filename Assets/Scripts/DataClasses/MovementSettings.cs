using UnityEngine;

[CreateAssetMenu(fileName = "Create movement settings asset")]
class MovementSettings : ScriptableObject
{
    [Header("Input")]
    [SerializeField] private float groundKeyboardLerpValue;
    [SerializeField] private float airKeyboardLerpValue;
    
    [Header("Camera movement")]
    [SerializeField] private float sensitivity = 1f;

    [Space]
    [Space]
    [Header("Body movement")]
    [SerializeField] private float defaultSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private Vector2 additionalJumpImpuls;
    [SerializeField] private float jumpCount;
    [SerializeField] private float dashCount;

    [Header("Crouch")]
    [SerializeField] private float crouchSpeed;
    [SerializeField] private float onFlyCrouchVelocitySpeed;

    [Header("Wall run")]
    [SerializeField] private float wallRunSpeed;
    [SerializeField] private float dashSpeed;
    [SerializeField] private Vector2 wallRunJumpAdditionalImpuls;

    [Header("Enables")]
    [SerializeField] private bool jumpLandingCameraRotationEnabled;
    [SerializeField] private bool keyboardLerpEnabled;
    [SerializeField] private bool zTiltCameraEnabled;
    
    public float GroundKeyboardLerpValue { get => groundKeyboardLerpValue; set => groundKeyboardLerpValue = value; }
    public float AirKeyboardLerpValue { get => airKeyboardLerpValue; set => airKeyboardLerpValue = value; }
    
    
    public float Sensitivity { get => sensitivity; set => sensitivity = value; }

    
    public float DefaultSpeed { get => defaultSpeed; set => defaultSpeed = value; }
    public float DashSpeed { get => dashSpeed; set => dashSpeed = value; }
    public float JumpForce { get => jumpForce; set => jumpForce = value; }
    public Vector2 AdditionalJumpImpuls { get => additionalJumpImpuls; set => additionalJumpImpuls = value; }
    public float JumpCount { get => jumpCount; set => jumpCount = value; }
    public float DashCount { get => dashCount; set => dashCount = value; }


    public float OnFlyCrouchVelocitySpeed { get => onFlyCrouchVelocitySpeed; set => onFlyCrouchVelocitySpeed = value; }
    public float CrouchSpeed { get => crouchSpeed; set => crouchSpeed = value; }
    
    
    public float WallRunSpeed { get => wallRunSpeed; set => wallRunSpeed = value; }
    public Vector2 WallRunJumpAdditionalImpuls { get => wallRunJumpAdditionalImpuls; set => wallRunJumpAdditionalImpuls = value; }


    public bool JumpLandingCameraRotationEnabled { get => jumpLandingCameraRotationEnabled; set => jumpLandingCameraRotationEnabled = value; }
    public bool KeyboardLerpEnabled { get => keyboardLerpEnabled; set => keyboardLerpEnabled = value; }
    public bool ZTiltCameraEnabled { get => zTiltCameraEnabled; set => zTiltCameraEnabled = value; }
}
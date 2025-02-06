using UnityEngine;

[CreateAssetMenu(fileName = "Create movement settings asset")]
class MovementSettings : ScriptableObject
{
    [Header("Input")]
    [SerializeField] private float keyboardLerpValue;
    [SerializeField] private bool keyboardLerpEnabled;
    
    [Header("Camera movement")]
    [SerializeField] private float sensitivity = 1f;

    [Space]
    [Space]
    [Header("Body movement")]
    [SerializeField] private float defaultSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private Vector2 additionalJumpImpuls;
    [SerializeField] private float jumpCount;

    [Header("Crouch")]
    [SerializeField] private float crouchSpeed;
    [SerializeField] private float onFlyCrouchVelocitySpeed;

    [Header("Wall run")]
    [SerializeField] private float wallRunSpeed;
    [SerializeField] private float dashSpeed;
    [SerializeField] private Vector2 wallRunJumpAdditionalImpuls;
    
    public bool KeyboardLerpEnabled { get => keyboardLerpEnabled; set => keyboardLerpEnabled = value; }
    public float KeyboardLerpValue { get => keyboardLerpValue; set => keyboardLerpValue = value; }

    public float DefaultSpeed { get => defaultSpeed; set => defaultSpeed = value; }
    public float DashSpeed { get => dashSpeed; set => dashSpeed = value; }
    public float JumpForce { get => jumpForce; set => jumpForce = value; }
    public Vector2 AdditionalJumpImpuls { get => additionalJumpImpuls; set => additionalJumpImpuls = value; }
    public float JumpCount { get => jumpCount; set => jumpCount = value; }

    public float OnFlyCrouchVelocitySpeed { get => onFlyCrouchVelocitySpeed; set => onFlyCrouchVelocitySpeed = value; }
    public float CrouchSpeed { get => crouchSpeed; set => crouchSpeed = value; }
    
    public float WallRunSpeed { get => wallRunSpeed; set => wallRunSpeed = value; }
    public Vector2 WallRunJumpAdditionalImpuls { get => wallRunJumpAdditionalImpuls; set => wallRunJumpAdditionalImpuls = value; }

    public float Sensitivity { get => sensitivity; set => sensitivity = value; }
}
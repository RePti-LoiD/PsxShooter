using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MovementControllable
{
    [Header("Links")]
    [SerializeField] private MovementSettings movementSettings;
    private Rigidbody playerRb;

    [Header("Ground check")]
    [SerializeField] private float groundCheckRaycastLength;
    private int currentJumpCount;
    private bool canJump = true;

    [Header("Wall run")]
    [SerializeField] private LayerMask mask;
    [SerializeField] private float wallCheckRaycastLength;
    [SerializeField] private float wallRunSpeedTreshold;

    public bool IsWallRun { get; protected set; } = false;
    private Vector3 currentWallNormal;
    public WallSide WallSide { get; protected set; }

    [Header("Jump")]
    [SerializeField] private float additionalImpulsFading;

    private Vector2 additionalHorizontalImpuls;

    public float CurrentSpeed { get; protected set; }

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    protected override void Update()
    {
        base.Update();

        currentWallNormal = GetWall();
        playerRb.useGravity = !IsWallRun;

        AdditionalImpulseFading();

        WallRunStateHandle();
    }

    protected override bool GroundCheck() =>
        Physics.Raycast(transform.position, -transform.up, groundCheckRaycastLength);

    protected override float HorizontalSpeedCalculate() =>
        Mathf.Sqrt(Mathf.Pow(playerRb.linearVelocity.x, 2) + Mathf.Pow(playerRb.linearVelocity.z, 2));

    private void AdditionalImpulseFading() =>
        additionalHorizontalImpuls = Vector2.Lerp(additionalHorizontalImpuls, Vector2.one, additionalImpulsFading * Time.deltaTime);

    private Vector3 GetWall()
    {
        Debug.DrawRay(transform.position, transform.right * wallCheckRaycastLength, Color.red);
        Debug.DrawRay(transform.position, -transform.right * wallCheckRaycastLength, Color.green);

        if (Physics.Raycast(transform.position, transform.right, out RaycastHit rightHit, wallCheckRaycastLength, mask) ^ Physics.Raycast(transform.position, -transform.right, out RaycastHit leftHit, wallCheckRaycastLength, mask))
        {
            if (rightHit.collider == null)
                return leftHit.normal;
            else
                return rightHit.normal;
        }

        return Vector3.zero;
    }

    private void WallRunStateHandle()
    {
        IsWallRun = false;
        CurrentSpeed = movementSettings.DefaultSpeed;
        if (currentWallNormal == Vector3.zero) return;
        if (HorizontalSpeed <= wallRunSpeedTreshold) return;
        if (IsGrounded) return;

        IsWallRun = true;

        WallRun();
    }

    private void WallRun()
    {
        playerRb.linearVelocity = new Vector3(playerRb.linearVelocity.x, 0, playerRb.linearVelocity.z);
        CurrentDirection *= movementSettings.WallRunSpeed;

        currentJumpCount = 0;

        CurrentSpeed += movementSettings.WallRunSpeed;
    }

    private void HandleGroundCheck()
    {
        if (IsGrounded) currentJumpCount = 0;
        else currentJumpCount++;

        if (currentJumpCount < movementSettings.JumpCount) canJump = true;
        else canJump = false;
    }

    public override void OnJump()
    {
        HandleGroundCheck();

        if (!canJump) return;

        playerRb.linearVelocity = new Vector3(playerRb.linearVelocity.x, movementSettings.JumpForce, playerRb.linearVelocity.z);
        CurrentDirection = new Vector3(CurrentDirection.x, movementSettings.JumpForce, CurrentDirection.z);

        if (IsWallRun)
            additionalHorizontalImpuls = new Vector2(movementSettings.WallRunJumpAdditionalImpuls.x, movementSettings.WallRunJumpAdditionalImpuls.y);
        else
            additionalHorizontalImpuls = movementSettings.AdditionalJumpImpuls;
    }
    
    public override void OnMove(Vector2 inputs)
    {
        Vector3 moveVector = transform.TransformDirection(new Vector3(inputs.x, 0, inputs.y)) * CurrentSpeed;

        playerRb.linearVelocity = new Vector3(moveVector.x * additionalHorizontalImpuls.x, playerRb.linearVelocity.y, moveVector.z * additionalHorizontalImpuls.x);
        CurrentDirection = new Vector3(inputs.x, CurrentDirection.y, inputs.y);
    }

    public override void OnDash()
    {
        additionalHorizontalImpuls = new Vector2(additionalHorizontalImpuls.x * movementSettings.DashSpeed, additionalHorizontalImpuls.y);
    }

    public override void OnCrouch()
    {
        if (!IsGrounded)
            playerRb.linearVelocity = new Vector3(playerRb.linearVelocity.x, -movementSettings.OnFlyCrouchVelocitySpeed, playerRb.linearVelocity.z);
    }
}
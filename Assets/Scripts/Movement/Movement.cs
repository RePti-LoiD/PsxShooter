using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MovementControllable
{
    [SerializeField] private UnityEvent OnMovementUpdateOrder;

    [Header("Links")]
    [SerializeField] private MovementSettings movementSettings;
    private Rigidbody playerRb;

    [Header("Ground check")]
    [SerializeField] private float groundCheckRaycastLength;
    private int currentJumpCount;
    private bool canJump = true;

    [Header("Jump")]
    [SerializeField] private float additionalImpulsFading;

    private Vector2 additionalHorizontalImpuls;
    public float CurrentSpeed { get; protected set; }

    public void SetCurrentSpeed(float newSpeed)
    {
        if (newSpeed < 0) return;

        CurrentSpeed = newSpeed;
    }

    public void AddCurrentSpeed(float additionalSpeed)
    {
        if (CurrentSpeed + additionalSpeed < 0) return;

        CurrentSpeed += additionalSpeed;
    }

    public void SetCurrentJumpCount(int newJumpCount)
    {
        if (newJumpCount < 0) return;

        currentJumpCount = newJumpCount;
    }

    public void SetAdditionalHorizonalImpulse(Vector2 newHorizonalImpluls) =>
        additionalHorizontalImpuls = newHorizonalImpluls;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    protected override void Update()
    {
        base.Update();

        AdditionalImpulseFading();

        CurrentSpeed = movementSettings.DefaultSpeed;
        
        OnMovementUpdateOrder.Invoke();
    }

    protected override bool GroundCheck() =>
        Physics.Raycast(transform.position, -transform.up, groundCheckRaycastLength);

    protected override float HorizontalSpeedCalculate() =>
        Mathf.Sqrt(Mathf.Pow(playerRb.linearVelocity.x, 2) + Mathf.Pow(playerRb.linearVelocity.z, 2));

    private void AdditionalImpulseFading() =>
        additionalHorizontalImpuls = Vector2.Lerp(additionalHorizontalImpuls, Vector2.one, additionalImpulsFading * Time.deltaTime);

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

        //if (IsWallRun)
        //    additionalHorizontalImpuls = new Vector2(movementSettings.WallRunJumpAdditionalImpuls.x, movementSettings.WallRunJumpAdditionalImpuls.y);
        //else
            additionalHorizontalImpuls = movementSettings.AdditionalJumpImpuls;
    }
    
    public override void OnMove(Vector2 inputs)
    {
        print("ass");
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
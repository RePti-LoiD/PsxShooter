using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MovementControllable
{
    [SerializeField] private UnityEvent OnMovementUpdate;
    [SerializeField] private Vector3Event OnLinearVelocityChanged;
    [SerializeField] private DashEvent OnDashParametrized;
    [Space]

    [Header("Links")]
    [SerializeField] private MovementSettings movementSettings;
    [SerializeField] private CoroutineQueue CoroutineQueue;
    private Rigidbody playerRb;

    [Header("Ground check")]
    [SerializeField] private float groundCheckRaycastLength;

    [Header("Jump")]
    [SerializeField] private float additionalImpulsFading;

    private int currentJumpCount;
    private int currentDashCount;

    private bool canJump = true;

    private Vector3 additionalHorizontalImpuls;
    public float CurrentSpeed { get; protected set; }

    private Vector3 moveVector;
    private float currentLerpValue;

    public int CurrentDashCount
    {
        get => currentDashCount; set
        {
            currentDashCount = value;
            OnDashParametrized?.Invoke(new DashEventArgs
            {
                CurrentDashCount = currentDashCount,
                TimeToRefill = movementSettings.DashTimeToRefill
            });
        }
    }

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

    public void SetAdditionalHorizonalImpulse(Vector3 newHorizonalImpluls) =>
        additionalHorizontalImpuls = newHorizonalImpluls;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        CurrentDashCount = movementSettings.DashCount;
    }

    protected override void Update()
    {
        base.Update();

        AdditionalImpulseFading();

        CurrentSpeed = movementSettings.DefaultSpeed;
        
        OnMovementUpdate.Invoke();
        OnLinearVelocityChanged.Invoke(playerRb.linearVelocity);
    }

    protected override bool GroundCheck() =>
        Physics.Raycast(transform.position, -transform.up, groundCheckRaycastLength);

    protected override float HorizontalSpeedCalculate() =>
        Mathf.Sqrt(Mathf.Pow(playerRb.linearVelocity.x, 2) + Mathf.Pow(playerRb.linearVelocity.z, 2));

    private void AdditionalImpulseFading() =>
        additionalHorizontalImpuls = Vector2.Lerp(additionalHorizontalImpuls, new Vector2(1, 0), additionalImpulsFading * Time.deltaTime);

    private void HandleGroundCheck()
    {
        if (IsGrounded) currentJumpCount = 0;
        else currentJumpCount++;

        if (currentJumpCount < movementSettings.JumpCount) canJump = true;
        else canJump = false;
    }

    public void IsGroundedChanged(bool isGrounded)
    {
        if (isGrounded)
            currentLerpValue = movementSettings.GroundKeyboardLerpValue;
        else
            currentLerpValue = movementSettings.AirKeyboardLerpValue;
    }

    public override void OnJump()
    {
        HandleGroundCheck();

        if (!canJump) return;

        playerRb.linearVelocity = new Vector3(playerRb.linearVelocity.x, movementSettings.JumpForce, playerRb.linearVelocity.z);
        CurrentDirection = new Vector3(CurrentDirection.x, movementSettings.JumpForce, CurrentDirection.z);

        additionalHorizontalImpuls = movementSettings.AdditionalJumpImpuls;
    }
    
    public override void OnMove(Vector2 inputs)
    {
        var transformedInput = transform.TransformDirection(new Vector3(inputs.x, 0, inputs.y)) * CurrentSpeed;
        
        if (movementSettings.KeyboardLerpEnabled)
            moveVector = Vector3.Lerp(moveVector, transformedInput, Time.deltaTime * currentLerpValue);
        else 
            moveVector = transformedInput;

        playerRb.linearVelocity = new Vector3(moveVector.x * additionalHorizontalImpuls.x, playerRb.linearVelocity.y, moveVector.z * additionalHorizontalImpuls.x);
        CurrentDirection = new Vector3(inputs.x, CurrentDirection.y, inputs.y);
    }

    public override void OnDash()
    {
        if (CurrentDashCount <= 0) return;       
        CurrentDashCount--;

        additionalHorizontalImpuls = new Vector2(additionalHorizontalImpuls.x * movementSettings.DashSpeed, additionalHorizontalImpuls.y);

        CoroutineQueue.AddCoroutineToQueue(RefillDash());
    }

    private IEnumerator RefillDash()
    {
        yield return new WaitForSeconds(movementSettings.DashTimeToRefill);

        if (CurrentDashCount < movementSettings.DashCount)
            CurrentDashCount += 1;
    }

    public override void OnCrouch()
    {
        if (!IsGrounded)
            playerRb.linearVelocity = new Vector3(playerRb.linearVelocity.x, -movementSettings.OnFlyCrouchVelocitySpeed, playerRb.linearVelocity.z);
    }
}
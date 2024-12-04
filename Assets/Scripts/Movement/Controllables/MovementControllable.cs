using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class MovementControllable : MonoBehaviour
{
    public abstract void OnMove(Vector2 dircetion);
    public abstract void OnDash();
    public abstract void OnJump();
    public abstract void OnCrouch();

    protected abstract bool GroundCheck();
    protected abstract float HorizontalSpeedCalculate();

    protected virtual void Update()
    {
        IsGrounded = GroundCheck();
        HorizontalSpeed = HorizontalSpeedCalculate();
    }

    private bool isGrounded;
    public bool IsGrounded 
    {
        get => isGrounded;  
        set
        {
            isGrounded = value;
            OnIsGroundedChanged.Invoke(value);
        }
    }
    [Header("Events")][SerializeField] private BoolEvent OnIsGroundedChanged; 
    
    private float horizontalSpeed;
    public float HorizontalSpeed 
    {
        get => horizontalSpeed; 
        set
        {
            horizontalSpeed = value;
            OnHorizonalSpeedChanged.Invoke(value);
        }
    }
    [SerializeField] private UnityEvent<float> OnHorizonalSpeedChanged;

    private Vector3 currentDirection;
    public Vector3 CurrentDirection 
    {
        get => currentDirection; 
        set
        {
            currentDirection = value;
            OnCurrentDirectionChanged.Invoke(value);
        }
    }
    [SerializeField] private Vector3Event OnCurrentDirectionChanged;
}
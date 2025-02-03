using UnityEngine;
using UnityEngine.Events;

public class MovementHandler : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private UnityEvent OnJump;
    [SerializeField] private UnityEvent OnDash;
    [SerializeField] private UnityEvent OnCrouch;

    [Header("Mouse")]
    [SerializeField] private Vector2Event OnMouseMove;
    [SerializeField] private Vector2Event OnMove;

    [Header("Gun")]
    [SerializeField] private UnityEvent OnShot;
    [SerializeField] private UnityEvent OnReload;
    [SerializeField] private UnityEvent OnAdditionalAction;
    [SerializeField] private UnityEvent OnInspect;

    private PlayerInputs inputs;

    private bool isInputEnabled;
    
    private void Awake()
    {
        inputs = new PlayerInputs();
    }

    private void OnEnable()
    {
        inputs.Enable();
        inputs.PlayerMap.Jump.performed += JumpHandler;
        inputs.PlayerMap.Dash.performed += DashHandler;
        inputs.PlayerMap.Crouch.performed += CrouchHandler;
        
        isInputEnabled = true;
    }


    private void OnDisable()
    {
        inputs.Disable();
        inputs.PlayerMap.Jump.performed -= JumpHandler;
        inputs.PlayerMap.Dash.performed -= DashHandler;
        inputs.PlayerMap.Crouch.performed -= CrouchHandler;
        
        isInputEnabled = false;
    }

    private void Update()
    {
        if (!isInputEnabled) return;

        HandleMovement();
        HandleCameraMovement();
    }

    private void JumpHandler(UnityEngine.InputSystem.InputAction.CallbackContext obj) =>
        OnJump.Invoke();
    
    private void DashHandler(UnityEngine.InputSystem.InputAction.CallbackContext obj) =>
        OnDash.Invoke();
    
    private void CrouchHandler(UnityEngine.InputSystem.InputAction.CallbackContext obj) =>
        OnCrouch.Invoke();
    
    private void HandleMovement() =>
        OnMove.Invoke(inputs.PlayerMap.Move.ReadValue<Vector2>());

    private void HandleCameraMovement() =>
        OnMouseMove.Invoke(inputs.PlayerMap.MouseMove.ReadValue<Vector2>());

}
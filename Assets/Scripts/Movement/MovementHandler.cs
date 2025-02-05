using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

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
    [SerializeField] private UnityEvent OnShotStart;
    [SerializeField] private UnityEvent OnShotStop;

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

        inputs.PlayerMap.Shot.started += ShotStartHandler;
        inputs.PlayerMap.Shot.canceled += ShotStopHandler;

        inputs.PlayerMap.Reload.performed += ReloadHandler;
        inputs.PlayerMap.Inspect.performed += InspectHandler;
        inputs.PlayerMap.OnAdditionalAction.performed += AdditionalActionHandler;

        isInputEnabled = true;
    }

    private void OnDisable()
    {
        inputs.Disable();
        inputs.PlayerMap.Jump.performed -= JumpHandler;
        inputs.PlayerMap.Dash.performed -= DashHandler;
        inputs.PlayerMap.Crouch.performed -= CrouchHandler;

        inputs.PlayerMap.Shot.started -= ShotStartHandler;
        inputs.PlayerMap.Shot.canceled -= ShotStopHandler;

        inputs.PlayerMap.Reload.performed -= ReloadHandler;
        inputs.PlayerMap.Inspect.performed -= InspectHandler;
        inputs.PlayerMap.OnAdditionalAction.performed -= AdditionalActionHandler;

        isInputEnabled = false;
    }

    private void Update()
    {
        if (!isInputEnabled) return;

        HandleMovement();
        HandleCameraMovement();
    }

    #region Movement
    private void JumpHandler(InputAction.CallbackContext obj) =>
        OnJump.Invoke();
    
    private void DashHandler(InputAction.CallbackContext obj) =>
        OnDash.Invoke();
    
    private void CrouchHandler(InputAction.CallbackContext obj) =>
        OnCrouch.Invoke();
    #endregion


    #region Mouse
    private void HandleMovement() =>
        OnMove.Invoke(inputs.PlayerMap.Move.ReadValue<Vector2>());

    private void HandleCameraMovement() =>
        OnMouseMove.Invoke(inputs.PlayerMap.MouseMove.ReadValue<Vector2>());
    #endregion


    #region Weapon
    private void ShotStartHandler(InputAction.CallbackContext context) =>
        OnShotStart?.Invoke();
    private void ShotStopHandler(InputAction.CallbackContext context) =>
        OnShotStop?.Invoke();

    private void ReloadHandler(InputAction.CallbackContext obj) =>
        OnReload?.Invoke();

    private void InspectHandler(InputAction.CallbackContext obj) =>
        OnInspect?.Invoke();

    private void AdditionalActionHandler(InputAction.CallbackContext obj) =>
        OnAdditionalAction?.Invoke();
    #endregion
}
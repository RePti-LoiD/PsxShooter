using UnityEngine;

public class MovementHandler : MonoBehaviour
{
    [SerializeField] private MovementControllable movementControllable;
    [SerializeField] private MouseMovementControllable mouseMovementControllable;
    private PlayerInputs inputs;

    private bool isInputEnabled;
    
    private void Awake()
    {
        inputs = new PlayerInputs();
    }

    private void OnEnable()
    {
        inputs.Enable();
        inputs.PlayerMap.Jump.performed += OnJump;
        inputs.PlayerMap.Dash.performed += OnDash;
        inputs.PlayerMap.Crouch.performed += OnCrouch;
        
        isInputEnabled = true;
    }


    private void OnDisable()
    {
        inputs.Disable();
        inputs.PlayerMap.Jump.performed -= OnJump;
        inputs.PlayerMap.Dash.performed -= OnDash;
        inputs.PlayerMap.Crouch.performed -= OnCrouch;
        
        isInputEnabled = false;
    }

    private void Update()
    {
        if (!isInputEnabled) return;

        HandleMovement();
        HandleCameraMovement();
    }

    private void OnJump(UnityEngine.InputSystem.InputAction.CallbackContext obj) =>
        movementControllable.OnJump();
    private void HandleMovement() =>
        movementControllable.OnMove(inputs.PlayerMap.Move.ReadValue<Vector2>());

    private void HandleCameraMovement() =>
        mouseMovementControllable.OnCameraMove(inputs.PlayerMap.MouseMove.ReadValue<Vector2>());

    private void OnDash(UnityEngine.InputSystem.InputAction.CallbackContext obj) =>
        movementControllable.OnDash();
    private void OnCrouch(UnityEngine.InputSystem.InputAction.CallbackContext obj) =>
        movementControllable.OnCrouch();
}
using UnityEngine;
using UnityEngine.InputSystem;

public class FPSMovement : MonoBehaviour
{
    [Header("Speed")]
    [SerializeField] private float walkSpeed = 5f;

    [Header("Jump and Fall")]
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private float gravity = -12f;
    [SerializeField] private float initialFallVelocity = -2f; 

    [Header("References")]
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference jumpAction;

    private CharacterController characterController;
    private bool _isGrounded;
    private float _verticalVelocity;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        moveAction.action.Enable();
        jumpAction.action.Enable();
        jumpAction.action.performed += Jump;
    }

    private void OnDisable()
    {
        moveAction.action.Disable();
        jumpAction.action.Disable();
        jumpAction.action.performed -= Jump;
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (_isGrounded)
        {
            _verticalVelocity = jumpForce;
        }
    }

    private void HandleGravity()
    {
        if (_isGrounded && _verticalVelocity < 0)
        {
            _verticalVelocity = initialFallVelocity; // Reset to a small negative value to keep the character grounded
        }
        else
        {
            _verticalVelocity += gravity * Time.deltaTime;
        }
    }

    private void Update()
    {
        _isGrounded = characterController.isGrounded;

        HandleGravity();

        Vector2 input = moveAction.action.ReadValue<Vector2>();

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // Keep movement horizontal
        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        Vector3 movement = forward * input.y + right * input.x;

        // Horizontal movement
        Vector3 finalMovement = movement * walkSpeed;

        // Vertical movement
        finalMovement.y = _verticalVelocity;

        // Move player
        characterController.Move(finalMovement * Time.deltaTime);
    }
}

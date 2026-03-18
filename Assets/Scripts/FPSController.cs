using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class FPSController : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 5f;
    public float sprintSpeed = 8f;
    public float airControl = 0.3f;

    [Header("Jump")]
    public float jumpHeight = 2f;
    public float coyoteTime = 0.1f;
    public float jumpBuffer = 0.1f;

    [Header("Gravity")]
    public float gravity = -20f;
    public float fallMultiplier = 1.5f;

    [Header("Ground Check")]
    public float groundCheckRadius = 0.4f;
    public LayerMask groundMask;

    [Header("Platform")]
    public float platformCheckDistance = 0.6f;

    [Header("Look")]
    public float mouseSensitivity = 0.5f;
    public float lookXLimit = 80f;

    [Header("References")]
    public Transform cameraHolder;
    public Transform groundCheck;

    // Private
    private CharacterController controller;
    private Vector3 velocity;
    private float xRotation = 0f;
    private bool isGrounded;

    // Platformer timing
    private float coyoteTimeCounter;
    private float jumpBufferCounter;
    private bool isJumping;

    // Input
    private Vector2 moveInput;
    private Vector2 lookInput;
    private bool jumpPressed;
    private bool jumpHeld;
    private bool sprintHeld;

    // Platform
    private Transform platform;
    private Vector3 platformLastPosition;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        ReadInput();
        HandleGroundCheck();
        HandleLook();
        HandleJump();
        HandleMovement();
        HandlePlatform();
        ApplyGravity();
    }

    void ReadInput()
    {
        moveInput = Vector2.zero;
        if (Keyboard.current.wKey.isPressed) moveInput.y += 1;
        if (Keyboard.current.sKey.isPressed) moveInput.y -= 1;
        if (Keyboard.current.dKey.isPressed) moveInput.x += 1;
        if (Keyboard.current.aKey.isPressed) moveInput.x -= 1;
        moveInput = moveInput.normalized;

        lookInput = Mouse.current.delta.ReadValue();

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
            jumpBufferCounter = jumpBuffer;
        jumpHeld = Keyboard.current.spaceKey.isPressed;

        sprintHeld = Keyboard.current.leftShiftKey.isPressed;
    }

    void HandleGroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundMask);

        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
            isJumping = false;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
    }

    void HandleLook()
    {
        float mouseX = lookInput.x * mouseSensitivity;
        float mouseY = lookInput.y * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -lookXLimit, lookXLimit);

        cameraHolder.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void HandleJump()
    {
        if (jumpBufferCounter > 0)
            jumpBufferCounter -= Time.deltaTime;

        if (jumpBufferCounter > 0 && coyoteTimeCounter > 0 && !isJumping)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            isJumping = true;
            jumpBufferCounter = 0;
            coyoteTimeCounter = 0;
        }

        if (!jumpHeld && velocity.y > 0 && isJumping)
        {
            velocity.y *= 0.5f;
        }
    }

    void HandleMovement()
    {
        float speed = sprintHeld ? sprintSpeed : walkSpeed;
        float control = isGrounded ? 1f : airControl;

        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        controller.Move(move * speed * control * Time.deltaTime);
    }

    void HandlePlatform()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, platformCheckDistance, groundMask))
        {
            if (hit.transform.CompareTag("MovingPlatform"))
            {
                if (platform != hit.transform)
                {
                    platform = hit.transform;
                    platformLastPosition = platform.position;
                }

                Vector3 platformDelta = platform.position - platformLastPosition;
                if (platformDelta != Vector3.zero)
                {
                    controller.Move(platformDelta);
                }
                platformLastPosition = platform.position;
            }
            else
            {
                platform = null;
            }
        }
        else
        {
            platform = null;
        }
    }

    void ApplyGravity()
    {
        if (velocity.y < 0)
            velocity.y += gravity * fallMultiplier * Time.deltaTime;
        else
            velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = isGrounded ? Color.green : Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
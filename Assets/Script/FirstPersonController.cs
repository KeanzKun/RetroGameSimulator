using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    // private ShootController shoot;
    public bool CanMove { get; private set; } = true;
    private bool IsSprinting => canSprint && Input.GetKey(sprintKey);
    private bool ShouldJump => Input.GetKeyDown(jumpKey) && characterController.isGrounded;

    [Header("Functional Options")]
    [SerializeField] private bool canSprint = true;
    [SerializeField] private bool canJump = true;

    [Header("Controls")]
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    //[SerializeField] private KeyCode crouchKey = KeyCode.LeftControl;

    [Header("Movement Parameters")]
    [SerializeField] private float walkSpeed = 3.0f;
    [SerializeField] private float sprintSpeed = 6.0f;

    [Header("Look Parameters")]
    [SerializeField, Range(1, 10)] private float lookSpeedX = 2.0f;
    [SerializeField, Range(1, 10)] private float lookSpeedY = 2.0f;
    [SerializeField, Range(1, 180)] private float upperLookLimit = 80.0f;
    [SerializeField, Range(1, 180)] private float lowerLookLimit = 80.0f;

    [Header("Jumping Parameters")]
    [SerializeField] private float jumpForce = 8.0f;
    [SerializeField] private float gravity = 30.0f;

    // [Header("Crouch Parameters")]
    // [SerializeField] private float crouchHeight = 0.5f;
    // [SerializeField] private float standingHeight = 30.0f;
    // [SerializeField] private float timeToCrouch = 0.25f;
    // [SerializeField] private Vector3 crouchingCenter = new Vector3(0,0.5f,0);
    // [SerializeField] private Vector3 standingCenter = new Vector3(0,0,0);
    // private bool isCrouching;
    // private bool duringCrouchAnimation;

    private Camera playerCamera;
    private CharacterController characterController;

    private Vector3 moveDirection;
    private Vector2 currentInput;
    private SceneController sceneController;
    private float rotationX = 0;

    void Awake()
    {
        playerCamera = GetComponentInChildren<Camera>();
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    

    void Update()
    {
        if (CanMove)
        {
            HandleMovementInput();
            HandleMouseLook();

            if (canJump)
                HandleJump();

            ApplyFinalMovements();
        }

        if(Input.GetKeyDown(KeyCode.M))
        {
            SceneController.ChangeScene("FireMap");
        }

    }

    private void HandleMovementInput()
    {
        currentInput = new Vector2((IsSprinting ? sprintSpeed : walkSpeed) * Input.GetAxis("Vertical"), (IsSprinting ? sprintSpeed : walkSpeed) * Input.GetAxis("Horizontal"));

        float moveDirectionY = moveDirection.y;
        moveDirection = (transform.TransformDirection(Vector3.forward) * currentInput.x) + (transform.TransformDirection(Vector3.right) * currentInput.y);
        moveDirection.y = moveDirectionY;
    }

    private void HandleMouseLook()
    {
        rotationX -= Input.GetAxis("Mouse Y") * lookSpeedY;
        rotationX = Mathf.Clamp(rotationX, -upperLookLimit, lowerLookLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeedX, 0);
    }

    private void HandleJump()
    {
        if (ShouldJump)
            moveDirection.y = jumpForce;
    }

    // private void Trigger()
    // {
    //     shoot = new ShootController();
    //     shoot.Shoot();
    // }

    private void ApplyFinalMovements()
    {
        if (!characterController.isGrounded)
            moveDirection.y -= gravity * Time.deltaTime;

        characterController.Move(moveDirection * Time.deltaTime);
    }
}

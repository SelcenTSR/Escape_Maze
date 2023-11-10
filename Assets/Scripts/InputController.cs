using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
   [SerializeField] PlayerInputController inputActions;
    [SerializeField] PlayerAnimatorController playerAnimator;

    [Header("Player Movement")]
    public float verticalMovementInput;
    public float horizontalMovementInput;
    private Vector2 movementInput;


    [Header("Camera Rotation")]
    public float verticalCameraMovement;
    public float horizontalCameraMovement;
    private Vector2 cameraInput;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<PlayerAnimatorController>();
    }
    private void OnEnable()
    {
        if (inputActions == null)
        {
            inputActions = new PlayerInputController();
            inputActions.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>(); // pressing key values going to assign to the movementInput
            inputActions.PlayerMovement.CameraRotation.performed+= i => cameraInput = i.ReadValue<Vector2>();
        }
        inputActions.Enable();
    }
    private void OnDisable()
    {
        inputActions.Disable();
    }


    public void HandleInputs()
    {
        HandleMovementInputs();
        HandleCameraInputs();
    }
    void HandleMovementInputs()
    {
        horizontalMovementInput = movementInput.x;
        verticalMovementInput = movementInput.y;
        playerAnimator.HandleAnimatorValues(horizontalMovementInput, verticalMovementInput);
    }

    void HandleCameraInputs()
    {
        horizontalCameraMovement = cameraInput.x;
        verticalCameraMovement = cameraInput.y;
       
    }

}

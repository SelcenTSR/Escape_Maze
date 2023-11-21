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
    public bool runInput;
    public bool quickTurnInput;


    public bool amingInput;
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
            inputActions.PlayerMovement.Run.performed += i => runInput=true;
            inputActions.PlayerMovement.Run.canceled += i => runInput = false;
            inputActions.PlayerMovement.QuickTurn.performed += i => quickTurnInput = true;
            inputActions.PlayerMovement.QuickTurn.performed += i => quickTurnInput = false;
            inputActions.PlayerActions.Aim.performed += i => amingInput = true;
            inputActions.PlayerActions.Aim.canceled += i => amingInput = false;
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
        HandleAmingInput();
    }
    void HandleMovementInputs()
    {
        horizontalMovementInput = movementInput.x;
        verticalMovementInput = movementInput.y;
        playerAnimator.HandleAnimatorValues(horizontalMovementInput, verticalMovementInput,runInput);

        if (verticalMovementInput != 0 || horizontalMovementInput != 0)
        {
            playerAnimator.rightHandIK.weight = 0;
            playerAnimator.leftHandIK.weight = 0;
        }
        else
        {
            playerAnimator.rightHandIK.weight = 1;
            playerAnimator.leftHandIK.weight = 1;
        }
    }

    void HandleCameraInputs()
    {
        horizontalCameraMovement = cameraInput.x;
        verticalCameraMovement = cameraInput.y;
       
    }
    void HandleQuickTurnInput()
    {
        if (quickTurnInput)
        {
            playerAnimator.PlayAnimationWithoutRootMotion("Quick Turn");
        }
    }

    private void HandleAmingInput()
    {
        if(verticalMovementInput!=0|| horizontalMovementInput != 0)
        {
            amingInput = false;
            playerAnimator.animator.SetBool("isAiming", false);
        }
        if (amingInput)
        {
            playerAnimator.animator.SetBool("isAiming", true);
        }
        else
        {
            playerAnimator.animator.SetBool("isAiming", false);
        }
    }

}
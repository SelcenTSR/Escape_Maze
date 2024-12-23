using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] PlayerInputController inputActions;
    [SerializeField] PlayerAnimatorController playerAnimator;
    PlayerUIManager playerUIManager;
    PlayerController playerController;

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
    public bool interactionInput;

    public bool shootInput;
    public bool aimingInput;
    public bool reloadInput;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        playerUIManager = FindObjectOfType<PlayerUIManager>();
        playerAnimator = GetComponent<PlayerAnimatorController>();
    }
    private void OnEnable()
    {
        if (inputActions == null)
        {
            inputActions = new PlayerInputController();
            inputActions.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>(); // basılı tuşlar movementInput'a atanır
            inputActions.PlayerMovement.CameraRotation.performed += i => cameraInput = i.ReadValue<Vector2>();

            inputActions.PlayerMovement.Run.performed += i => runInput = true;
            inputActions.PlayerMovement.Run.canceled += i => runInput = false;

            inputActions.PlayerMovement.QuickTurn.performed += i => quickTurnInput = true;
            inputActions.PlayerMovement.QuickTurn.performed += i => quickTurnInput = false;

            inputActions.PlayerActions.Aim.performed += i => aimingInput = true;
            inputActions.PlayerActions.Aim.canceled += i => aimingInput = false;

            inputActions.PlayerActions.Shoot.performed += i => shootInput = true;
            inputActions.PlayerActions.Shoot.canceled += i => shootInput = false;

            inputActions.PlayerActions.Reload.performed += i => reloadInput = true;
            inputActions.PlayerActions.Reload.canceled += i => reloadInput = false;

            inputActions.PlayerActions.Interact.performed += i => interactionInput = true;
            inputActions.PlayerActions.Interact.canceled += i => interactionInput = false;
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
        HandleShootingInput();
        HandleReloadInput();
        HandleInteractionInput();
    }
    void HandleMovementInputs()
    {
        horizontalMovementInput = movementInput.x;
        verticalMovementInput = movementInput.y;
        playerAnimator.HandleAnimatorValues(horizontalMovementInput, verticalMovementInput, runInput);

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
            playerAnimator.PlayAnimationWithoutRootMotion("Quick Turn", true);
        }
    }

    private void HandleAmingInput()
    {
        if (verticalMovementInput != 0 || horizontalMovementInput != 0)
        {
            aimingInput = false;
            playerAnimator.animator.SetBool("isAiming", false);
            playerUIManager.crossHair.SetActive(false);
        }
        if (aimingInput)
        {
            playerAnimator.animator.SetBool("isAiming", true);
            playerUIManager.crossHair.SetActive(true);
        }
        else
        {
            playerAnimator.animator.SetBool("isAiming", false);
            playerUIManager.crossHair.SetActive(false);
        }

        playerAnimator.UpdateAimConstraints();
    }


    private void HandleShootingInput()
    {
        if (shootInput && aimingInput)
        {
            shootInput = false;
            Debug.Log("a");
            GetComponent<PlayerController>().UseWeapon();
        }
    }

    private void HandleReloadInput()
    {
        if (reloadInput)
        {
            reloadInput = false;
            if (playerController.playerEquipmentManager.weapon.remainingAmmo == playerController.playerEquipmentManager.weapon.maxAmmo)
                return;

            if (playerController.playerInventoryManager.currentAmmoInInventory
                != null)
            {
                if (playerController.playerInventoryManager.currentAmmoInInventory.ammoType == playerController.playerEquipmentManager.weapon.ammoType)
                {
                    if (playerController.playerInventoryManager.currentAmmoInInventory.ammoRemaining == 0)
                        return;
                    int amountOfAmmoToReload = 0;
                    amountOfAmmoToReload = playerController.playerEquipmentManager.weapon.maxAmmo - playerController.playerEquipmentManager.weapon.remainingAmmo;

                    if (playerController.playerInventoryManager.currentAmmoInInventory.ammoRemaining >= amountOfAmmoToReload)
                    {
                        playerController.playerEquipmentManager.weapon.remainingAmmo = amountOfAmmoToReload + amountOfAmmoToReload;
                        playerController.playerInventoryManager.currentAmmoInInventory.ammoRemaining -= amountOfAmmoToReload;
                    }
                    else
                    {
                        playerController.playerEquipmentManager.weapon.remainingAmmo = playerController.playerInventoryManager.currentAmmoInInventory.ammoRemaining;
                        playerController.playerInventoryManager.currentAmmoInInventory.ammoRemaining = 0;
                    }

                    playerController.playerAnimator.ClearHandIKWeights();
                    playerController.playerAnimator.PlayAnimation("Pistol_Reload", true);

                    playerController.playerEquipmentManager.weapon.remainingAmmo = 12;
                    playerController.playerUIManager.currentAmmoCountText.text = playerController.playerEquipmentManager.weapon.remainingAmmo.ToString();
                    playerController.playerUIManager.reservedAmmoCountText.text = playerController.playerInventoryManager.currentAmmoInInventory.ammoRemaining.ToString();
                    //play reload;
                }
            }


        }
    }

    private void HandleInteractionInput()
    {
        if (interactionInput)
        {
            if (!playerController.canInteract)
            {
                interactionInput = false;
            }
        }
    }

}

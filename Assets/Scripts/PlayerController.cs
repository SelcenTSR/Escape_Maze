using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class PlayerController : MonoBehaviour
{
    [SerializeField] InputController inputController;
    public Transform cameraHolderTransform;
    public float rotationSpeed = 3.5f;
    public Rigidbody playerRigidbody;
    Quaternion targetRotation;
    Quaternion playerRotation;
    public bool isAiming;
    public bool isPerformingAction;
    Animator animator;

    PlayerAnimatorController playerAnimator;
    PlayerEquipmentManager playerEquipmentManager;
    // Start is called before the first frame update
    void Start()
    {
        playerAnimator= GetComponent<PlayerAnimatorController>();
        animator = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerEquipmentManager = GetComponent<PlayerEquipmentManager>();
    }

    private void FixedUpdate()
    {
        HandleRotation();
    }
    private void Update()
    {
        inputController.HandleInputs();
        isPerformingAction = animator.GetBool("isPerformingAction");
        isAiming = animator.GetBool("isAiming");
    }
    public void HandleRotation()
    {
        if (isAiming)
        {
            targetRotation = Quaternion.Euler(0, cameraHolderTransform.eulerAngles.y, 0);
            playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            transform.rotation = playerRotation;
        }
        else
        {
            targetRotation = Quaternion.Euler(0, cameraHolderTransform.eulerAngles.y, 0);
            playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            if (inputController.verticalMovementInput != 0 || inputController.horizontalMovementInput != 0)
            {
                transform.rotation = playerRotation;
            }
        }
        
    }

    public void UseWeapon()
    {
        if (isPerformingAction)
        {
            return;
        }
        playerAnimator.PlayAnimationWithoutRootMotion("Pistol_Shoot",true);
        playerEquipmentManager.weaponManager.ShootWeapon();
    }

  
}

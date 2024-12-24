using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class PlayerController : MonoBehaviour
{
    public InputController inputController;
    public Transform cameraHolderTransform;
    public float rotationSpeed = 3.5f;
    public Rigidbody playerRigidbody;

    Quaternion targetRotation;
    Quaternion playerRotation;

    public bool isAiming;
    public bool canInteract;
    public bool isPerformingAction;
    public bool isDead;

    Animator animator;

    public PlayerInventoryManager playerInventoryManager;
    public PlayerUIManager playerUIManager;
    public PlayerAnimatorController playerAnimator;
    public PlayerEquipmentManager playerEquipmentManager;
    public PlayerStatManager playerStatManager;
    public GameObject bloodExplosion, bloodPool;
    // Start is called before the first frame update
    void Awake()
    {
        playerStatManager = GetComponent<PlayerStatManager>();
        playerInventoryManager = GetComponent<PlayerInventoryManager>();
        playerAnimator = GetComponent<PlayerAnimatorController>();
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
        animator.SetBool("isDead", isDead);
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
        if (playerEquipmentManager.weapon.remainingAmmo > 0)
        {
            playerEquipmentManager.weapon.remainingAmmo -= 1;
            playerUIManager.currentAmmoCountText.text = playerEquipmentManager.weapon.remainingAmmo.ToString();
            playerAnimator.PlayAnimationWithoutRootMotion("Pistol_Shoot", true);
            playerEquipmentManager.weaponManager.ShootWeapon();
        }
        else
        {
            Debug.Log("click");
        }

    }

    public void BloodExplosion()
    {
        bloodExplosion.SetActive(true);
        bloodExplosion.GetComponent<ParticleSystem>().Play();
    }
    public void BloodPool()
    {
        bloodPool.SetActive(true);
        bloodExplosion.GetComponent<ParticleSystem>().Play();
    }

}

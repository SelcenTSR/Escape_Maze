using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipmentManager : MonoBehaviour
{
    PlayerAnimatorController animatorController;
    WeaponLoaderSlot weaponLoaderSlot;
    [Header("Current Equipment")]
    public WeaponItem weapon;
    RightHandIKTarget rightHandIK;
    LeftHandIKTarget leftHandIK;

    PlayerController playerController;

    public WeaponManager weaponManager;
    //subWeaponItem // knife
    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        animatorController = GetComponent<PlayerAnimatorController>();
        LoadWeaponLoaderSlot();
    }
    private void Start()
    {
        LoadCurrentWeapon();
    }

    private void LoadWeaponLoaderSlot()
    {
        weaponLoaderSlot = GetComponentInChildren<WeaponLoaderSlot>();
    }

    //silah animasyonlarına geçiş
    //player'ın eline silahı alma 
    private void LoadCurrentWeapon()
    {
        weaponLoaderSlot.LoadWeaponModel(weapon);
        animatorController.animator.runtimeAnimatorController = weapon.weaponAnimator;
        rightHandIK = weaponLoaderSlot.currentWeaponModel.GetComponentInChildren<RightHandIKTarget>();
        leftHandIK = weaponLoaderSlot.currentWeaponModel.GetComponentInChildren<LeftHandIKTarget>();
        weaponManager = weaponLoaderSlot.currentWeaponModel.GetComponentInChildren<WeaponManager>();
        animatorController.AssignHandIK(rightHandIK, leftHandIK);
        playerController.playerUIManager.currentAmmoCountText.text = weapon.remainingAmmo.ToString();


        if (playerController.playerInventoryManager.currentAmmoInInventory != null && playerController.playerInventoryManager.currentAmmoInInventory.ammoType == weapon.ammoType)
        {
            playerController.playerUIManager.reservedAmmoCountText.text = playerController.playerInventoryManager.currentAmmoCountInInventory.ToString();
        }
    }
    public void SetAmmoEquipment()
    {
        playerController.playerUIManager.reservedAmmoCountText.text = playerController.playerInventoryManager.currentAmmoCountInInventory.ToString();
    }

}

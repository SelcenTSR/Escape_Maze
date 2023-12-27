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

    public WeaponManager weaponManager;
    //subWeaponItem // knife
    private void Awake()
    {
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

    private void LoadCurrentWeapon()
    {
        weaponLoaderSlot.LoadWeaponModel(weapon);
        animatorController.animator.runtimeAnimatorController = weapon.weaponAnimator;
        rightHandIK = weaponLoaderSlot.currentWeaponModel.GetComponentInChildren<RightHandIKTarget>();
        leftHandIK = weaponLoaderSlot.currentWeaponModel.GetComponentInChildren<LeftHandIKTarget>();
        weaponManager = weaponLoaderSlot.currentWeaponModel.GetComponentInChildren<WeaponManager>();
        animatorController.AssignHandIK(rightHandIK,leftHandIK);
    }
}

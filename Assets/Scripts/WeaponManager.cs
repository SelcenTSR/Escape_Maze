using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class WeaponManager : MonoBehaviour
{
    PlayerController playerController;
    Animator weaponAnimator;


    [Header("Weapon FX")]
    public GameObject weaponMuzzleFlashFX;

    [Header("Weapon FX Transforms")]
    public Transform weaponMuzzleFlashTransform;


    public float bulletRange = 100f;
    public LayerMask shootableLayer;
    // Start is called before the first frame update
    void Start()
    {
        weaponAnimator = GetComponentInChildren<Animator>();
        playerController = GetComponentInParent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ShootWeapon()
    {
        GameObject muzzleFlash = Instantiate(weaponMuzzleFlashFX, weaponMuzzleFlashTransform);
        muzzleFlash.transform.parent = null;
        GameObject particle = muzzleFlash;
        DOVirtual.DelayedCall(2,null,false).OnComplete(() => Destroy(particle));
        RaycastHit hit;
        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward,out hit,bulletRange,shootableLayer))
        {
            Debug.Log(hit.collider.gameObject.layer);
            ZombieEffectManager zombie = hit.collider.GetComponent<ZombieEffectManager>();
            if (zombie != null)
            {
                zombie.DamageZombie(playerController.playerEquipmentManager.weapon.damage);
            }
        }

    }
}

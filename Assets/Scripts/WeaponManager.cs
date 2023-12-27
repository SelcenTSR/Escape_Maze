using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class WeaponManager : MonoBehaviour
{

    [Header("Weapon FX")]
    public GameObject weaponMuzzleFlashFX;

    [Header("Weapon FX Transforms")]
    public Transform weaponMuzzleFlashTransform;



    // Start is called before the first frame update
    void Start()
    {
        
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
        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward,out hit))
        {
            Debug.Log(hit.transform.gameObject);
        }

    }
}

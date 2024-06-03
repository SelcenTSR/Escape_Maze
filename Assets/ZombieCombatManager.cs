using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieCombatManager : MonoBehaviour
{

    public int grappleDamage = 33;

   private ZombieGrappleCollider leftHandGrappleCollider;
   private ZombieGrappleCollider rightHandGrappleCollider;
    ZombieManager zombie;

    private void Awake()
    {
        zombie = GetComponent<ZombieManager>();
        LoadGrappleColliders();
    }
    private void LoadGrappleColliders()
    {
        ZombieGrappleCollider[] grappleColliders = GetComponentsInChildren<ZombieGrappleCollider>();
        for(int i = 0; i < grappleColliders.Length; i++)
        {
            if (grappleColliders[i].isRightHandedGrappleCollider)
                rightHandGrappleCollider = grappleColliders[i];
            else
                leftHandGrappleCollider = grappleColliders[i];
        }
    }

    public void OpenGrappleColliders()
    {
        rightHandGrappleCollider.grappleCollider.enabled = true;
        leftHandGrappleCollider.grappleCollider.enabled = true;
    }

    public void CloseGrappleColliders()
    {
        rightHandGrappleCollider.grappleCollider.enabled = false;
        leftHandGrappleCollider.grappleCollider.enabled = false;
    }

    public void EnableRotationDuringAttack()
    {
        zombie.canRotate = true;
    }
    public void DisableRotationDuringAttack()
    {
        zombie.canRotate = true;
    }


}

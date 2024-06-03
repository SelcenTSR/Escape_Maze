using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimatorManager : MonoBehaviour
{
    ZombieManager zombieManager;

    private void Awake()
    {
        zombieManager = GetComponent<ZombieManager>();
    }

    public void PlayGrappleAnimation(string grappleAnimation, bool useRootMotion)
    {
        zombieManager.animator.applyRootMotion = useRootMotion;
        zombieManager.isPerformingAction = transform;
        zombieManager.animator.CrossFade(grappleAnimation, .2f);
    }

    public void PlayTargetAttackAnimation(string attackAnimation)
    {
        zombieManager.animator.applyRootMotion = true;
        zombieManager.isPerformingAction = true;
        zombieManager.animator.CrossFade(attackAnimation, .2f,1);
    }

    public void PlayTargetActionAnimation(string actionAnimation)
    {
        zombieManager.animator.applyRootMotion = true;
        zombieManager.isPerformingAction = true;
        zombieManager.animator.CrossFade(actionAnimation, .2f);
    }
}

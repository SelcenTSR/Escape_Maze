using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ZombieEffectManager : MonoBehaviour
{
    ZombieManager zombieManager;
    private void Awake()
    {
        zombieManager = GetComponent<ZombieManager>();
    }
    public void DamageZombie(int damage)
    {
        zombieManager.isPerformingAction = true;
        zombieManager.animator.CrossFade("GetHit", .2f);
        zombieManager.zombieStatManager.DealDamage(damage);
    }
}

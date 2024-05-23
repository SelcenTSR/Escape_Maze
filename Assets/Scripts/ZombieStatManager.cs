using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieStatManager : MonoBehaviour
{
    ZombieManager zombieManager;
    [SerializeField] int overallHealth=100;

    private void Awake()
    {
        zombieManager = GetComponent<ZombieManager>();
    }
    public void DealDamage(int damage)
    {
        overallHealth -= damage;
        CheckForDeath();
    }

    private void CheckForDeath()
    {
        if (overallHealth <= 0)
        {
            overallHealth = 0;
            zombieManager.isDead=true;
            zombieManager.zombieAnimatorManager.PlayTargetAttackAnimation("Zombie_Dead");
        }
    }
}

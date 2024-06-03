using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    PursueTargetState pursueTargetState;
    public ZombieAttackAction[] zombieAttackActions;

    public List<ZombieAttackAction> potentialAttacks;

    public ZombieAttackAction currentAttack;

    public bool hasPerformedAttack;


    private void Awake()
    {
        pursueTargetState = GetComponent<PursueTargetState>();
    }
    public override State Tick(ZombieManager zombieManager)
    {
        zombieManager.animator.SetFloat("Vertical", 0f, .2f, Time.deltaTime);
        if (zombieManager.currentTarget.isDead) return this;
        if (zombieManager.isPerformingAction)
        {
            zombieManager.animator.SetFloat("Vertical", 0f, .2f, Time.deltaTime);
            return this;
        }

        if(!hasPerformedAttack && zombieManager.attackCoolDownTimer <= 0)
        {
            if (currentAttack == null)
            {
                GetNewAttack(zombieManager);
            }
            else
            {
                AttackTarget(zombieManager);
            }
        }

        if (hasPerformedAttack)
        {
            ResetStateFlag();
            return pursueTargetState;
        }
        else
        {
            return this;
        }
       
    }


    private void GetNewAttack(ZombieManager zombieManager)
    {
        for(int i = 0; i < zombieAttackActions.Length; i++)
        {
            ZombieAttackAction zombieAttack = zombieAttackActions[i];

            if(zombieManager.distanceFromCurrentTarget <= zombieAttack.maxAttackDistance
                && zombieManager.distanceFromCurrentTarget >= zombieAttack.minAttackDistance)
            {
                if(zombieManager.viewableAngleFromCurrentTarget<=zombieAttack.maxAttackAngle&&
                    zombieManager.viewableAngleFromCurrentTarget >= zombieAttack.minAttackAngle)
                {
                    potentialAttacks.Add(zombieAttack);
                }
            }
        }

        int randomValue = Random.Range(0, potentialAttacks.Count);

        if (potentialAttacks.Count > 0)
        {
            currentAttack = potentialAttacks[randomValue];
            potentialAttacks.Clear();
        }
    }

    private void AttackTarget(ZombieManager zombieManager)
    {
        if (currentAttack != null)
        {
            hasPerformedAttack = true;
            zombieManager.attackCoolDownTimer = currentAttack.attackCoolDown;
            zombieManager.zombieAnimatorManager.PlayTargetAttackAnimation(currentAttack.attackAnimation);

        }
        else
        {
            Debug.Log("Zombie attempting attack but has no attack");
        }
    }

    private void ResetStateFlag()
    {
        hasPerformedAttack = false;
    }

  
}

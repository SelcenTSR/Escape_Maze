using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursueTargetState : State
{
    AttackState attackState;

    private void Awake()
    {
        attackState = GetComponent<AttackState>();
    }


    public override State Tick(ZombieManager zombieManager)
    {
        if (zombieManager.isPerformingAction)
        {
            RotateTowardsTargetWhilstAttacking(zombieManager);
            zombieManager.animator.SetFloat("Vertical", 0f, .2f, Time.deltaTime);
            return this;
        }
        MoveTowardsCurrentTarget(zombieManager);
        RotateTowardsTarget(zombieManager);
        if (zombieManager.distanceFromCurrentTarget <= zombieManager.maxAttackDistance)
        {
            zombieManager.navMeshAgent.enabled = false;
            return attackState;
        }
        else
        {
            return this;
        }

    }

    private void MoveTowardsCurrentTarget(ZombieManager zombieManager)
    {
        zombieManager.animator.SetFloat("Vertical", 1.3f, .2f, Time.deltaTime);
    }

    private void RotateTowardsTarget(ZombieManager zombieManager)
    {
        if (!zombieManager.canRotate) return;
        zombieManager.navMeshAgent.enabled = true;
        zombieManager.navMeshAgent.SetDestination(zombieManager.currentTarget.transform.position);
        zombieManager.transform.rotation = Quaternion.Slerp(zombieManager.transform.rotation, zombieManager.navMeshAgent.transform.rotation,zombieManager.rotationSpeed/Time.deltaTime);
    }

    private void RotateTowardsTargetWhilstAttacking(ZombieManager zombieManager)
    {
        if (!zombieManager.canRotate) return;
        Vector3 dir = zombieManager.currentTarget.transform.position - zombieManager.transform.position;
        dir.y = 0;
        dir.Normalize();

        if (dir == Vector3.zero)
        {
            dir = zombieManager.transform.forward;
        }

        Quaternion targetRot = Quaternion.LookRotation(dir);
        zombieManager.transform.rotation = Quaternion.Slerp(zombieManager.transform.rotation, targetRot, zombieManager.rotationSpeed * Time.deltaTime);

    }
}

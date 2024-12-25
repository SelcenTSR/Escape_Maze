using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    PursueTargetState pursueTargetState;

    [SerializeField] float characterEyeLevel = 1.8f;
    [SerializeField] LayerMask ignoreForLineOfSightDetection;

    //how far away we can detect a target
    [SerializeField] LayerMask detectionLayer;
    [SerializeField] float detectionRadius = 20;

    //how wide away we can see  a target within our field of view
    [SerializeField] float minDetectionRadiusAngle = -60f;
    [SerializeField] float maxDetectionRadiusAngle = 60f;

    // we make our character idle until tehy find a potential target

    private void Awake()
    {
        pursueTargetState = GetComponent<PursueTargetState>();
    }

    public override State Tick(ZombieManager zombieManager)
    {
        if (zombieManager.currentTarget != null)
        {
            return pursueTargetState;
        }
        else
        {
            FindAtargetViaLineOfSight(zombieManager);
            return this;
        }
    }

    private void FindAtargetViaLineOfSight(ZombieManager zombieManager)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, detectionLayer);

        for (int i = 0; i < colliders.Length; i++)
        {
            PlayerController playerController = colliders[i].transform.GetComponent<PlayerController>();

            //playercontroller is detected we check for line of sight
            if (playerController != null)
            {
                //Target önümüzde olmalı
                Vector3 targetDirection = transform.position - playerController.transform.position;
                float viewableAngle = Vector3.Angle(targetDirection, transform.forward);
                Debug.Log("found the player");


                if (viewableAngle > minDetectionRadiusAngle && viewableAngle < maxDetectionRadiusAngle)
                {
                    //this make sure raycast start from the floor
                    Vector3 playerStartPoint = new Vector3(playerController.transform.position.x, characterEyeLevel, playerController.transform.position.z);
                    Vector3 zombieStartPoint = new Vector3(transform.position.x, characterEyeLevel, transform.position.z);
                    Debug.DrawLine(playerStartPoint, zombieStartPoint, Color.red);
                    RaycastHit hit;
                    if (Physics.Linecast(playerStartPoint, zombieStartPoint, out hit, ignoreForLineOfSightDetection))
                    {
                        Debug.Log("There is smt in the way");
                    }
                    else
                    {
                        if (playerController.isDead) return;
                        zombieManager.currentTarget = playerController;
                        Debug.Log("target detected" + zombieManager.currentTarget);

                    }

                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{

    PursueTargetState pursueTargetState;

    //how far away we can detect a target
    [SerializeField] LayerMask detectionLayer;
    [SerializeField] float detectionRadius=5;

    //how wide away we can see  a target within our field of view
    [SerializeField] float minDetectionRadiusAngle = -50f;
    [SerializeField] float maxDetectionRadiusAngle = 50f;

    private void Awake()
    {
        pursueTargetState = GetComponent<PursueTargetState>();
    }

    public override State Tick(ZombieManager zombieManager)
    {
        if(zombieManager.currentTarget!=null)
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

        for(int i = 0; i < colliders.Length; i++)
        {
            PlayerController playerController = colliders[i].transform.GetComponent<PlayerController>();

            //playercontroller is detected we check for line of sight
            if (playerController != null)
            {
                Vector3 targetDirection = transform.position - playerController.transform.position;
                float viewableAngle = Vector3.Angle(targetDirection, transform.forward);
                Debug.Log("found the player");


                if (viewableAngle>minDetectionRadiusAngle && viewableAngle < maxDetectionRadiusAngle)
                {
                    float playerHeight = 2f;
                    //this make sure raycast start from the floor
                    Vector3 playerStartPoint=new Vector3(playerController.transform.position.x,playerHeight, playerController.transform.position.z);
                    Vector3 zombieStartPoint= new Vector3(transform.position.x, playerHeight, transform.position.z);
                    Debug.DrawLine(playerStartPoint, zombieStartPoint, Color.red);
                    RaycastHit hit;
                    if(Physics.Linecast(playerStartPoint,zombieStartPoint,out hit))
                    {
                        Debug.Log("There is smt in the way");
                    }
                    else
                    {
                        Debug.Log("target detected");
                        zombieManager.currentTarget = playerController;
                    }
                   
                }
            }
        }
    }
}

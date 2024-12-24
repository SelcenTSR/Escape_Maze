using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerAnimatorController : MonoBehaviour
{
    public Animator animator;
    float snappedHorizontal;
    float snappedVertical;
    PlayerController playerController;

    [SerializeField] RigBuilder rigBuilder;// hold the weapon properly
    public TwoBoneIKConstraint rightHandIK, leftHandIK;


    [Header("Aiming Constraints")]
    public MultiAimConstraint spine01; // turn the character toward the aiming target
    public MultiAimConstraint spine02;
    public MultiAimConstraint head;

    // Start is called before the first frame update
    void Start()
    {
        rigBuilder = GetComponent<RigBuilder>();
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
    }
    public void PlayAnimationWithoutRootMotion(string targetAnimation, bool isPerformingAction)
    {
        animator.SetBool("isPerformingAction", isPerformingAction);
        animator.applyRootMotion = false;
        animator.CrossFade(targetAnimation, .2f);
    }

    public void PlayAnimation(string targetAnimation, bool isPerformingAction)
    {
        animator.SetBool("isPerformingAction", isPerformingAction);
        animator.CrossFade(targetAnimation, .2f);
    }

    public void HandleAnimatorValues(float horizontalMovement, float verticalMovement, bool isRunning)
    {
        if (horizontalMovement > 0)
        {
            snappedHorizontal = 1;
        }
        else if (horizontalMovement < 0)
        {
            snappedHorizontal = -1;
        }
        else
        {
            snappedHorizontal = 0;
        }
        if (verticalMovement > 0)
        {
            snappedVertical = 1;
        }
        else if (verticalMovement < 0)
        {
            snappedVertical = -1;
        }
        else
        {
            snappedVertical = 0;
        }

        if (isRunning && snappedVertical > 0)
        {
            snappedVertical = 2;
        }
        if (horizontalMovement != 0 || verticalMovement != 0)
            animator.applyRootMotion = true;
        animator.SetFloat("Horizontal", snappedHorizontal, 0.1f, Time.deltaTime);
        animator.SetFloat("Vertical", snappedVertical, 0.1f, Time.deltaTime);
    }

    public void ClearHandIKWeights()
    {
        rightHandIK.data.targetPositionWeight = 0;
        rightHandIK.data.targetRotationWeight = 0;

        leftHandIK.data.targetPositionWeight = 0;
        leftHandIK.data.targetRotationWeight = 0;
    }

    public void RefreshHandIKWeights()
    {
        rightHandIK.data.targetPositionWeight = 1;
        rightHandIK.data.targetRotationWeight = 1;

        leftHandIK.data.targetPositionWeight = 1;
        leftHandIK.data.targetRotationWeight = 1;
    }

    public void OnAnimationMove()
    {
        Vector3 animatorDeltaPos = animator.deltaPosition;
        animatorDeltaPos.y = 0;

        Vector3 velocity = animatorDeltaPos / Time.deltaTime;
        playerController.playerRigidbody.linearDamping = 0;
        playerController.playerRigidbody.linearVelocity = velocity;
        transform.rotation *= animator.deltaRotation;
    }


    public void AssignHandIK(RightHandIKTarget rightHand, LeftHandIKTarget leftHand)
    {
        leftHandIK.data.target = leftHand.transform;
        rightHandIK.data.target = rightHand.transform;
        rigBuilder.Build();
    }


    public void UpdateAimConstraints()
    {
        if (playerController.isAiming)
        {
            spine01.weight = .3f;
            spine02.weight = .3f;
            head.weight = .7f;
        }
        else
        {
            spine01.weight = 0;
            spine02.weight = 0;
            head.weight = 0;
        }
    }


}

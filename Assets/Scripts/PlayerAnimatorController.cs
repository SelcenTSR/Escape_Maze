using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    Animator animator;
    float snappedHorizontal;
    float snappedVertical;
    PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
    }
    public void PlayAnimationWithoutRootMotion(string targetAnimation)
    {
        animator.applyRootMotion = false;
        animator.CrossFade(targetAnimation, .2f);
    }
    public void HandleAnimatorValues(float horizontalMovement,float verticalMovement,bool isRunning)
    {
        if (horizontalMovement > 0)
        {
            snappedHorizontal = 1;
        }
        else if(horizontalMovement < 0 )
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

        if (isRunning && snappedVertical>0)
        {
            snappedVertical = 2;
        }

        animator.SetFloat("Horizontal", snappedHorizontal, 0.1f, Time.deltaTime);
        animator.SetFloat("Vertical", snappedVertical, 0.1f, Time.deltaTime);
    }

    public void OnAnimationMove()
    {
        Vector3 animatorDeltaPos = animator.deltaPosition;
        animatorDeltaPos.y = 0;

        Vector3 velocity = animatorDeltaPos / Time.deltaTime;
        playerController.playerRigidbody.drag = 0;
        playerController.playerRigidbody.velocity = velocity;
        transform.rotation *= animator.deltaRotation;
    }
}

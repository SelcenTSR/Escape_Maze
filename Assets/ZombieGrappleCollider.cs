using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieGrappleCollider : MonoBehaviour
{
    ZombieManager zombie;
    public Collider grappleCollider;
    public bool isRightHandedGrappleCollider;
    // Start is called before the first frame update
    void Start()
    {
        zombie = GetComponentInParent<ZombieManager>();
        grappleCollider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController == null) return;
            if (!playerController.isPerformingAction)
            {
                zombie.zombieAnimatorManager.PlayGrappleAnimation("Grapple", true);
                zombie.animator.SetFloat("Vertical", 0); // yürümeyi durdur

                playerController.playerAnimator.PlayAnimation("PlayerGrapple", true);
                playerController.playerStatManager.pendingDamage = zombie.zombieCombatManager.grappleDamage;
                playerController.playerAnimator.ClearHandIKWeights();


                Quaternion targetZombieRotation = Quaternion.LookRotation(playerController.transform.position-zombie.transform.position);
                zombie.transform.rotation = targetZombieRotation;

                Quaternion targetPlayerRotation = Quaternion.LookRotation(zombie.transform.position - playerController.transform.position);
                playerController.transform.rotation = targetPlayerRotation;
            }
        }
    }
}

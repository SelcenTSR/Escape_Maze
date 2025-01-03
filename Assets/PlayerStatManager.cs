using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatManager : MonoBehaviour
{
    [Header("Health")]
    public int playerHealth = 100;

    public int pendingDamage;


    PlayerController player;

    private void Awake()
    {
        player = GetComponent<PlayerController>();
    }

    public void TakeDamage()
    {
        player.BloodExplosion();
        playerHealth = playerHealth - pendingDamage;
        if (playerHealth <= 0)
        {
            Dead();
        }
        else
        {
            player.playerUIManager.DisplayHealthPopUp();
        }
    }

    private void Dead()
    {
        if (player.isPerformingAction)
        {
            player.playerAnimator.PlayAnimation("Player Dead", true);
        }


        player.isDead = true;
        player.playerUIManager.gameOverPanel.SetActive(true);
        player.BloodPool();
    }
}

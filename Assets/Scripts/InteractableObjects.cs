using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjects : MonoBehaviour
{
    protected PlayerController player;
    [SerializeField] protected GameObject interactableCanvas;
    protected Collider interactableCollider;

    private void OnTriggerEnter(Collider other)
    {
        if (player == null)
        {
            player = other.GetComponent<PlayerController>();
        }
        if (player != null)
        {
            interactableCanvas.SetActive(true);
            player.canInteract = true;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (player == null)
        {
            if (player.inputController.interactionInput)
            {
                Interact(player);
                player.inputController.interactionInput = false;
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (player == null)
        {
            player = other.GetComponent<PlayerController>();
        }
        if (player != null)
        {
            interactableCanvas.SetActive(false);
            player.canInteract = false;
        }
    }

    protected virtual void Interact(PlayerController player)
    {
        Debug.Log("interacted");
    }
}

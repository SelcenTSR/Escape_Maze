using UnityEngine;

public class AmmoBoxItem : InteractableObjects
{
    [SerializeField] BoxOfAmmoItem boxOfAmmoItem;
    protected override void Interact(PlayerController player)
    {
        base.Interact(player);
        player.playerInventoryManager.currentAmmoInInventory = boxOfAmmoItem;
        player.playerInventoryManager.currentAmmoCountInInventory += player.playerInventoryManager.currentAmmoInInventory.boxOfAmmoCapacity;
        player.playerEquipmentManager.SetAmmoEquipment();
        Destroy(gameObject);
    }
}

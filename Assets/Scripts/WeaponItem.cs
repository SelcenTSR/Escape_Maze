using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Weapon Item")]

public class WeaponItem : Item
{
    [Header("Weapon Animation")]
    public AnimatorOverrideController weaponAnimator;

    public int damage=20;

    [Header("Ammo")]

    public int remainingAmmo;
    public int maxAmmo=12;
    public AmmoType ammoType;
}

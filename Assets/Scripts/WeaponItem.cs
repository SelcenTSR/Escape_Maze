using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Weapon Item")]

public class WeaponItem : Item
{
    [Header("Weapon Animation")]
    public AnimatorOverrideController weaponAnimator;

}

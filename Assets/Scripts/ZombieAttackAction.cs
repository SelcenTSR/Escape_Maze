using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Zombie Attack Action")]
public class ZombieAttackAction : ScriptableObject
{
    [Header("Attack Animation")]
    public string attackAnimation;

    [Header("Attack Cooldown")]
    public float attackCoolDown = 5f; // başka bir saldırı için bekleme süresi

    [Header("Attack Angles & Distances")]
    public float maxAttackAngle = 25f; // zombienin saldırı için player'a maximum bakış açısı
    public float minAttackAngle = -25f;
    public float maxAttackDistance = 1f;
    public float minAttackDistance = 6f;

}

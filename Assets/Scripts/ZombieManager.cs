using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class ZombieManager : MonoBehaviour
{
    public ZombieCombatManager zombieCombatManager;
    public ZombieAnimatorManager zombieAnimatorManager;
    public ZombieStatManager zombieStatManager;
    [SerializeField] private State currentState;
    public IdleState startingState;

    public bool isPerformingAction;
    public bool isDead;
    public bool canRotate;

    public PlayerController currentTarget;
    public float distanceFromCurrentTarget;

    public Animator animator;

    public NavMeshAgent navMeshAgent;

    public Rigidbody rigidbody;

    public float rotationSpeed=5;

    public float attackCoolDownTimer;
    public float maxAttackDistance = 3.5f;
    public float minAttackDistance = 1;

    public float viewableAngleFromCurrentTarget = 1;
    public Vector3 targetDirection;

    private void Awake()
    {
        zombieCombatManager = GetComponent<ZombieCombatManager>();
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponentInChildren<NavMeshAgent>();
        currentState = startingState;
        zombieAnimatorManager = GetComponent<ZombieAnimatorManager>();
        zombieStatManager = GetComponent<ZombieStatManager>();
    }

    private void FixedUpdate()
    {
        if (!isDead)
        {
            HandleStateMachine();
        }
       
    }

    private void Update()
    {
        navMeshAgent.transform.localPosition = Vector3.zero;
        if (attackCoolDownTimer > 0)
        {
            attackCoolDownTimer -= Time.deltaTime;
        }

        if (currentTarget != null)
        {
            targetDirection = currentTarget.transform.position - transform.position;
            viewableAngleFromCurrentTarget = Vector3.SignedAngle(targetDirection, transform.forward, Vector3.up);
            distanceFromCurrentTarget = Vector3.Distance(currentTarget.transform.position, transform.position);
        }
    }

    private void HandleStateMachine()
    {
        State nextState;
        if (currentState != null)
        {
            nextState = currentState.Tick(this);
            if (nextState != null)
            {
                currentState = nextState;
            }
        }

    }

 

  
}

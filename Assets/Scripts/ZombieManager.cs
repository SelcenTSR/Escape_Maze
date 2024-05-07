using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class ZombieManager : MonoBehaviour
{
    [SerializeField] private State currentState;
    public IdleState startingState;

    public PlayerController currentTarget;
    public float distanceFromCurrentTarget;

    public Animator animator;

    public NavMeshAgent navMeshAgent;

    public Rigidbody rigidbody;

    public float rotationSpeed=5;


    public float minAttackDistance = 1;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponentInChildren<NavMeshAgent>();
        currentState = startingState;
    }

    private void FixedUpdate()
    {
        HandleStateMachine();
    }

    private void Update()
    {
        navMeshAgent.transform.localPosition = Vector3.zero;
        if (currentTarget != null)
        {
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

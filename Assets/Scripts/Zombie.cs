using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : Damagable
{

    [SerializeField] private float minDistanceToChase;
    [SerializeField] private float attackRange;
    [SerializeField] private int attackDamage;
    [SerializeField] private float walkSpeed = 2f;
    [SerializeField] private float chaseSpeed = 5f;

    private NavMeshAgent navMeshAgent;
    private Transform target;
    private Animator animator;

    private int isMovingHash;
    private int deadHash;
    private int attackHash;

    public static Action<Zombie> OnDead;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        SetCurrentHealthToMax();

        isMovingHash = Animator.StringToHash("isMoving");
        deadHash = Animator.StringToHash("Dead");
        attackHash = Animator.StringToHash("Attack");
    }

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;

        navMeshAgent.speed = walkSpeed;
    }

    private void Update()
    {
        Vector3 distance = (transform.position - target.position);
        float distanceSquared = distance.sqrMagnitude;

        navMeshAgent.SetDestination(target.position);

        if (distanceSquared < minDistanceToChase * minDistanceToChase)
        {
            navMeshAgent.isStopped = false;
            animator.SetBool(isMovingHash, true);
            navMeshAgent.speed = chaseSpeed;
        }
        else
        {
            navMeshAgent.speed = walkSpeed;
        }

        if (distanceSquared < attackRange * attackRange)
        {
            animator.SetTrigger(attackHash);
        }

        if (CheckIfDead())
        {
            navMeshAgent.isStopped = true;
            animator.SetTrigger(deadHash);
            OnDead?.Invoke(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerHealth playerHealth))
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AI_Controller : MonoBehaviour
{
    public enum State { Idle, Chase, Attack }

    [SerializeField] private State currentState = State.Idle;
    [SerializeField] private Player target;

    [SerializeField] private float chaseRange, attackRange;
    [SerializeField] private bool inChaseRange, inAttackRange;

    [SerializeField] private LayerMask targetLayer;

    public State CurrentState => currentState;
    public bool IsTargetDetected { get; private set; }
    public float AgentSpeed { get => agent.speed; set => agent.speed = value; }

    private NavMeshAgent agent;
    private Warrior controlledWarrior;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        controlledWarrior = GetComponent<Warrior>();
        target = FindObjectOfType<Player>();
    }
    private void Update()
    {
        if (target.IsDead)
        {
            Idle();
            return;
        }

        inChaseRange = Physics.CheckSphere(transform.position, chaseRange, targetLayer);
        inAttackRange = Physics.CheckSphere(transform.position, attackRange, targetLayer);

        if (inChaseRange && !inAttackRange && controlledWarrior.IsAttackEnded) MoveTo(target);
        if (inChaseRange && inAttackRange) Attack();
        if (!inChaseRange && !inAttackRange) Idle();
    }

    private void Idle()
    {
        agent.ResetPath();
        currentState = State.Idle;
    }

    private void Attack()
    {
        agent.velocity = Vector3.zero;
        agent.ResetPath();
        currentState = State.Attack;
    }

    public void MoveTo(Warrior target)
    {
        currentState = State.Chase;
        agent.SetDestination(target.transform.position);
    }
    public void MoveTo(Vector3 position)
    {
        agent.SetDestination(position);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}

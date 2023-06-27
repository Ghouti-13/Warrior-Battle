using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Warrior
{
    public static event Action OnEnemyDeath = null;

    [Space(10)]
    [SerializeField] private HealthBarManager _healthManager;

    private Animator warriorAnimator;
    private AI_Controller warriorAI;
    private Player target;

    private void Awake()
    {
        warriorAnimator = GetComponentInChildren<Animator>();
        warriorAI = GetComponent<AI_Controller>();
        target = FindObjectOfType<Player>();
    }
    protected override void Start()
    {
        base.Start();
        warriorAI.AgentSpeed = moveSpeed;
        _healthManager.SetMaxHealth(maxHealth);
    }
    private void Update()
    {
        if (isDead)
        {
            warriorAI.enabled = false;
            return;
        }

        switch (warriorAI.CurrentState)
        {
            case AI_Controller.State.Idle:
                Stop();
                break;

            case AI_Controller.State.Chase:
                Move();
                break;

            case AI_Controller.State.Attack:
                if (IsAttacking) break;
                Attack();
                break;
        }
    }

    private void Stop()
    {
        warriorAnimator.SetBool("Moving", false);
    }

    protected override void Move()
    {
        warriorAnimator.Play("Movement Blend");
        warriorAnimator.SetBool("Moving", true);
        warriorAnimator.SetFloat("Velocity", 1f);
    }
    protected override void Attack()
    {
        base.Attack();

        IsAttacking = true;
        isAttackEnded = false;
        transform.LookAt(target.transform);
        warriorAnimator.SetBool("Moving", false);
        warriorAnimator.SetFloat("Velocity", 0f);
        warriorAnimator.SetInteger("Trigger Number", 2);
        warriorAnimator.SetTrigger("Trigger");
    }
    public override void TakeDamage(int damage, AudioSource warriorAudio)
    {
        base.TakeDamage(damage, warriorAudio);
        _healthManager.SetHealthValue(currentHealth);
    }
    protected override void Die()
    {
        isDead = true;
        OnEnemyDeath?.Invoke();

        warriorAnimator.SetTrigger("Die");
        Destroy(gameObject, 2f);
    }
    public void EndingAttack()
    {
        isAttackEnded = true;
    }
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && IsAttacking)
        {
            if (!other.TryGetComponent<Player>(out Player player)) return;

            player.TakeDamage(damage, _warriorAudio);
        }
    }
}

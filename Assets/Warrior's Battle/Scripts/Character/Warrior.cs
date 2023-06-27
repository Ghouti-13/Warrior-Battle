using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : MonoBehaviour
{
    [SerializeField] protected int maxHealth;
    [SerializeField] protected int damage;
    [SerializeField] protected int moveSpeed;
    [SerializeField] protected bool isAttacking;
    [SerializeField] protected bool isAttackEnded;
    [Space(10)]
    [Header("ENEMY ATTACK TIMER")]
    [SerializeField] protected float timeBetweenAttack;

    protected AudioSource _warriorAudio;

    protected int currentHealth;
    protected bool isDead = false;

    public int Damage => damage;
    public bool IsAttacking { get => isAttacking; set => isAttacking = value; }
    public bool IsAttackEnded { get => isAttackEnded; }

    protected virtual void Start()
    {
        _warriorAudio = GetComponent<AudioSource>();

        currentHealth = maxHealth;
        isAttackEnded = true;
    }
    protected virtual void Move()
    {

    }
    protected virtual void Attack()
    {
        Invoke(nameof(ResetAttack), timeBetweenAttack);
    }
    public virtual void TakeDamage(int damage, AudioSource warriorAudio)
    {
        if (isDead) return;

        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);

        warriorAudio.pitch = UnityEngine.Random.Range(0.8f, 1.1f);
        warriorAudio?.Play();

        // DEBUG //
        print(gameObject.name + " Health is " + currentHealth + " / " + maxHealth);

        if (currentHealth == 0) Die();
    }

    protected virtual void Die()
    {
        isDead = true;
    }

    private void ResetAttack()
    {
        IsAttacking = false;
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        // We trigger if we're attacking only.
        if (!IsAttacking) return;

        if (!other.TryGetComponent<Warrior>(out Warrior target)) return;

        target.TakeDamage(damage, _warriorAudio);
    }
}

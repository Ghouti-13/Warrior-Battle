using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Warrior
{
    [SerializeField] private HealthBarManager healthManager;

    private Animator _animator;

    public bool IsDead => isDead;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        healthManager.SetMaxHealth(maxHealth);
    }
    public override void TakeDamage(int damage, AudioSource warriorAudio)
    {
        base.TakeDamage(damage, warriorAudio);

        healthManager.SetHealthValue(currentHealth);

        if (currentHealth == 0) 
        {
            isDead = true;
            _animator.SetTrigger("Die");
            GameManager.Instance.EndGame(false);
        }
    }
}

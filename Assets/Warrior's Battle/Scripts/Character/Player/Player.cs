using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Warrior
{
    private Animator _animator;

    public bool IsDead => isDead;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }
    public override void TakeDamage(int damage, AudioSource warriorAudio)
    {
        base.TakeDamage(damage, warriorAudio);

        if (currentHealth == 0) 
        {
            isDead = true;
            _animator.SetTrigger("Die");
            GameManager.Instance.EndGame(false);
        }
    }
}

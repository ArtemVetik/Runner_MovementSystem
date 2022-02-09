using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatedObstacle : Obstacle
{
    [SerializeField] private float _damageForce;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    protected override void OnDamage(IDamageable damageable)
    {
        damageable.TakeDamage(_damageForce);
        _animator.enabled = false;
    }
}

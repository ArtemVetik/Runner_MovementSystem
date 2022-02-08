using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PillarsObstacle : Obstacle
{
    [SerializeField] private float _damageForce;

    private Animator _selfAnimator;

    private void Awake()
    {
        _selfAnimator = GetComponent<Animator>();
    }

    protected override void OnDamage(IDamageable damageable)
    {
        damageable.TakeDamage(_damageForce);
        _selfAnimator.SetTrigger(AnimationParams.Stop);
    }

    private static class AnimationParams
    {
        public static readonly string Stop = nameof(Stop);
    }
}

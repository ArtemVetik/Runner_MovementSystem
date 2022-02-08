using UnityEngine;

[RequireComponent(typeof(Animator))]
public class RotatorObstacle : Obstacle
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
        _selfAnimator.enabled = false;
    }
}

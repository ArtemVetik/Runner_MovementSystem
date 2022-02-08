using UnityEngine;

public abstract class Obstacle : MonoBehaviour
{
    [SerializeField] private ObstacleTrigger[] _triggers;

    private void OnEnable()
    {
        foreach (var trigger in _triggers)
            trigger.Triggered += OnDamage;
    }

    private void OnDisable()
    {
        foreach (var trigger in _triggers)
            trigger.Triggered += OnDamage;
    }

    protected abstract void OnDamage(IDamageable damageable);
}

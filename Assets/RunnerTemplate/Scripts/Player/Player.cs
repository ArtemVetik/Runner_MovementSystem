using RunnerMovementSystem;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    private Health _health;

    public float Health => _health.Value;

    private void Awake()
    {
        _health = new Health(100f);
    }

    private void OnEnable()
    {
        _health.Died += OnDied;
    }

    private void OnDisable()
    {
        _health.Died += OnDied;
    }

    public void TakeDamage(float damage)
    {
        _health.TakeDamage(damage);
        Debug.Log(name + ": Take Damage " + damage);
    }

    private void OnDied()
    {
        Debug.Log(name + " died!");
    }
}

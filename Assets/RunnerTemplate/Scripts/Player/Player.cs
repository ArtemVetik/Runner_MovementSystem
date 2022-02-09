using RunnerMovementSystem;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] private MovementSystem _movementSystem;

    private Health _health;

    public event UnityAction Died;

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
        _movementSystem.enabled = false;
        Died?.Invoke();
        Debug.Log(name + " died!");
    }
}

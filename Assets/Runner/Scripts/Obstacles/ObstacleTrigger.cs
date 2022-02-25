using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class ObstacleTrigger : MonoBehaviour
{
    public event UnityAction<IDamageable> Triggered;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageable damageable))
            Triggered?.Invoke(damageable);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out IDamageable damageable))
            Triggered?.Invoke(damageable);
    }
}

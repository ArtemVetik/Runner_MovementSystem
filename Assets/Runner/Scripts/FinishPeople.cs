using System.Threading.Tasks;
using UnityEngine;

public class FinishPeople : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    public async void Dance()
    {
        await Task.Delay(Random.Range(0, 1000));

        _animator.SetTrigger(AnimationParams.Chicken);
    }

    private static class AnimationParams
    {
        public static readonly string Chicken = nameof(Chicken);
    }
}

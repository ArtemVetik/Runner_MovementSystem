using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class LoseCanvas : MonoBehaviour
{
    [SerializeField] private Player _player;

    private Canvas _canvas;
    private Animator _animator;

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _player.Died += OnPlayerDied;
    }

    private void OnDisable()
    {
        _player.Died -= OnPlayerDied;
    }

    private async void OnPlayerDied()
    {
        await Task.Delay(1000);
        _canvas.enabled = true;
        _animator.SetTrigger(AnimationParams.Show);
    }

    private static class AnimationParams
    {
        public static readonly string Show = nameof(Show);
    }
}

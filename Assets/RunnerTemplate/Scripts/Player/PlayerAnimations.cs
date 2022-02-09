using RunnerMovementSystem;
using UnityEngine;
using RunnerMovementSystem.Examples;

[RequireComponent(typeof(Animator))]
public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private MouseInput _input;
    [SerializeField] private MovementSystem _movement;

    private Animator _animator;
    private bool _died = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _player.Died += OnPlayerDied;
        _input.MoveStarted += OnMoveStarted;
        _input.MoveEnded += OnMoveEnded;
        _movement.PathChanged += OnPathChanged;
    }

    private void OnDisable()
    {
        _player.Died -= OnPlayerDied;
        _input.MoveStarted -= OnMoveStarted;
        _input.MoveEnded -= OnMoveEnded;
        _movement.PathChanged -= OnPathChanged;
    }

    private void OnPlayerDied()
    {
        _died = true;
        _animator.SetTrigger(AnimationParams.Death);
    }

    private void OnMoveStarted()
    {
        if (_died || _movement.IsOnTransition)
            return;

        _animator.SetTrigger(AnimationParams.Running);
    }

    private void OnMoveEnded()
    {
        if (_died || _movement.IsOnTransition)
            return;

        _animator.SetTrigger(AnimationParams.DynIdle);
    }

    private void OnPathChanged(PathSegment pathSegment)
    {
        if (_movement.IsOnTransition)
            _animator.SetTrigger(AnimationParams.Jumping);
        else
            _animator.SetTrigger(AnimationParams.Landing);
    }

    private static class AnimationParams
    {
        public static readonly string Death = nameof(Death);
        public static readonly string Running = nameof(Running);
        public static readonly string DynIdle = nameof(DynIdle);
        public static readonly string Jumping = nameof(Jumping);
        public static readonly string Landing = nameof(Landing);
    }
}

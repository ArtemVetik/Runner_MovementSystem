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

    public void Walk()
    {
        _animator.SetTrigger(AnimationParams.Walking);
    }

    public void ChickenDance()
    {
        _animator.SetTrigger(AnimationParams.Chicken);
    }

    private void OnPlayerDied()
    {
        _animator.SetTrigger(AnimationParams.Death);
    }

    private void OnMoveStarted()
    {
        if (_movement.enabled == false || _movement.IsOnTransition)
            return;

        _animator.SetTrigger(AnimationParams.Running);
    }

    private void OnMoveEnded()
    {
        if (_movement.enabled == false || _movement.IsOnTransition)
            return;

        _animator.SetTrigger(AnimationParams.DynIdle);
    }
    
    private void OnPathChanged(PathSegment pathSegment)
    {
        _animator.SetBool(AnimationParams.Jumping, _movement.IsOnTransition);

        if (_movement.IsOnTransition == false && _input.IsMoved)
            _animator.SetTrigger(AnimationParams.Running);
    }

    private static class AnimationParams
    {
        public static readonly string Death = nameof(Death);
        public static readonly string Running = nameof(Running);
        public static readonly string DynIdle = nameof(DynIdle);
        public static readonly string Jumping = nameof(Jumping);
        public static readonly string StartJump = nameof(StartJump);
        public static readonly string Chicken = nameof(Chicken);
        public static readonly string Walking = nameof(Walking);
    }
}

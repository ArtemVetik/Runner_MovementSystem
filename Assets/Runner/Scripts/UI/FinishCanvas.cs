using UnityEngine;

[RequireComponent(typeof(Canvas))]
[RequireComponent(typeof(Animator))]
public class FinishCanvas : MonoBehaviour
{
    [SerializeField] private PlayerFinishWalking _finishWalk;

    private Canvas _canvas;
    private Animator _animator;

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _finishWalk.WalkEnded += OnWalkEnded;
    }

    private void OnDisable()
    {
        _finishWalk.WalkEnded += OnWalkEnded;
    }

    private void OnWalkEnded()
    {
        _canvas.enabled = true;
        _animator.SetTrigger(AnimationParams.Show);
    }

    private static class AnimationParams
    {
        public static readonly string Show = nameof(Show);
    }
}

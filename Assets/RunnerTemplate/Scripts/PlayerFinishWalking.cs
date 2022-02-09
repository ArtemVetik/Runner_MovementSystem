using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerFinishWalking : MonoBehaviour
{
    [SerializeField] private float _walkSpeed;
    [SerializeField] private Transform _targetWalkPoint;
    [SerializeField] private FinishTrigger _trigger;
    [SerializeField] private PlayerAnimations _animations;
    [SerializeField] private Transform _player;

    public event UnityAction WalkEnded;

    private void OnEnable()
    {
        _trigger.Finished += OnFinished;
        WalkEnded += OnWalkEnded;
    }

    private void OnDisable()
    {
        _trigger.Finished -= OnFinished;
        WalkEnded -= OnWalkEnded;
    }

    private void OnFinished()
    {
        _animations.Walk();

        StartCoroutine(FinishWalk());
    }

    private IEnumerator FinishWalk()
    {
        while (_player.position != _targetWalkPoint.position)
        {
            _player.position = Vector3.MoveTowards(_player.position, _targetWalkPoint.position, _walkSpeed * Time.deltaTime);
            yield return null;
        }

        WalkEnded?.Invoke();
    }

    private void OnWalkEnded()
    {
        _animations.ChickenDance();
    }
}

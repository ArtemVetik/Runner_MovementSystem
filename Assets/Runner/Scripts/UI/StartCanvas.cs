using RunnerMovementSystem.Examples;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class StartCanvas : MonoBehaviour
{
    [SerializeField] private MouseInput _input;

    private Canvas _canvas;

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();
    }

    private void OnEnable()
    {
        _input.MoveStarted += OnMoveStarted;
    }

    private void OnDisable()
    {
        _input.MoveStarted += OnMoveStarted;
    }

    private void OnMoveStarted()
    {
        _canvas.enabled = false;
    }
}

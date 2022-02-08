using RunnerMovementSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllStatsView : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private MovementSystem _movementSystem;
    [Header("Settings")]
    [SerializeField] private Font _font;
    [SerializeField] private int _fontSize;

    private Text _playerHealh;
    private Text _currentOffset;
    private Text _maxSpeed;
    private Text _isOnTransition;

    private void Start()
    {
        _playerHealh = Create("PlayerStat");
        _currentOffset = Create("CurrentOffset");
        _maxSpeed = Create("MaxSpeed");
        _isOnTransition = Create("IsOnTransition");
    }

    private void Update()
    {
        _playerHealh.text = $"Player Health: {_player.Health}";
        _currentOffset.text = $"Move offset: {_movementSystem.Offset}";
        _maxSpeed.text = $"Max speed: {_movementSystem.CurrentSpeed}";
        _isOnTransition.text = $"Is On Transition: {_movementSystem.IsOnTransition}";
    }

    private Text Create(string objectName = "New Game Object")
    {
        var text = new GameObject(objectName).AddComponent<Text>();
        text.transform.parent = transform;

        text.font = _font;
        text.fontSize = _fontSize;

        return text;
    }
}

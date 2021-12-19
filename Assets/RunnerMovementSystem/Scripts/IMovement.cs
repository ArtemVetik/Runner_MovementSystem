using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RunnerMovementSystem
{
    public interface IMovement
    {
        float Offset { get; }

        void MoveForward();
        void SetOffset(float offset);
    }
}
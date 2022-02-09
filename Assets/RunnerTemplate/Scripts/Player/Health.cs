using System;

public class Health
{
    public readonly float MaxValue;

    public Health(float maxValue)
    {
        MaxValue = maxValue;
        Value = maxValue;
    }

    public event Action Died;

    public float Value { get; private set; }

    public void TakeDamage(float value)
    {
        Value -= value;

        if (Value <= 0)
        {
            Value = 0;
            Died?.Invoke();
        }
    }
}

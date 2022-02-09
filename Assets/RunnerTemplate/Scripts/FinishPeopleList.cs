using UnityEngine;

public class FinishPeopleList : MonoBehaviour
{
    [SerializeField] private FinishTrigger _finishTrigger;

    private FinishPeople[] _finishPeople;

    private void Awake()
    {
        _finishPeople = GetComponentsInChildren<FinishPeople>();
    }

    private void OnEnable()
    {
        _finishTrigger.Finished += OnFinished;
    }

    private void OnDisable()
    {
        _finishTrigger.Finished -= OnFinished;
    }

    private void OnFinished()
    {
        foreach (var people in _finishPeople)
            people.Dance();
    }
}

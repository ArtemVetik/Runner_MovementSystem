using System.Collections;
using UnityEngine;

public class FinishConfetti : MonoBehaviour
{
    [SerializeField] private FinishTrigger _trigger;
    [SerializeField] private ParticleSystem[] _confettiTemplates;
    [SerializeField] private Transform[] _spawnPositions;

    private Coroutine _spawnLoop;

    private void OnEnable()
    {
        _trigger.Finished += OnFinished;
    }

    private void OnDisable()
    {
        _trigger.Finished -= OnFinished;
    }

    private void OnFinished()
    {
        StartSpawn();
    }

    public void StartSpawn()
    {
        if (_spawnLoop != null)
            StopCoroutine(_spawnLoop);

        _spawnLoop = StartCoroutine(SpawnLoop());
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            var confetti = _confettiTemplates[Random.Range(0, _confettiTemplates.Length)];
            var spawnPosition = _spawnPositions[Random.Range(0, _spawnPositions.Length)];

            var inst = Instantiate(confetti, transform);
            inst.transform.position = spawnPosition.position;

            yield return new WaitForSeconds(Random.Range(0.2f, 1f));
        }
    }
}

using UnityEngine;
using UnityEngine.Events;

public class Score : MonoBehaviour
{
    [SerializeField] private TriggerTilemap _triger;
    [SerializeField] private GameController _gameController;

    public int Points { get; private set; }

    public event UnityAction Changed;

    private void OnEnable()
    {
        _triger.Entered += OnTriggerEntered;
        _gameController.GameStarted += OnGameStarted;
    }

    private void OnDisable()
    {
        _triger.Entered -= OnTriggerEntered;
        _gameController.GameStarted -= OnGameStarted;
    }

    private void OnTriggerEntered(Vector3 vector)
    {
        Points++;
        Changed?.Invoke();
    }

    private void OnGameStarted()
    {
        Points = 0;
        Changed?.Invoke();
    }
}

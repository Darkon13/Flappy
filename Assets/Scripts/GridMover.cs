using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Grid))]
public class GridMover : MonoBehaviour
{
    [SerializeField] public int _lenght;
    [SerializeField] public float _duration;
    [SerializeField] public float _tickPerSecond;
    [SerializeField] public GameController _gameController;

    public int Lenght => _lenght;

    public event UnityAction Moved;
    public event UnityAction Inited;
  
    private Transform _transform;
    private Coroutine _coroutine;
    private Vector3 _startPosition;

    void Start()
    {
        _transform = GetComponent<Transform>();
        _startPosition = _transform.position;
    }

    private void OnEnable()
    {
        Moved += OnMoved;
        _gameController.GameStarted += OnGameStarted;
    }

    private void OnDisable()
    {
        Moved -= OnMoved;
        _gameController.GameStarted -= OnGameStarted;
    }

    private void OnGameStarted()
    {
        _transform.position = _startPosition;

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(nameof(Move));

        Inited?.Invoke();
    }

    private IEnumerator Move()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_duration/_tickPerSecond);
        float startPosX = _transform.position.x;
        float nextX = startPosX - _lenght;
        float delta = 1f/_tickPerSecond;
        float current = 0;

        while(_transform.position.x != nextX)
        {
            current += delta;
            _transform.position = new Vector3(Mathf.Max(Mathf.Lerp(startPosX, nextX, current), nextX), _transform.position.y, _transform.position.z);

            yield return waitForSeconds;
        }

        Moved?.Invoke();
    }

    private void OnMoved()
    {
        if(_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(nameof(Move));
    }
}

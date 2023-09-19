using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Grid))]
public class GridMover : MonoBehaviour
{
    [SerializeField] public int _lenght;
    [SerializeField] public float _duration;
    [SerializeField] public float _tickPerSecond;

    public int Lenght => _lenght;
    public event UnityAction Moved;

    private Transform _transform;
    private Coroutine _coroutine;

    void Start()
    {
        _transform = GetComponent<Transform>();

        Moved += OnMoved;

        _coroutine = StartCoroutine(nameof(Move));
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

        Debug.Log("Moved");
    }
}

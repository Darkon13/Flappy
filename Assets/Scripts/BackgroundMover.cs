using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class BackgroundMover : MonoBehaviour
{
    [SerializeField] public float _duration;
    [SerializeField] public float _tickPerSecond;
    [SerializeField] public GameController _gameController;

    private RawImage _image;
    private Coroutine _coroutine;
    private float _startX;

    void Start()
    {
        _image = GetComponent<RawImage>();
        _startX = 0;
    }

    private void OnEnable()
    {
        _gameController.GameStarted += OnGameStarted;
    }

    private void OnDisable()
    {
        _gameController.GameStarted -= OnGameStarted;
    }

    private void OnGameStarted()
    {
        ChangeUVRectX(_startX);

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(nameof(Move));
    }

    private IEnumerator Move()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_duration / _tickPerSecond);
        float delta = 1f / _tickPerSecond;
        float current = 0;
        float target = 1;

        while (_image.uvRect.x != target)
        {
            current += delta;
            ChangeUVRectX(current % target);

            yield return waitForSeconds;
        }

        ChangeUVRectX(_startX);
        StartMove();
    }

    private void ChangeUVRectX(float x) => _image.uvRect = new Rect(x, _image.uvRect.y, _image.uvRect.width, _image.uvRect.height);

    private void StartMove()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(nameof(Move));
    }
}

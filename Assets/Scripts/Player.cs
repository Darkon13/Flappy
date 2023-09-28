using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _minAngle;
    [SerializeField] private float _maxAngle;
    [SerializeField] private float _angleForce;
    [SerializeField] private GameController _gameController;

    private Rigidbody2D _playerRigidbody;
    private Transform _transform;
    private bool _isAlive;
    private bool _isAwake;
    private Vector3 _startPosition;
    private Quaternion _startAngle;

    public event UnityAction Died;
    public event UnityAction Awaked;

    private void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody2D>();
        _transform = GetComponent<Transform>();

        _startPosition = transform.position;
        _startAngle = transform.rotation;

        _isAlive = false;
    }

    private void OnEnable()
    {
        _gameController.GameStarted += OnGameStarted;
    }

    private void OnDisable()
    {
        _gameController.GameStarted -= OnGameStarted;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _isAlive = false;
        Died?.Invoke();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (_isAwake)
        {
            Rotate(-_angleForce * Time.deltaTime);
        }
    }

    private void OnGameStarted()
    {
        _isAlive = true;
        _isAwake = false;
        _transform.position = _startPosition;
        _transform.rotation = _startAngle;
    }

    private void Rotate(float angle)
    {
        Vector3 currentAngle = _transform.rotation.eulerAngles;

        _transform.rotation = Quaternion.Euler(currentAngle.x, currentAngle.y, Mathf.Clamp(GetSignedAngle(currentAngle.z) + angle, _minAngle, _maxAngle));
    }

    private float GetSignedAngle(float angle)
    {
        angle = angle % 360;

        if (angle >= 180)
        {
            return -(360 - angle);
        }
        else
        {
            return angle;
        }
    }

    private void Jump()
    {
        if (_isAlive)
        {
            if (_isAwake == false)
            {
                _isAwake = true;
                Awaked?.Invoke();
            }

            _playerRigidbody.velocity = Vector2.zero;
            _playerRigidbody.AddForce(Vector2.up * _jumpForce);

            Rotate(_angleForce);
        }
    }
}

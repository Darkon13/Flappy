using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent (typeof(CircleCollider2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _minAngle;
    [SerializeField] private float _maxAngle;
    [SerializeField] private float _angleForce;
    private Rigidbody2D _playerRigidbody;
    private bool _isAlive;

    public event UnityAction Died;

    void Start()
    {
        _isAlive = true;
        _playerRigidbody = GetComponent<Rigidbody2D>();
        _playerRigidbody.Sleep();
        //Died += OnDead;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
            //transform.rotation = new Quaternion(0, 0, 0.1f, 0);

        }

        if (_playerRigidbody.IsAwake())
        {
            Rotate(-_angleForce * Time.deltaTime);
        }
    }

    private void Rotate(float angle)
    {
        Vector3 currentAngle = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(currentAngle.x, currentAngle.y, Mathf.Clamp(GetSignedAngle(currentAngle.z) + angle, _minAngle, _maxAngle));
        //Debug.Log(currentAngle);
        //Debug.Log(angle);
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
            _playerRigidbody.velocity = Vector2.zero;
            _playerRigidbody.AddForce(Vector2.up * _jumpForce);
            Rotate(_angleForce);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _isAlive = false;
        Died?.Invoke();
    }

    //private void OnDead()
    //{
    //    Debug.Log("Ya sdox");
    //}
}

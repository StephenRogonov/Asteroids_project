using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;

    private Rigidbody2D _rigidbody;
    private Transform _player;
    private Vector2 _playerDirection;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _player = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        GetPlayerDirection();
    }

    private void FixedUpdate()
    {
        RotateTowardsPlayer();
        SetVelocity();
    }

    private void GetPlayerDirection()
    {
        _playerDirection = (_player.position - transform.position).normalized;
    }

    private void RotateTowardsPlayer()
    {
        if (_playerDirection == Vector2.zero)
        {
            return;
        }

        Quaternion playerRotation = Quaternion.LookRotation(transform.forward, _playerDirection);
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, playerRotation, _rotationSpeed * Time.deltaTime);

        _rigidbody.SetRotation(rotation);
    }

    private void SetVelocity()
    {
        if (_playerDirection == Vector2.zero)
        {
            _rigidbody.velocity = Vector2.zero;
        }
        else
        {
            _rigidbody.velocity = transform.up * _speed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Missile"))
        {
            Destroy(gameObject);
        }
    }
}

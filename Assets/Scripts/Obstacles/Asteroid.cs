using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float destroyTimeout = 30f;
    [SerializeField] private float shardSize = 0.75f;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        if (gameObject.transform.localScale.x > shardSize)
        {
            Move(transform.up);
        }
        else
        {
            Move(Random.insideUnitCircle.normalized);
        }

        Destroy(gameObject, destroyTimeout);
    }

    private void Move(Vector2 direction)
    {
        _rigidbody.AddForce(direction * speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Missile"))
        {
            if (gameObject.transform.localScale.x > shardSize)
            {
                CreateShard();
                CreateShard();
            }

            Destroy(gameObject);
        }
    }

    private void CreateShard()
    {
        Vector2 position = transform.position;
        position += Random.insideUnitCircle * 0.5f;

        Asteroid shard = Instantiate(this, position, transform.rotation);
        shard.transform.localScale = new Vector2(shardSize, shardSize);
    }
}

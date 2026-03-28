using UnityEngine;

public class Bullet4Enemy : MonoBehaviour
{
    private GameObject enemy;
    private Rigidbody2D rb;
    public float force;

    public float timer;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemy = GameObject.FindGameObjectWithTag("Enemy");

        if (enemy == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = enemy.transform.position - transform.position;
        rb.linearVelocity = new Vector2(direction.x, direction.y).normalized * force;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 5)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(25);
            Destroy(gameObject);
        }
    }
}

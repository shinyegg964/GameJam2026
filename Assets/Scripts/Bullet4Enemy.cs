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
        enemy = FindClosestEnemy();

        if (enemy == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = enemy.transform.position - transform.position;
        rb.linearVelocity = new Vector2(direction.x, direction.y).normalized * force;
    }

    GameObject FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        GameObject closest = null;
        float minDistance = Mathf.Infinity;

        Vector3 currentPosition = transform.position;

        foreach (GameObject e in enemies)
        {
            float distance = Vector3.Distance(currentPosition, e.transform.position);

            if (distance < minDistance)
            {
                minDistance = distance;
                closest = e;
            }
        }

        return closest;
    }
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

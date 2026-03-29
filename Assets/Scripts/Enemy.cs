using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject brko;
    Rigidbody2D rb;
    Transform target;
    Vector2 moveDirection;
    public float health = 50f;
    public XP_Manager xpManager;

    public GameObject bullet;
    public Transform bulletPos;
    private float timer;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        xpManager = FindObjectOfType<XP_Manager>();
    }

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (target)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            moveDirection = direction;

        }

        if(health <= 0)
        {
            Destroy(gameObject);
            xpManager.AddExperience(10);
            if (Random.Range(0,5) == 0)
            {
                Instantiate(brko, transform.position, Quaternion.identity);
            }
           
        }

        timer += Time.deltaTime;

        if(timer > 2)
        {
            timer = 0;
            Shoot();
        }
    }

    private void FixedUpdate()
    {
        if (target)
        {
            rb.linearVelocity = new Vector2(moveDirection.x, moveDirection.y) * moveSpeed;
        }
    }

    private void Shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }


}

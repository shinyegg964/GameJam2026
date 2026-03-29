using UnityEngine;
using System;
public class EnemyDash : MonoBehaviour, IDamegable
{
    public GameObject brko;
    public float moveSpeed = 2f;
    public float health = 50f;
    private bool isDead = false;
    public float dashSpeed = 10f;
    public float dashDuration = 0.3f;
    public float dashCooldown = 2f;
    public XP_Manager xpManager;
    private float dashTimer;
    private float cooldownTimer;
    public Action OnDeath;
    private bool isDashing = false;

    private Rigidbody2D rb;
    private Transform target;
    private Vector2 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        cooldownTimer = dashCooldown;
    }

    void Update()
    {
        if (!target) return;

        
        Vector2 direction = (target.position - transform.position).normalized;
        moveDirection = direction;

        if (isDashing)
        {
            dashTimer -= Time.deltaTime;

            if (dashTimer <= 0f)
            {
                isDashing = false;
                cooldownTimer = dashCooldown;
            }
        }
        else
        {
            cooldownTimer -= Time.deltaTime;

            if (cooldownTimer <= 0f)
            {
                StartDash(direction);
            }
        }
    }

    void FixedUpdate()
    {
        if (!target) return;

        if (isDashing)
        {
            rb.linearVelocity = moveDirection * dashSpeed;
        }
        else
        {
            rb.linearVelocity = moveDirection * moveSpeed;
        }
    }

    void StartDash(Vector2 direction)
    {
        isDashing = true;
        dashTimer = dashDuration;
        moveDirection = direction; // zamkne směr dasha
    }
    public void TakeDamage(float damage)
    {
        

        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        if (isDead) return; // 🔥 zabrání double death

        isDead = true;

        // 🔔 notify spawner
        OnDeath?.Invoke();

        // 🎯 XP
        if (xpManager != null)
            xpManager.AddExperience(10);

        // 🎁 drop
        if (UnityEngine.Random.Range(0, 5) == 0)
        {
            Instantiate(brko, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(50);
            
        }
    }
}
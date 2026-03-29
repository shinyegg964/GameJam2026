using UnityEngine;
using Unity.VisualScripting;
using System;

public class Enemy : MonoBehaviour, IDamegable
{
    public float moveSpeed = 5f;
    public GameObject brko;
    public Action OnDeath;

    Rigidbody2D rb;
    Transform target;
    Vector2 moveDirection;

    public float health = 50f;
    public XP_Manager xpManager;

    public GameObject bullet;
    public Transform bulletPos;

    private float timer;
    private bool isDead = false; // důležité!

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
        if (isDead) return; // 💀 stop všechno po smrti

        if (target)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            moveDirection = direction;
        }

        if (health <= 0)
        {
            Die();
        }

        timer += Time.deltaTime;

        if (timer > 2f)
        {
            timer = 0;
            Shoot();
        }
    }

    private void FixedUpdate()
    {
        if (target && !isDead)
        {
            rb.linearVelocity = moveDirection * moveSpeed;
        }
    }

    private void Shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        health -= damage;
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
}
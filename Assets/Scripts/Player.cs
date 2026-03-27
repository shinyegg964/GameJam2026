using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    private Rigidbody2D rb;
    Transform target;
    public GameObject bullet;
    public Transform bulletPos;
    private float timer;
    Vector2 moveDirection;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Enemy").transform;
    }

    void Update()
    {
        Vector2 direction1 = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        direction1.Normalize();

        rb.linearVelocity = direction1 * moveSpeed;

        if (target)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            moveDirection = direction;

        }

        timer += Time.deltaTime;

        if (timer > 2)
        {
            timer = 0;
            Shoot();
        }
    }

    private void Shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }
}

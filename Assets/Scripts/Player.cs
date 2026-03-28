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
    public float timer;
    public Animator anim;
    Vector2 moveDirection;
    private bool isFacingRight = false;
    public float input;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Enemy").transform;
    }

    void Update()
    {
        input = Input.GetAxisRaw("Horizontal");
        Vector2 direction1 = new Vector2(input, Input.GetAxisRaw("Vertical"));
        direction1.Normalize();

        rb.linearVelocity = direction1 * moveSpeed;

        if(input != 0)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }

        if (target)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            moveDirection = direction;

        }

        timer += Time.deltaTime;

        if (timer > 1)
        {
            timer = 0;
            Shoot();
        }

        Flip();
    }

    private void Shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }

    private void Flip()
    {
        if (isFacingRight && input < 0f || !isFacingRight && input > 0f)
        {
            Vector3 localScale = transform.localScale;
            isFacingRight = !isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}

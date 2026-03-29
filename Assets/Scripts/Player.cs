using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    private Rigidbody2D rb;
    private AudioSource AS;
    public AudioClip smoke;
    public float damage;
    Transform target;
    public TextMeshProUGUI cigarety;
    public GameObject bullet;
    public Transform bulletPos;
    public float timer;
    public Animator anim;
    Vector2 moveDirection;
    private bool isFacingRight = true;
    public float input;
    public int cig = 5;

    
    
    void Start()
    {
        cigarety.text = "Cigarety: " + cig;
        rb = GetComponent<Rigidbody2D>();
        AS = GetComponent<AudioSource>();
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

        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(Smoke());
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

    private void Upgrade()
    {

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

    IEnumerator Smoke()
    {
        if(cig >= 1 && gameObject.GetComponent<PlayerHealth>().isDead == false)
        {
            cig -= 1;
            cigarety.text = "Cigarety: " + cig;
            AS.PlayOneShot(smoke);
            anim.SetTrigger("isSmoking");
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
            rb.constraints = RigidbodyConstraints2D.None;
            gameObject.GetComponent<PlayerHealth>().Heal(25);
        }
        

    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f;

    private Rigidbody2D rb;
    public Sprite healthy;
    public Sprite mid;
    public Sprite dead;

    public Image healthBar;
    public Image Status;

    public Animator anim;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(Addiction());
    }

    void Update()
    {
        if(health <= 0f)
        {
            Status.sprite = dead;
            anim.SetTrigger("isDead");
            rb.constraints = RigidbodyConstraints2D.FreezeAll;

        }
        if(health > 50f)
        {
            Status.sprite = healthy;
        }
        else if (health <= 50f && health != 0f)
        {
            Status.sprite = mid;
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.fillAmount = health / 100f;
    }

    public void Heal(float heal)
    {
        if(health <= 100)
        {
            health += heal;
            if(health > 100)
            {
                health = 100;
            }
        }
        
        healthBar.fillAmount = health / 100f;
    }

    IEnumerator Addiction()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            health -= 1;
            healthBar.fillAmount = health / 100f;
        }
    }
}

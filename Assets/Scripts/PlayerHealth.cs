using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f;

    private AudioSource AS;
    public AudioClip hit;
    private Rigidbody2D rb;

    public Sprite healthy;
    public Sprite mid;
    public Sprite dead;

    public bool isDead = false;

    public Image healthBar;   // cigareta (Filled Image)
    public Image Status;

    public RectTransform burnPoint; // žhavý konec

    public Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        AS = GetComponent<AudioSource>();
        StartCoroutine(Addiction());
    }

    void Update()
    {
        // death fix (nespouští se pořád dokola)
        if (health <= 0f && !isDead)
        {
            StartCoroutine(Dead());
        }

        // status sprite
        if (health > 50f)
        {
            Status.sprite = healthy;
        }
        else if (health <= 50f && health > 0f)
        {
            Status.sprite = mid;
        }

        UpdateCigaretteUI();
    }

    void UpdateCigaretteUI()
    {
        float normalizedHealth = health / 100f;

        // fill cigarety
        healthBar.fillAmount = normalizedHealth;

        // posun žhavého konce
        float width = healthBar.rectTransform.rect.width;

        burnPoint.anchoredPosition = new Vector2(
            width * normalizedHealth,
            burnPoint.anchoredPosition.y
        );
    }

    public void TakeDamage(float damage)
    {
        if (!isDead)
        {
            AS.PlayOneShot(hit);
            health -= damage;

            if (health < 0f)
                health = 0f;
        }
    }

    public void Heal(float heal)
    {
        if (!isDead)
        {
            health += heal;

            if (health > 100f)
                health = 100f;
        }
    }

    IEnumerator Addiction()
    {
        while (!isDead)
        {
            yield return new WaitForSeconds(1f);
            health -= 1f;

            if (health < 0f)
                health = 0f;
        }
    }

    IEnumerator Dead()
    {
        isDead = true;

        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        anim.SetTrigger("isDead");

        yield return new WaitForSeconds(1f);

        Time.timeScale = 0f;

        Status.sprite = dead;
    }
}
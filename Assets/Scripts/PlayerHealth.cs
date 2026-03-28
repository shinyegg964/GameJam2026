using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f;
    public Image healthBar;
    void Start()
    {
        StartCoroutine(Addiction());
    }

    void Update()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
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

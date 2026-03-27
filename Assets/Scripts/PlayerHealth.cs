using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f;
    public Image healthBar;
    void Start()
    {
        
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
        healthBar.fillAmount = health / 100;
    }
}

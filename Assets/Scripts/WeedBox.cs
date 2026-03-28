using UnityEngine;

public class WeedBox : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();

            if (player != null)
            {
                player.cig += 5;
                player.cigarety.text = "Cigarety: " + player.cig;
            }

            Destroy(gameObject); // zmizí po sebrání
        }
    }
}

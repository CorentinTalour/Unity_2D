using UnityEngine;

public class HealPowerUp : MonoBehaviour
{
    public int HealtPoints;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if (PlayerHealth.instence.currentHealth < PlayerHealth.instence.maxHealth)
            {
                // rendre de la vie au joueur
                PlayerHealth.instence.HealPlayer(HealtPoints);
                Destroy(gameObject);
            }
        }
    }
}

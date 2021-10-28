using UnityEngine;

public class HealPowerUp : MonoBehaviour
{
    public int HealtPoints;
    public AudioClip pickUpSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if (PlayerHealth.instence.currentHealth < PlayerHealth.instence.maxHealth)
            {
                AudioManager.instence.PlayClipAt(pickUpSound, transform.position);
                // rendre de la vie au joueur
                PlayerHealth.instence.HealPlayer(HealtPoints);
                Destroy(gameObject);
            }
        }
    }
}

using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public float invincibilityTimeAfterHit = 3f;
    public float invincibilityFlashDelay = 0.2f; // 0,2 secondes
    public bool isInvincible = false;

    public SpriteRenderer graphics;
    public HealthBar healthBar;

    public AudioClip hitSound;

    public static PlayerHealth instence;

    // permet d'acceder a Incentory depuis n'importe qu'elle classe
    private void Awake()
    {
        if (instence != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de PlayerHealth dans la scène");
            return;
        }

        instence = this;
    }

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDommage(60);
        }
    }

    public void HealPlayer (int amout)
    {
        if((currentHealth + amout) > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += amout;
        }

        healthBar.SetHealth(currentHealth);

    }

     public void TakeDommage(int damage)
    {
        if (!isInvincible)
        {
            AudioManager.instence.PlayClipAt(hitSound, transform.position);
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);

            // vérifier si le joueur est toujours vivant
            if(currentHealth <= 0)
            {
                Die();
                return;
            }

            isInvincible = true;
            StartCoroutine(InvincibilityFlash());
            StartCoroutine(HandleInvincibilityDelay());
        }
    }

    public void Die()
    {
        // bloquer les mouvement du personage
        PlayerMovement.instence.enabled = false;
        // jouer l'animation d'élimination
        PlayerMovement.instence.animator.SetTrigger("Die");
        // empêcher les interactions physique avec les autre éléments de la scène
        PlayerMovement.instence.rb.bodyType = RigidbodyType2D.Kinematic;
        // met la vitesse a 0
        PlayerMovement.instence.rb.velocity = Vector3.zero;
        PlayerMovement.instence.playerCollider.enabled = false;
        GameOverManager.instence.OnPlayerDeath();
    }

    public void Respawn()
    {
        PlayerMovement.instence.enabled = true;
        PlayerMovement.instence.animator.SetTrigger("Respawn");
        PlayerMovement.instence.rb.bodyType = RigidbodyType2D.Dynamic;
        PlayerMovement.instence.playerCollider.enabled = true;
        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth);
    }

    public IEnumerator InvincibilityFlash()
    {
        while (isInvincible)
        {
            // 1f correspond a 255 dans color
            graphics.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(invincibilityFlashDelay);
            graphics.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(invincibilityFlashDelay);
        }
    }

    public IEnumerator HandleInvincibilityDelay()
    {
        yield return new WaitForSeconds(invincibilityTimeAfterHit);
        isInvincible = false;
    }
}

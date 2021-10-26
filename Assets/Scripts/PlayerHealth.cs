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
            TakeDommage(20);
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
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);
            isInvincible = true;
            StartCoroutine(InvincibilityFlash());
            StartCoroutine(HandleInvincibilityDelay());
        }
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

using UnityEngine;


public class PickUpObject : MonoBehaviour
{
    public AudioClip sound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            AudioManager.instence.PlayClipAt(sound, transform.position);
            Inventory.instence.AddCoins(1);
            CurrentSceneManager.instence.coinsPickdUpInThisSceneCount++;
            Destroy(gameObject);
        }
    }
}

using UnityEngine;

public class LoadAndSaveData : MonoBehaviour
{


    public static LoadAndSaveData instence;

    // permet d'acceder a Incentory depuis n'importe qu'elle classe
    private void Awake()
    {
        if (instence != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de LoadAndSaveData dans la scène");
            return;
        }

        instence = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Inventory.instence.coinCount = PlayerPrefs.GetInt("coinsSave", 0);
        Inventory.instence.UpdateTextUI();

        int currentHealth = PlayerPrefs.GetInt("playerHealthSave", PlayerHealth.instence.maxHealth);
        PlayerHealth.instence.currentHealth = currentHealth;
        PlayerHealth.instence.healthBar.SetHealth(currentHealth);
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("coinsSave", Inventory.instence.coinCount);
        

        if(CurrentSceneManager.instence.levelToUnlock > PlayerPrefs.GetInt("LevelReachedSucceeded", 1))
        {
            PlayerPrefs.SetInt("LevelReachedSucceeded", CurrentSceneManager.instence.levelToUnlock);
        }

        PlayerPrefs.SetInt("playerHealthSave", PlayerHealth.instence.currentHealth);
    }

}

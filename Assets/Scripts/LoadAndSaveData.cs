using UnityEngine;
using System.Linq;

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

        // chargement des items
        string[] itemsSaved = PlayerPrefs.GetString("inventoryItems", "").Split(',');
        for (int i = 0; i < itemsSaved.Length; i++)
        {
            if(itemsSaved[i] != "")
            {
                int id = int.Parse(itemsSaved[i]);
                Item currentItem = ItemsDataBase.instence.allItems.Single(x => x.id == id);
                Inventory.instence.content.Add(currentItem);
            }

        }

        Inventory.instence.UpdateInventoryUI();
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("coinsSave", Inventory.instence.coinCount);
        

        if(CurrentSceneManager.instence.levelToUnlock > PlayerPrefs.GetInt("LevelReachedSucceeded", 1))
        {
            PlayerPrefs.SetInt("LevelReachedSucceeded", CurrentSceneManager.instence.levelToUnlock);
        }

        PlayerPrefs.SetInt("playerHealthSave", PlayerHealth.instence.currentHealth);

        // sauvegarde
        string itemsInInventory = string.Join(",", Inventory.instence.content.Select(x => x.id));
        PlayerPrefs.SetString("inventoryItems", itemsInInventory);


    }
}

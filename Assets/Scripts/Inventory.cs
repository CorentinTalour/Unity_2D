using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int coinCount;
    public Text coinsCountText;

    public List<Item> content = new List<Item>();
    public int contentCurrentIndex = 0;
    public Image ItemImageUI;
    public Text itemNameUI;
    public Sprite emptyItemImage;

    public static Inventory instence;

    // permet d'acceder a Incentory depuis n'importe qu'elle classe
    private void Awake()
    {
        if(instence != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de Inventory dans la scène");
            return;
        }

        instence = this;
    }

    private void Start()
    {
        UpdateInventoryUI();
    }

    public void ConsumeItem()
    {
        if(content.Count == 0)
        {
            return;
        }

        Item currentItem = content[contentCurrentIndex];
        PlayerHealth.instence.HealPlayer(currentItem.hpGiven);
        PlayerMovement.instence.moveSpeed += currentItem.speedGiven;
        content.Remove(currentItem);
        GetNextItem();
        UpdateInventoryUI();
    }

    public void GetNextItem()
    {
        if (content.Count == 0)
        {
            return;
        }

        contentCurrentIndex++;
        if (contentCurrentIndex > content.Count - 1)
        {
            contentCurrentIndex = 0;
        }
        UpdateInventoryUI();
    }

    public void GetPreviousItem()
    {
        if (content.Count == 0)
        {
            return;
        }

        contentCurrentIndex--;
        if (contentCurrentIndex < 0)
        {
            contentCurrentIndex = content.Count - 1;
        }
        UpdateInventoryUI();
    }

    public void UpdateInventoryUI()
    {
        if(content.Count > 0)
        {
            ItemImageUI.sprite = content[contentCurrentIndex].image;
            itemNameUI.text = content[contentCurrentIndex].name;
        }
        else
        {
            ItemImageUI.sprite = emptyItemImage;
            itemNameUI.text = "";
        }
        
    }

    public void AddCoins(int count)
    {
        coinCount += count;
        UpdateTextUI();
    }

    public void RemoveCoins(int count)
    {
        coinCount -= count;
        UpdateTextUI();
    }

    public void UpdateTextUI()
    {
        coinsCountText.text = coinCount.ToString();
    }
}

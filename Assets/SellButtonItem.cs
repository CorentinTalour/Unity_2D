using UnityEngine;
using UnityEngine.UI;

public class SellButtonItem : MonoBehaviour
{
    public Text itemName;
    public Image itemImage;
    public Text itemPrice;

    public Item item;

    public void BuyItem()
    {
        Inventory inventory = Inventory.instence;

        if(Inventory.instence.coinCount >= item.price)
        {
            inventory.content.Add(item);
            inventory.UpdateInventoryUI();
            inventory.coinCount -= item.price;
            inventory.UpdateTextUI();
        }
    }
}

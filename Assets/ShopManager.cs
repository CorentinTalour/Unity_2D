using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public Text pnjNameText;

    public Animator animator;

    public GameObject sellButtonPrefab;
    public Transform sellButtonsParent;

    public static ShopManager instence;

    // permet d'acceder a ShopManager depuis n'importe qu'elle classe
    private void Awake()
    {
        if (instence != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de ShopManager dans la scène");
            return;
        }

        instence = this;

    }

    public void OpenShop(Item[] items, string pnjName)
    {
        pnjNameText.text = pnjName;
        UpdateItemsToSell(items);
        animator.SetBool("isOpen", true);
    }

    void UpdateItemsToSell(Item[] items)
    {
        //childCount renvoie le nombre d'enfant qu'a sellButtonsParent
        /*for (int i = 0; i < sellButtonsParent.childCount; i++)
        {
            Destroy(sellButtonsParent.GetChild(i).gameObject);
        }*/

        //Instancie un bouton pour chaque item à vendre et le configure 
        for (int i = 0; i < items.Length; i++)
        {
            GameObject button = Instantiate(sellButtonPrefab, sellButtonsParent);
            SellButtonItem buttonScript = button.GetComponent<SellButtonItem>();
            buttonScript.itemName.text = items[i].name;
            buttonScript.itemImage.sprite = items[i].image;
            buttonScript.itemPrice.text = items[i].price.ToString();

            buttonScript.item = items[i];

            //delegate permet de faire un appel un a event a unity // la ligne correspond a un onClick()
            button.GetComponent<Button>().onClick.AddListener(delegate { buttonScript.BuyItem(); });
        }
    }

    public void CloseShop()
    {
        animator.SetBool("isOpen", false);
    }
}

using UnityEngine;
using UnityEngine.UI;

public class ShopTrigger : MonoBehaviour
{
    private bool isInRange;

    private Text interactUI;

    public string pnjName;
    public Item[] itemToSell;


    private void Awake()
    {
        interactUI = GameObject.FindGameObjectWithTag("InteractUI").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            ShopManager.instence.OpenShop(itemToSell, pnjName);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = true;
            interactUI.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = false;
            interactUI.enabled = false;
            ShopManager.instence.CloseShop();
        }
    }
}

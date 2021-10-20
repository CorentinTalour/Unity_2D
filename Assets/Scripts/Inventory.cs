using UnityEngine.UI;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int coinCount;
    public Text coinsCountText;

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

    public void AddCoins(int count)
    {
        coinCount += count;
        coinsCountText.text = coinCount.ToString();
    }
}

using UnityEngine;

public class ItemsDataBase : MonoBehaviour
{
    public Item[] allItems;

    public static ItemsDataBase instence;

    // permet d'acceder a ItemsDataBase depuis n'importe qu'elle classe
    private void Awake()
    {
        if (instence != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de ItemsDataBase dans la scène");
            return;
        }

        instence = this;
    }
}

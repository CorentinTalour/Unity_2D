using UnityEngine;

public class CurrentSceneManager : MonoBehaviour
{
    public bool isPlayerPresentByDefault = false;
    public int coinsPickdUpInThisSceneCount;

    public static CurrentSceneManager instence;

    // permet d'acceder a Incentory depuis n'importe qu'elle classe
    private void Awake()
    {
        if (instence != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de CurrentSceneManager dans la scène");
            return;
        }

        instence = this;
    }
}

using UnityEngine;

public class CurrentSceneManager : MonoBehaviour
{
    public int coinsPickdUpInThisSceneCount;
    public Vector3 respwnPoint;

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

        respwnPoint = GameObject.FindGameObjectWithTag("Player").transform.position;
    }
}

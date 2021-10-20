using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    public Gradient gradient;
    public Image fill;

    // initialise la vie du joueur
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

        // initialise le gradient au max donc (1f)
        fill.color = gradient.Evaluate(1f);
    }

    // indique dans la barre de vie qu'elle le nombre de points de vie a afficher quand le joueur prend dégat ou soigné
    public void SetHealth(int health)
    {
        slider.value = health;
        /*
         * change la valeur du gradiant mais en récupérent la vie actuelle du joueur
         * slider.normalizedValue permet de définir la vie du joueur qui est de 0 à 100 le transforme de 0 à 1
         * se que le gradient peut comprendre
        */
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}

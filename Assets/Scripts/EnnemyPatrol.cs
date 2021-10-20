using UnityEngine;

public class EnnemyPatrol : MonoBehaviour
{
    public float speed;
    public Transform[] waypoints;

    public SpriteRenderer graphics;
    private Transform target;
    private int destPoint = 0;

    public int damageOnCollision = 20;

    void Start()
    {
        // par d�fault la cible se d�place vers le 1er waypoints de la liste.
        target = waypoints[0];
    }

    void Update()
    {
        // Permet de savoir vers quelle direction se d�placer pour se rendre vers le prochain waypoints
        Vector3 dir = target.position - transform.position;
        /*  
         *  Translate : m�thode de d�placement de Unity
         *  normalized : permet de normaliser vecteur c'est de mettre la magnitude de ce vecteur � 1
         *  donc on va faire en sorte que le vecteur est toujours la m�me taille.
         *  Speed : Permet de g�rer la vitesse du joueur
         *  Time.deltaTime : permet de faire un d�placement au fil du temp
         *  Space.World : permet de d�placer le personnage par rapport au monde 
        */
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        /* Si l'ennemi est quasiment arriv� a destination
         * transform.position : r�cupere la position du joueur
         * target.position : r�cupere la position du waypoint
         * 0,3f : valeur de s�curit� pour �tre sur que les animaion se joue bien.
        */
        if (Vector3.Distance(transform.position, target.position) < 0.3f)
        {
            //Permet de d�placer le point de la liste (A vers B) et si on vas vers le dernier point alors on 
            //retourne au point 0 grace a la division
            //Permet de bien se d�placer dans la liste waypoint
            destPoint = (destPoint + 1) % waypoints.Length;
            /*
             * target = objet destPoint
             * destPoint = int qui represente l'index de la target
             * Target r�cupere le point ou il doit aller.
            */
            target = waypoints[destPoint];
            /*
             * Si les graphisme sont flipp� alors on les d�flipp et inversement 
            */
            graphics.flipX = !graphics.flipX;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.transform.GetComponent<PlayerHealth>();
            playerHealth.TakeDommage(damageOnCollision);
        }
    }
}

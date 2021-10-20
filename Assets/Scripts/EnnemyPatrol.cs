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
        // par défault la cible se déplace vers le 1er waypoints de la liste.
        target = waypoints[0];
    }

    void Update()
    {
        // Permet de savoir vers quelle direction se déplacer pour se rendre vers le prochain waypoints
        Vector3 dir = target.position - transform.position;
        /*  
         *  Translate : méthode de déplacement de Unity
         *  normalized : permet de normaliser vecteur c'est de mettre la magnitude de ce vecteur à 1
         *  donc on va faire en sorte que le vecteur est toujours la même taille.
         *  Speed : Permet de gérer la vitesse du joueur
         *  Time.deltaTime : permet de faire un déplacement au fil du temp
         *  Space.World : permet de déplacer le personnage par rapport au monde 
        */
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        /* Si l'ennemi est quasiment arrivé a destination
         * transform.position : récupere la position du joueur
         * target.position : récupere la position du waypoint
         * 0,3f : valeur de sécurité pour être sur que les animaion se joue bien.
        */
        if (Vector3.Distance(transform.position, target.position) < 0.3f)
        {
            //Permet de déplacer le point de la liste (A vers B) et si on vas vers le dernier point alors on 
            //retourne au point 0 grace a la division
            //Permet de bien se déplacer dans la liste waypoint
            destPoint = (destPoint + 1) % waypoints.Length;
            /*
             * target = objet destPoint
             * destPoint = int qui represente l'index de la target
             * Target récupere le point ou il doit aller.
            */
            target = waypoints[destPoint];
            /*
             * Si les graphisme sont flippé alors on les déflipp et inversement 
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

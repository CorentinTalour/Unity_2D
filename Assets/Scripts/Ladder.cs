using UnityEngine;
using UnityEngine.UI;

public class Ladder : MonoBehaviour
{
    private bool isInRange;
    private PlayerMovement playerMovement;
    public BoxCollider2D topCollider;
    private Text interactUI;

    // Start is called before the first frame update
    void Awake()
    {
        // récuper le scrpit PlayerMovement
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        interactUI = GameObject.FindGameObjectWithTag("InteractUI").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update() 
    {
        if(isInRange && topCollider.isTrigger && Input.GetKeyDown(KeyCode.E))
        {
            // descendre de l'echelle
            playerMovement.isClimbing = false;
            topCollider.isTrigger = false;
            Debug.Log("Descente de l'echelle");
            return;
        }

        if(isInRange && Input.GetKeyDown(KeyCode.E))
        {
            playerMovement.isClimbing = true;
            topCollider.isTrigger = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactUI.enabled = true;
            isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isInRange = false;
        playerMovement.isClimbing = false;
        topCollider.isTrigger = false;
        interactUI.enabled = false;
    }
}



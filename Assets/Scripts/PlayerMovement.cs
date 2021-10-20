using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;

    public Transform groundChek;
    public float groundCheckRaduis;
    public LayerMask collisionLayers;

    private bool isJumping;
    private bool isGrounded;

    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    private Vector3 velocity = Vector3.zero;

    private float horizontalMovement;

    void Update()
    {
        //Vitesse du personnage 
        horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isJumping = true;
        }

        flip(rb.velocity.x);

        // math renvoie toujours une valeur positif ex -1 renvoie 1
        float characterVelocity = Mathf.Abs(rb.velocity.x);
        animator.SetFloat("Speed", characterVelocity);
    }

    void FixedUpdate()
    {
        //cree un ligne sous le personnage
        isGrounded = Physics2D.OverlapCircle(groundChek.position, groundCheckRaduis, collisionLayers);
        MovePlayer(horizontalMovement);
    }

    // _ = convention de nommage étant donné que cette variable est un paramettre elle est passer en paramettre a la fonction donc ajout _ a l'avant de
    // horizontaleMovement qui est au dessus
    void MovePlayer(float _horizontalMovement)
    {
        Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);

        if(isJumping == true)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
            isJumping = false;
        }
    }

    void flip(float _velocity)
    {
        if(_velocity > 0.1f)
        {
            spriteRenderer.flipX = false;
        } else if(_velocity < -0.1f)
        {
            spriteRenderer.flipX = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundChek.position, groundCheckRaduis);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D Rb;

    private Vector2 moveDirection;

    // Quand le joueur va prendre la pillule rouge, sa vitesse a augmenter de 40%
    private static bool peutPrendreDoubleDommage = false;
    private float vitesseMovementOriginal;

    void Start()
    {
        vitesseMovementOriginal = moveSpeed;
    }

    void Update()
    {
        ProcessInputs();
    }

    void FixedUpdate()
    {
        Move();
    }

    void ProcessInputs()
    {

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized; 

    }

    void Move() 
    {
        if (peutPrendreDoubleDommage)
        {
            moveSpeed = vitesseMovementOriginal * 1.4f;
        }
        else
        {
            moveSpeed = vitesseMovementOriginal;
        }
        Rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
        
    }

    public static void SetDoubleVitesseJoueur(bool value)
    {
        peutPrendreDoubleDommage = value;
    }
}

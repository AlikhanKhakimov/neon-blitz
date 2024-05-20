using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalleScript : MonoBehaviour
{
    private Vector3 positionSouris;
    private Camera cam;
    private Rigidbody2D rb;
    public float force;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        positionSouris = cam.ScreenToWorldPoint(Input.mousePosition);

        // La direction oû la balle va aller. Ici c'est la souris
        Vector3 direction = positionSouris - transform.position;
        // Ne pas permettre la balle de tourner vers le curseur
        Vector3 rotation = transform.position - positionSouris;
        //Peut importe la postion de la souris entre le joueur, loin ou proche, la vitesse va pas changer
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force; // force nous permet d'ajuster la vitesse dans l'inspecteur 

        float rot = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // Permet de changer la rotation de notre balle 
        transform.rotation = Quaternion.Euler(0, 0, rot + 90); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Ennemi"))
        {
            EnemiDestroy enemy = collision.GetComponent<EnemiDestroy>();
            if (enemy != null)
            {
                enemy.PrendreDommage(50);
            }
            Destroy(gameObject);
        }
        
        

    }


   private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ennemi")
        {
            Destroy(gameObject);
        }
    }
}

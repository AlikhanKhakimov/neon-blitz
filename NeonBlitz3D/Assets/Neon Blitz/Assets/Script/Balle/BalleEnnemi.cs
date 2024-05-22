using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalleEnnemi : MonoBehaviour
{
    public float vitesseBalle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * vitesseBalle * Time.deltaTime);
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Mur"))
        {

            print("test collision enemy mur");
            Destroy(gameObject);

        }

        else if (collision.CompareTag("Player"))
        {
            JoueurDestroy joueur = collision.GetComponent<JoueurDestroy>();
            if (joueur != null)
            {
                joueur.PrendreDommage(50);
                print("test collision enemy player");
            }
            Destroy(gameObject);
        }
        
    }

    public void Impact()
    {

    }

    
}

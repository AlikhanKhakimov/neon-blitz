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
        switch (collision.gameObject.tag)
        {
            case "Player":
                collision.gameObject.GetComponent<JoueurDestroy>().PrendreDommage(100);
                Destroy(gameObject);
                break;
        }
    }

    public void Impact()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            Destroy(gameObject);
        }

        
    }
    
}

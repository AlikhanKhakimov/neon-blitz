using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PilluleVerteController2D : MonoBehaviour
{
    private bool isTriggered = false;

    // Duration of the effect in seconds
    public float dur�ePouvoirPilluleVerte = 5f;

    TextSecVerte textSecVerte;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator AppliquerBuffForDur�e()
    {
        // Les pouvoirs commencent
        JoueurDestroy.SetMoiti�Dommage(true);
        //Faire disparaitre le gameObject pillule et non d�truire. Si on d�truit, les pouvoirs ne pourons pas disparr�tre
        gameObject.GetComponent<Renderer>().enabled = false;

        // Le UI pour la pillule va �tre afficher
        TextSecVerte.textSecVerte.MontrerUISecVerte(true);

        // Donn� une dur�er � ce pouvoir (Ici le joueur a 5 secondes)
        yield return new WaitForSeconds(dur�ePouvoirPilluleVerte);

        // Retirer les pouvoirs du joueur et remettre le dommage originale
        EnemiDestroy.SetDoubleDommage(false);

        // Le UI pour la pillule va �tre cacher
        TextSecVerte.textSecVerte.MontrerUISecVerte(false);
        // Detruire l'objet pillule rouge
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isTriggered && collision.gameObject.CompareTag("Player"))
        {
            isTriggered = true;
            StartCoroutine(AppliquerBuffForDur�e());
        }
    }
}
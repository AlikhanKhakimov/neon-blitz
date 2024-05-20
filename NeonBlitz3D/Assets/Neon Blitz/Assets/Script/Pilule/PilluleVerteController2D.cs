using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PilluleVerteController2D : MonoBehaviour
{
    private bool isTriggered = false;

    // Duration of the effect in seconds
    public float duréePouvoirPilluleVerte = 5f;

    TextSecVerte textSecVerte;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator AppliquerBuffForDurée()
    {
        // Les pouvoirs commencent
        JoueurDestroy.SetMoitiéDommage(true);
        //Faire disparaitre le gameObject pillule et non détruire. Si on détruit, les pouvoirs ne pourons pas disparrître
        gameObject.GetComponent<Renderer>().enabled = false;

        // Le UI pour la pillule va être afficher
        TextSecVerte.textSecVerte.MontrerUISecVerte(true);

        // Donné une duréer à ce pouvoir (Ici le joueur a 5 secondes)
        yield return new WaitForSeconds(duréePouvoirPilluleVerte);

        // Retirer les pouvoirs du joueur et remettre le dommage originale
        EnemiDestroy.SetDoubleDommage(false);

        // Le UI pour la pillule va être cacher
        TextSecVerte.textSecVerte.MontrerUISecVerte(false);
        // Detruire l'objet pillule rouge
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isTriggered && collision.gameObject.CompareTag("Player"))
        {
            isTriggered = true;
            StartCoroutine(AppliquerBuffForDurée());
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PilluleRougeController : MonoBehaviour
{
    private bool isTriggered = false;


    // Duration of the effect in seconds
    public float duréePouvoirPilluleRouge = 5f;

    TextSecRouge textSecRouge;

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
        EnemiDestroy.SetDoubleDommage(true);
        PlayerMovement.SetDoubleVitesseJoueur(true);
        //Faire disparaitre le gameObject pillule et non détruire. Si on détruit, les pouvoirs ne pourons pas disparrître
        gameObject.GetComponent<Renderer>().enabled = false;

        // Le UI pour la pillule va être afficher
        TextSecRouge.textSecRouge.MontrerUISecRouge(true);

        // Donné une duréer à ce pouvoir (Ici le joueur a 5 secondes)
        yield return new WaitForSeconds(duréePouvoirPilluleRouge);

        // Retirer les pouvoirs du joueur et remettre la vitesse et le dommage originales
        EnemiDestroy.SetDoubleDommage(false);
        PlayerMovement.SetDoubleVitesseJoueur(false);

        // Le UI pour la pillule va être cacher
        TextSecRouge.textSecRouge.MontrerUISecRouge(false);

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
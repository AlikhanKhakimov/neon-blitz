using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PilluleRougeController : MonoBehaviour
{
    // Durée du buff en secondes
    public float durée = 5f;

    // Indicateur si le buff a déjà été déclenché
    private bool isTriggered = false;

    // Déclenché lorsque le joueur entre en collision avec la pilule rouge
    void OnTriggerEnter(Collider other)
    {
        // Vérifie si le buff n'a pas déjà été déclenché et que le joueur a collisionné avec la pilule
        if (!isTriggered && other.gameObject.CompareTag("Player"))
        {
            isTriggered = true;
            // Démarre la coroutine pour appliquer le buff pendant la durée spécifiée
            StartCoroutine(AppliquerBuffPourDurée());
        }
    }

    // Coroutine pour appliquer le buff pendant une durée spécifiée
    private IEnumerator AppliquerBuffPourDurée()
    {
        // Active le mode double vitesse du joueur
        JoueurrMouvement.SetDoubleVitesse(true);
        // Désactive le rendu de la pilule rouge
        gameObject.GetComponent<Renderer>().enabled = false;

        // Affiche l'UI de la pilule rouge
        TextSecRouge.textSecRouge.MontrerUISecRouge(true);

        // Attend la durée spécifiée avant de désactiver le buff
        yield return new WaitForSeconds(durée);

        // Désactive le mode double vitesse du joueur
        JoueurrMouvement.SetDoubleVitesse(false);
        // Cache l'UI de la pilule rouge
        TextSecRouge.textSecRouge.MontrerUISecRouge(false);
        // Détruit la pilule rouge
        Destroy(gameObject);
    }
}
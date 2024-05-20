using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PilluleVerteController : MonoBehaviour
{
    // Références aux armes à modifier
    public GameObject glock;
    public GameObject ak;
    // Durée pour afficher l'UI
    public float durée = 10f;

    // Déclenché lorsque le joueur entre en collision avec la pilule verte
    void OnTriggerEnter(Collider other)
    {
        // Vérifie si le joueur a collisionné avec la pilule verte
        if (other.gameObject.CompareTag("Player"))
        {
            // Applique les modifications aux armes spécifiées
            if (glock != null)
            {
                Pistolet glockScript = glock.GetComponent<Pistolet>();
                if (glockScript != null)
                {
                    glockScript.AjouterDommage(5f);
                }
            }

            if (ak != null)
            {
                ArmeFusille akScript = ak.GetComponent<ArmeFusille>();
                if (akScript != null)
                {
                    akScript.AjouterDommage(2f);
                }
            }

            // Affiche l'UI pour la durée spécifiée
            StartCoroutine(AfficherUIPourDurée());

            // Désactive la pilule verte
            gameObject.SetActive(false);
        }
    }

    // Coroutine pour afficher l'UI pendant une durée spécifiée
    IEnumerator AfficherUIPourDurée()
    {
        // Affiche l'UI
        TextSecVerte.textSecVerte.MontrerUISecVerte(true);

        // Attend la durée spécifiée
        yield return new WaitForSeconds(durée);

        // Cache l'UI
        TextSecVerte.textSecVerte.MontrerUISecVerte(false);
    }
}
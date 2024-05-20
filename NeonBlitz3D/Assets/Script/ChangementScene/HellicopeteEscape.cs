using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HellicopeteEscape : MonoBehaviour
{
    // Référence au joueur
    public GameObject player;

    // Références à l'image et au texte à afficher lorsque le joueur gagne
    public Image imageGAGNE;
    public Text textGAGNE;

    // Indique si le joueur a gagné
    private bool hasWon = false;

    // Appelée lorsque quelque chose entre en collision avec le déclencheur attaché à cet objet
    private void OnTriggerEnter(Collider other)
    {
        // Vérifie si l'objet en collision est le joueur et si le joueur n'a pas déjà gagné
        if (other.gameObject == player && !hasWon)
        {
            // Indique que le joueur a gagné
            hasWon = true;
            // Affiche le texte de victoire
            ShowWinText();
        }
    }

    // Affiche le texte de victoire
    private void ShowWinText()
    {
        // Active l'image et le texte de victoire
        imageGAGNE.enabled = true;
        textGAGNE.enabled = true;
    }

    // Appelée au démarrage
    private void Start()
    {
        // Désactive l'image et le texte de victoire au démarrage
        imageGAGNE.enabled = false;
        textGAGNE.enabled = false;
    }
}
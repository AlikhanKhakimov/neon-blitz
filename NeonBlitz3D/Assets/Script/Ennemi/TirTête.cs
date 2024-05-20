using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TirTête : MonoBehaviour
{
    Poupée poupée; // Référence à la classe Poupée pour accéder à ses méthodes et variables
    public ParticleSystem têteSang; // Système de particules pour représenter du sang lors d'un tir à la tête

    void Start()
    {
        poupée = transform.root.gameObject.GetComponent<Poupée>(); // Obtient la référence au script Poupée attaché à la racine de cet objet
    }

    void Update()
    {
        // Aucune action dans la mise à jour pour le moment
    }

    // Méthode appelée pour infliger des dégâts à la tête de l'ennemi
    public void TêteSeulCoup()
    {
        têteSang.Play(); // Active l'effet de particules de sang
        poupée.PrendreDommage(50f); // Inflige 50 points de dégâts à l'ennemi
    }
}
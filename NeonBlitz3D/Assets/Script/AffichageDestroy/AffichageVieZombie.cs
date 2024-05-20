using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AffichageVieZombie : MonoBehaviour
{
    [SerializeField] private Slider slider; // Référence au composant Slider utilisé pour afficher la santé
    [SerializeField] private Vector3 offset; // Position de décalage pour la barre de vie (non utilisée dans ce script)

    // Méthode pour mettre à jour la barre de vie en fonction des valeurs de santé actuelles et maximales
    public void UpdateBarreVie(float valeurActuel, float valeurMaximal)
    {
        // Mettre à jour la valeur du slider pour représenter le pourcentage de santé actuel
        slider.value = valeurActuel / valeurMaximal;
    }

    void Update()
    {
        
    }
}
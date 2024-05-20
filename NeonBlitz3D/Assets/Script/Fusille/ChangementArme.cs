using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangementArme : MonoBehaviour
{
    // Indice de l'arme actuellement sélectionnée
    public int armeSélectionnée = 0;

    void Start()
    {
        // Appel de la fonction pour sélectionner l'arme au démarrage
        SelectionnerArme();
    }

    void Update()
    {
        // Sauvegarde de l'indice précédent de l'arme sélectionnée
        int précédentArmeSélectionnée = armeSélectionnée;

        // Gestion de la sélection d'arme via les touches numériques et la molette de la souris
        if (Input.GetKeyDown("1"))
        {
            armeSélectionnée = 0;
        }
        if (Input.GetKeyDown("2"))
        {
            armeSélectionnée = 1;
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (armeSélectionnée >= transform.childCount - 1)
                armeSélectionnée = 0;
            else
                armeSélectionnée++;
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (armeSélectionnée <= 0)
                armeSélectionnée = transform.childCount - 1;
            else
                armeSélectionnée--;
        }

        // Vérification si l'arme sélectionnée a changé
        if (précédentArmeSélectionnée != armeSélectionnée)
            SelectionnerArme();
    }

    // Fonction pour sélectionner l'arme appropriée
    void SelectionnerArme()
    {
        int i = 0;
        foreach (Transform arme in transform)
        {
            if (i == armeSélectionnée)
            {
                arme.gameObject.SetActive(true);
            }
            else
            {
                arme.gameObject.SetActive(false);
            }
            i++;
        }
    }
}

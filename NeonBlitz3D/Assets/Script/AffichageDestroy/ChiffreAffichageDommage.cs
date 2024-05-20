using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChiffreAffichageDommage : MonoBehaviour
{
    // Temps après lequel l'objet sera détruit
    public float DestroyTemps = 1f;

    void Start()
    {
        // Détruit l'objet après un certain temps
        Destroy(gameObject, DestroyTemps);
    }

    void Update()
    {
        
    }
}
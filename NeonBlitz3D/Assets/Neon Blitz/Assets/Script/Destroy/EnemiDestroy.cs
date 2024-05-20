using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiDestroy : MonoBehaviour
{
    [SerializeField] private int pointVie = 100;
    [SerializeField] private GameObject textFlottantPrefab;
    // Start is called before the first frame update

    // Quand le joueur va prendre la pillule rouge, l'ennemi peut prendre 2 fois plus de dommage
    private static bool peutPrendreDoubleDommage = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Une valeur de dommage pris, on va dire 20, le 20 va être converti en String, un texte.
    public void PrendreDommage(int dommage)
    {
        
        if (peutPrendreDoubleDommage)
        {
            dommage *= 2;
        }
        // On passe le chiffre de dommage pris dans notre méthode MontrerDommage()
        MontrerDommage(dommage.ToString());
        pointVie -= dommage;
        if (pointVie <= 0)
        {
            Destroy(gameObject);
            TextNbrEnnemi.textNbrEnnemi.TuerEnnemi();
        }
    }

    void MontrerDommage(string text)
    {
        if (textFlottantPrefab)
        {
            GameObject prefab = Instantiate(textFlottantPrefab, transform.position, Quaternion.identity);
            prefab.GetComponentInChildren<TextMesh>().text = text;
        }
    }

    public static void SetDoubleDommage(bool value)
    {
        peutPrendreDoubleDommage = value;
    }


}

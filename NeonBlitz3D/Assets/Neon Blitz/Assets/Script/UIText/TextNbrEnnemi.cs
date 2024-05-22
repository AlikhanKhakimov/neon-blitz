using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

// Source: https://www.youtube.com/watch?v=YUcvy9PHeXs
public class TextNbrEnnemi : MonoBehaviour
{
    public Text text;
    public Image bgText;

    public Transform[] ennemisListe; // On va utilis� une liste contenant les ennemis
    private int nbrTu�s = 0;

    public static TextNbrEnnemi textNbrEnnemi;
    public static bool haswon = false;

    // Start is called before the first frame update
    void Start()
    {
        UpdateTexte();
    }

    private void Awake()
    {
        textNbrEnnemi = this;
    }
    void Update()
{
    if (ennemisListe.Length == nbrTu�s)
    {
            haswon = true;
    }
}
    

    public void TuerEnnemi()
    {
        nbrTu�s++;
        UpdateTexte();
    }

    void UpdateTexte()
    {
        text.text = nbrTu�s + "/" + ennemisListe.Length + " Ennemis";

        if (ennemisListe.Length == nbrTu�s)
        {
            text.text = "Niveau fini, dirigez-vous vers la voiture.";
        }
    }
}

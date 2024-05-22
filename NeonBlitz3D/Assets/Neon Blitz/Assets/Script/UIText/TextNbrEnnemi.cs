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

    public Transform[] ennemisListe; // On va utilisé une liste contenant les ennemis
    private int nbrTués = 0;

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
    if (ennemisListe.Length == nbrTués)
    {
            haswon = true;
    }
}
    

    public void TuerEnnemi()
    {
        nbrTués++;
        UpdateTexte();
    }

    void UpdateTexte()
    {
        text.text = nbrTués + "/" + ennemisListe.Length + " Ennemis";

        if (ennemisListe.Length == nbrTués)
        {
            text.text = "Niveau fini, dirigez-vous vers la voiture.";
        }
    }
}

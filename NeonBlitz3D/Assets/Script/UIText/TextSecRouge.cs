﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Source: https://www.youtube.com/watch?v=YUcvy9PHeXs
public class TextSecRouge : MonoBehaviour
{
    public static TextSecRouge textSecRouge;

    public Text text;
    public Image imagePilule;
    public Image bgText;

    public float intervalle = 0.3f;
    private float timer;

    private float compteRebours = 5f;
    private bool décompte = false;

    void Start()
    {
        // Quand on commence le niveau, le UI ne s'affiche pas
        MontrerUISecRouge(false);
    }

    void Update()
    {
        if (décompte)
        {
            compteRebours -= Time.deltaTime;
            if (compteRebours <= 0)
            {
                compteRebours = 0;
                décompte = false;
                MontrerUISecRouge(false);
            }
            text.text = FormatTemps(compteRebours);
        }
    }

    private string FormatTemps(float tempsSec)
    {
        int minutes = Mathf.FloorToInt(tempsSec / 60);
        int secondes = Mathf.FloorToInt(tempsSec % 60);
        return string.Format("{0}:{1:00}", minutes, secondes);
    }

    private void Awake()
    {
        textSecRouge = this;
    }

    public void MontrerUISecRouge(bool cas)
    {
        // si false, n'affiche pas
        // si trye, affiche
        bgText.enabled = cas;
        text.enabled = cas;
        imagePilule.enabled = cas;

        if (cas)
        {
            InitierClignotant(); // Clignote quand le UI est montré
            décompte = true;
        }
        else
        {
            ArrêterClignotant(); // Clignote pas quand le UI est caché
            décompte = false;
        }
    }

    private void InitierClignotant()
    {
        timer = 0f;
        InvokeRepeating("CommencerVisibilitéTexte", intervalle, intervalle); // Invoque ToggleTextVisibility avec l'intervalle spécifié
    }

    private void ArrêterClignotant()
    {
        CancelInvoke("CommencerVisibilitéTexte"); // Annuler l'invocation répétée
    }

    private void CommencerVisibilitéTexte()
    {
        text.enabled = !text.enabled; // Commencer la visibilité du texte
    }
}

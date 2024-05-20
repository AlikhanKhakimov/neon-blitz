using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Source: https://www.youtube.com/watch?v=YUcvy9PHeXs
public class TextSecVerte2D : MonoBehaviour
{
    public static TextSecVerte2D textSecVerte2D;


    public Text text2D;
    public Image imagePilule2D;
    public Image bgText2D;

    public float intervalle2D = 0.3f;
    private float timer2D; 

    private float compteRebours2D = 5f;
    private bool décompte2D = false;
    // Start is called before the first frame update
    void Start()
    {
        // Quand on commence le niveau, le UI ne s'affiche pas
        MontrerUISecVerte2D(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (décompte2D)
        {
            compteRebours2D -= Time.deltaTime;
            if (compteRebours2D <= 0)
            {
                compteRebours2D = 0;
                décompte2D = false;
                MontrerUISecVerte2D(false);
            }
            text2D.text = FormatTemps2D(compteRebours2D);
        }
    }

    private string FormatTemps2D(float tempsSec)
    {
        int minutes = Mathf.FloorToInt(tempsSec / 60);
        int secondes = Mathf.FloorToInt(tempsSec % 60);
        return string.Format("{0}:{1:00}", minutes, secondes);
    }

    private void Awake()
    {
        textSecVerte2D = this;
    }

    public void MontrerUISecVerte2D(bool cas)
    {
        // si false, n'affiche pas
        // si trye, affiche
        bgText2D.enabled = cas;
        text2D.enabled = cas;
        imagePilule2D.enabled = cas;

        if (cas)
        {
            InitierClignotant2D(); // Clignote quand le UI est montré
            décompte2D = true;
        }
        else
        {
            ArrêterClignotant2D(); // Clignote pas quand le UI est caché
            décompte2D = false;
        }
    }

    private void InitierClignotant2D()
    {
        timer2D = 0f;
        InvokeRepeating("CommencerVisibilitéTexte", intervalle2D, intervalle2D); // Invoque ToggleTextVisibility avec l'intervalle spécifié
    }

    private void ArrêterClignotant2D()
    {
        CancelInvoke("CommencerVisibilitéTexte"); // Annuler l'invocation répétée
    }

    private void CommencerVisibilitéTexte()
    {
        text2D.enabled = !text2D.enabled; // Commencer la visibilité du texte
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Source: https://www.youtube.com/watch?v=YUcvy9PHeXs
public class TextSecVerte : MonoBehaviour
{
    public static TextSecVerte textSecVerte;


    public Text text;
    public Image imagePilule;
    public Image bgText;

    public float intervalle = 0.3f;
    private float timer;

    private float compteRebours = 5f;
    private bool d�compte = false;
    // Start is called before the first frame update
    void Start()
    {
        // Quand on commence le niveau, le UI ne s'affiche pas
        MontrerUISecVerte(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (d�compte)
        {
            compteRebours -= Time.deltaTime;
            if (compteRebours <= 0)
            {
                compteRebours = 0;
                d�compte = false;
                MontrerUISecVerte(false);
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
        textSecVerte = this;
    }

    public void MontrerUISecVerte(bool cas)
    {
        // si false, n'affiche pas
        // si trye, affiche
        bgText.enabled = cas;
        text.enabled = cas;
        imagePilule.enabled = cas;

        if (cas)
        {
            InitierClignotant(); // Clignote quand le UI est montr�
            d�compte = true;
        }
        else
        {
            Arr�terClignotant(); // Clignote pas quand le UI est cach�
            d�compte = false;
        }
    }

    private void InitierClignotant()
    {
        timer = 0f;
        InvokeRepeating("CommencerVisibilit�Texte", intervalle, intervalle); // Invoque ToggleTextVisibility avec l'intervalle sp�cifi�
    }

    private void Arr�terClignotant()
    {
        CancelInvoke("CommencerVisibilit�Texte"); // Annuler l'invocation r�p�t�e
    }

    private void CommencerVisibilit�Texte()
    {
        text.enabled = !text.enabled; // Commencer la visibilit� du texte
    }
}

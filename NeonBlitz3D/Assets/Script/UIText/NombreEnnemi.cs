using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Source: https://www.youtube.com/watch?v=YUcvy9PHeXs
public class NombreEnnemi : MonoBehaviour
{
    public Text text;
    public Image imageObjectif;
    public Text textObjectif;

    private bool isBlinking = false;

    public Transform[] ennemisListe; // On va utilisé une liste contenant les ennemis
    private int nbrTués = 0;

    public static NombreEnnemi textNbrEnnemi;

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
        if (ennemisListe.Length == nbrTués && !isBlinking)
        {
            StartCoroutine(BlinkObjectif());
        }
    }

    IEnumerator BlinkObjectif()
    {
        isBlinking = true;
        while (true)
        {
            textObjectif.enabled = !textObjectif.enabled;
            imageObjectif.enabled = !imageObjectif.enabled;
            yield return new WaitForSeconds(0.5f);
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
            textObjectif.text = "Évades toi ! Vers l'hélicoptère";
        }
    }
}

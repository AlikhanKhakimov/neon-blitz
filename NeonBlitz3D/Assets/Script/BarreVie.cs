using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarreVie : MonoBehaviour
{
    public Slider slider;
    private bool estMort;

    public GameManagerScript gameManager;
    public GameObject joueur;

    public void SetVieMax(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetVie(int health)
    {
        slider.value = health;
        if (health == 0 && !estMort)
        {
            estMort = true;
            joueur.SetActive(false);
            gameManager.GameOver();
            Debug.Log("Tu es mort !!!");
        }
    }
}

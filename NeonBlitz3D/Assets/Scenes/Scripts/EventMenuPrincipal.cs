using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventMenuPrincipal : MonoBehaviour
{
    public void Quitter() {
        Application.Quit();
    }

    public void Load2DGame()
    {
        SceneManager.LoadScene("Niveau_1");
    }

    public void Load3DGame()
    {
        SceneManager.LoadScene("Level");
    }
}

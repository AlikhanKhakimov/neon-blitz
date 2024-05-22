using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoController : MonoBehaviour
{
    public string sceneToLoad = "Niveau_2";

    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        Debug.Log("Collision détectée avec : " + collision.gameObject.name);
        if (collision.CompareTag("Player") && TextNbrEnnemi.haswon == true) 
        { 
        SceneManager.LoadScene(sceneToLoad);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeliicoptereController : MonoBehaviour
{
    // Nom de la scène à charger lorsque le joueur entre en collision avec l'hélicoptère
    public string sceneToLoad = "Scene2";

    void OnTriggerEnter(Collider other)
    {
        // Affiche un message dans la console indiquant la détection d'une collision avec un objet
        Debug.Log("Collision détectée avec : " + other.gameObject.name);

        // Vérifie si l'objet en collision est étiqueté en tant que "Player"
        if (other.CompareTag("Player"))
        {
            // Affiche un message dans la console indiquant que le joueur est entré en collision avec l'hélicoptère
            Debug.Log("Le joueur est entré en collision avec l'hélicoptère !");

            // Charge la scène spécifiée
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
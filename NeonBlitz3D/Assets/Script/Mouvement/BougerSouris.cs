using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BougerSouris : MonoBehaviour
{
    // Sensibilité de la souris pour le mouvement de la caméra
    public float sensibilitéSouris = 100f;

    // Référence au corps du joueur pour la rotation horizontale
    public Transform joueurCorps;

    // Rotation actuelle autour de l'axe X
    float xRotation = 0f;

    void Start()
    {
        // Cacher le curseur et le verrouiller au centre de l'écran au démarrage
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Récupération des mouvements de la souris
        float sourisX = Input.GetAxis("Mouse X") * sensibilitéSouris * Time.deltaTime;
        float sourisY = Input.GetAxis("Mouse Y") * sensibilitéSouris * Time.deltaTime;

        // Calcul de la rotation autour de l'axe X avec restriction entre -90 et 90 degrés
        xRotation -= sourisY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Rotation de la caméra locale selon la rotation autour de l'axe X
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        // Rotation du corps du joueur autour de l'axe vertical (Y)
        joueurCorps.Rotate(Vector3.up * sourisX);

    }
}
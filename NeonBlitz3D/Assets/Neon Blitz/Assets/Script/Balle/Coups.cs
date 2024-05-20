using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coups : MonoBehaviour
{
    AudioManager audioManager;

    public GameObject balle;
    public Transform balleTransform;
    public bool peurTiré;
    private float minuteur; //Combien de fois peut tiré
    public float tempsEntreTirs;
    // Start is called before the first frame update

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!peurTiré) {
            // Augmente avec le temps du jeux. Suit les secondes, le temps du jeu
            minuteur += Time.deltaTime;
            if (minuteur > tempsEntreTirs) {
                peurTiré = true;
                minuteur = 0;
            }
        }
        // Si on clique droit et on peut cliqué
        if (Input.GetMouseButton(0) && peurTiré) {
            peurTiré = false;
            // Instancier la balle
            //audioManager.PlaySFX(audioManager.gunfire);
            Instantiate(balle, balleTransform.position, Quaternion.identity);
        }
    }
}

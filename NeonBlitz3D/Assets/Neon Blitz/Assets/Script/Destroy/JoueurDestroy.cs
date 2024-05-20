using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoueurDestroy : MonoBehaviour
{
    [SerializeField] private int pointVie = 100;
    [SerializeField] private GameObject textFlottantPrefab;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private bool isDead;

    public GameManagerScript gameManager;

    private static bool peutPrendreMoitiéDommage = false;

    void Start()
    {

    }

    void Update()
    {

    }

    public void PrendreDommage(int dommage)
    {
        if (peutPrendreMoitiéDommage)
        {
            dommage /= 2;
        }

        MontrerDommage(dommage.ToString());
        pointVie -= dommage;
        if (pointVie <= 0 && !isDead)
        {
            isDead = true;
            audioManager.PlaySFX(audioManager.death);
            gameObject.SetActive(false);
            gameManager.GameOver();
            Debug.Log("Dead");
        }
    }

    void MontrerDommage(string text)
    {
        if (textFlottantPrefab)
        {
            GameObject prefab = Instantiate(textFlottantPrefab, transform.position, Quaternion.identity);
            prefab.GetComponentInChildren<TextMesh>().text = text;
        }
    }

    public static void SetMoitiéDommage(bool value)
    {
        peutPrendreMoitiéDommage = value;
    }
}

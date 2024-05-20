using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmeFusille : MonoBehaviour
{
    public float dommage = 10f;
    public float cadenceTir = 4f;

    public int maxClipMunition = 10;
    public int maxBackMunition = 25;
    public int actuelRetourMunitions;
    private int actuelClipMunition;
    public float tempsRechargement = 2f;
    private bool estRecharger = false;
    public Text munitionsAffichage;
    public Text affichageTousMunitions;

    public ParticleSystem museauFlash;
    public Camera fpsCam;

    private float prochaineFoisTirer = 0f;

    public AudioSource source;
    public AudioClip autogunshot, akreload;

    void Start()
    {
        // Initialisation des munitions au démarrage
        actuelClipMunition = maxClipMunition;
        actuelRetourMunitions = maxBackMunition;
    }

    void OnEnable()
    {
        // Désactivation du rechargement au début
        estRecharger = false;
    }

    void Update()
    {
        // Gestion de la recharge si la touche "r" est pressée
        // et s'il y a des munitions dans le stockage et que le chargeur n'est pas plein
        if (Input.GetKeyDown("r") && actuelRetourMunitions > 0 && actuelClipMunition != maxClipMunition)
        {
            StartCoroutine(Recharger());
        }

        // Mise à jour de l'affichage des munitions
        munitionsAffichage.text = actuelClipMunition.ToString();
        affichageTousMunitions.text = actuelRetourMunitions.ToString();

        // Retour si en train de recharger
        if (estRecharger)
            return;

        // Recharge automatique si le chargeur est vide et il y a des munitions dans le stockage
        if (actuelClipMunition <= 0 && actuelRetourMunitions > 0)
        {
            StartCoroutine(Recharger());
            return;
        }

        // Tir si la touche de tir est maintenue et si le chargeur n'est pas vide
        if (Input.GetButton("Fire1") && Time.time >= prochaineFoisTirer && actuelClipMunition > 0)
        {
            source.clip = autogunshot;
            source.Play();
            prochaineFoisTirer = Time.time + 1f / cadenceTir;
            Shoot();
        }
    }

    // Coroutine pour le rechargement
    IEnumerator Recharger()
    {
        estRecharger = true;
        source.clip = akreload;
        source.Play();
        Debug.Log("Rechargement...");

        yield return new WaitForSeconds(tempsRechargement);

        int ammoToReload = maxClipMunition - actuelClipMunition;
        if (actuelRetourMunitions < ammoToReload)
        {
            actuelClipMunition += actuelRetourMunitions;
            actuelRetourMunitions = 0;
        }
        else
        {
            actuelClipMunition = maxClipMunition;
            actuelRetourMunitions -= ammoToReload;
        }

        estRecharger = false;
    }

    // Méthode pour recharger les munitions
    public void RechargeMunitions()
    {
        actuelRetourMunitions = maxBackMunition;
    }

    // Méthode pour effectuer un tir
    void Shoot()
    {
        museauFlash.Play();

        actuelClipMunition--;

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit))
        {
            Debug.Log(hit.transform.name);

            // Gestion des dommages infligés aux ennemis et aux objets
            Ennemi enemy = hit.transform.GetComponent<Ennemi>();
            if (enemy != null)
                enemy.PrendreDommage(dommage);

            Poupée ragdoll = hit.transform.GetComponent<Poupée>();
            if (ragdoll != null)
                ragdoll.PrendreDommage(dommage);

            TirTête headshot = hit.transform.GetComponent<TirTête>();
            if (headshot != null)
                headshot.TêteSeulCoup();
        }
    }

    // Méthode pour ajouter des dommages supplémentaires
    public void AjouterDommage(float dommageAjoute)
    {
        dommage += dommageAjoute;
    }
}
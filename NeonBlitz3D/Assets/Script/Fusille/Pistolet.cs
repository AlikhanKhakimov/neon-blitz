using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Pistolet : MonoBehaviour
{
    public float dommage = 20f;
    public float cadenceTir = 4f;

    public int maxClipMunition = 10;
    public int maxBackMunition = 25;
    public int actuelRetourMunitions;
    private int actuelClipMunition;
    public float tempsRechargement = 2f;
    private bool estReloading = false;
    public Text munitionsAffichage;
    public Text affichageTousMunitions;

    public ParticleSystem museauFlash;
    public Camera fpsCam;

    private float prochaineFoisTirer = 0f;

    public AudioSource source;
    public AudioClip gunshot, glockreload;

    void Start()
	{
        actuelClipMunition = maxClipMunition;
        actuelRetourMunitions = maxBackMunition;
	}

    void OnEnable()
	{
        estReloading = false;
	}

	void Update()
    {
		if (Input.GetKeyDown("r") && actuelRetourMunitions > 0 && actuelClipMunition != maxClipMunition)
		{
            StartCoroutine(Recharger());
		}

        munitionsAffichage.text = actuelClipMunition.ToString();
        affichageTousMunitions.text = actuelRetourMunitions.ToString();

        if (estReloading)
            return;

        if(actuelClipMunition <= 0 && actuelRetourMunitions > 0)
		{
            StartCoroutine(Recharger());
            return;
		}

        if (Input.GetButtonDown("Fire1") && Time.time >= prochaineFoisTirer && actuelClipMunition > 0)
        {
            source.clip = gunshot;
            source.Play();
            prochaineFoisTirer = Time.time + 1f / cadenceTir;
            Tirer();
        }
    }

    IEnumerator Recharger()
    {
        estReloading = true;
        source.clip = glockreload;
        source.Play();

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

        estReloading = false;
	}
    public void RechargerMunitions()
    {
        actuelRetourMunitions = maxBackMunition;
    }

    void Tirer()
    {
        museauFlash.Play();

        actuelClipMunition--;

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit))
        {
            Debug.Log(hit.transform.name);

            Ennemi ennemi = hit.transform.GetComponent<Ennemi>();
            if (ennemi != null)
                ennemi.PrendreDommage(dommage);

            Poupée poupée = hit.transform.GetComponent<Poupée>();
            if (poupée != null)
                poupée.PrendreDommage(dommage);

            TirTête tirTête = hit.transform.GetComponent<TirTête>();
            if (tirTête != null)
                tirTête.TêteSeulCoup();
        }
	}

    public void AjouterDommage(float dommageAjoute)
    {
        dommage += dommageAjoute;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatistiqueJoueur : MonoBehaviour
{
	public int joueurMaxVie = 100;
	public int joueurVie;

	public BarreVie barreVie;
	public Pistolet pistolet;
	public ArmeFusille fusillade;

	public AudioSource source;
	public AudioClip heal, ammopickup;

	void Start()
	{
		joueurVie = joueurMaxVie;
		barreVie.SetVieMax(joueurMaxVie);
	}

	public void PrendreDommageJoueur(int damage)
	{
		joueurVie -= damage;

		barreVie.SetVie(joueurVie);
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.tag == "medkit")
		{
			Debug.Log("Medkit picked up");
			Destroy(collider.gameObject);
			barreVie.SetVieMax(joueurMaxVie);
			joueurVie = joueurMaxVie;
			source.clip = heal;
			source.Play();
		}
		if (collider.gameObject.tag == "ammo")
		{
			Debug.Log("Ammo picked up");
			Destroy(collider.gameObject);
			pistolet.RechargerMunitions();
			fusillade.RechargeMunitions();
			source.clip = ammopickup;
			source.Play();
		}	
	}
}

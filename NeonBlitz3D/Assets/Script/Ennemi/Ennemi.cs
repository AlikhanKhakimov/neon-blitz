using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ennemi : MonoBehaviour
{
	public float ennemiVie = 30f;
	public NavMeshAgent nav;
	public Transform cible;

	public GameObject zombie;
	public Rigidbody rb;

	public float portéeVue = 10f;
	public float plageAttaque = 2.5f;

	private float prochaineTempsAttaque = 0f;
	private float tauxAttaque = 0.5f;

	public StatistiqueJoueur statistiqueJoueur;

	void Start()
	{
		nav = GetComponent<NavMeshAgent>();
	}

	void Update()
	{
		float distance = Vector3.Distance(cible.position, transform.position);

		if (ennemiVie > 0 && distance <= portéeVue)
			nav.SetDestination(cible.position);

		if (ennemiVie > 0 && distance <= plageAttaque && Time.time >= prochaineTempsAttaque)
		{
			prochaineTempsAttaque = Time.time + 1f / tauxAttaque;
			statistiqueJoueur.PrendreDommageJoueur(20);
		}
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, portéeVue);
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(transform.position, plageAttaque);
	}
	public void PrendreDommage(float quantité)
	{
		ennemiVie -= quantité;

		if (ennemiVie <= 0f)
		{
			Mourir();
		}
	}


	void Mourir()
	{
		nav.enabled = false;

		rb = zombie.GetComponent<Rigidbody>();
		rb.isKinematic = false;

		rb.AddForce(-transform.forward * 150);

		Invoke(nameof(DeleteObject), 2);
	}
	void DeleteObject()
	{
		Destroy(gameObject);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Ce script gère le comportement d'un ennemi dans le jeu.
// Il utilise le système de navigation pour suivre et attaquer le joueur lorsque celui-ci est à portée.

public class Poupée1 : MonoBehaviour
{
    public GameObject textFlotantPrefab;

    public float ennemiVie, vieMax = 30f;
    public NavMeshAgent nav;
    public Transform cible;

    public GameObject zombie;

    public float portéeVue = 10f;
    public float plageAttaque = 2.5f;
    public float speed = 3.5f; // Added speed variable

    private float prochaineTempsAttaque = 0f;
    private float tauxAttaque = 0.5f;

    public StatistiqueJoueur statistiqueJoueur;

    public Collider collider1;
    public Rigidbody rigidbody1;

    public Animator animator;
    public bool chasserJoueur = false;
    public bool attackPlayer = false;

    [SerializeField] AffichageVieZombie barreVie;

    private void Awake()
    {
        rigidbody1 = GetComponent<Rigidbody>();
        barreVie = GetComponentInChildren<AffichageVieZombie>();
    }

    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        SetKinematic(true);
        SetColliderTrigger(true);
        animator = GetComponent<Animator>();

        //barreVie.UpdateBarreVie(ennemiVie, vieMax);
    }

    void SetKinematic(bool newValue)
    {
        Rigidbody[] bodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in bodies)
        {
            rb.isKinematic = newValue;
        }
    }

    void SetColliderTrigger(bool newValue)
    {
        Collider[] colliders = GetComponentsInChildren<Collider>();
        foreach (Collider coll in colliders)
        {
            coll.isTrigger = newValue;
        }
    }

    void Update()
    {
        float distance = Vector3.Distance(cible.position, transform.position);

        if (ennemiVie > 0 && distance <= portéeVue)
        {
            chasserJoueur = true;
        }

        if (ennemiVie > 0 && distance <= plageAttaque && Time.time >= prochaineTempsAttaque)
        {
            prochaineTempsAttaque = Time.time + 1f / tauxAttaque;
            statistiqueJoueur.PrendreDommageJoueur(20);

            animator.SetBool("isPunching", true);
            animator.SetBool("isWalking", false);
        }

        if (chasserJoueur && ennemiVie > 0 && distance >= plageAttaque)
        {
            nav.SetDestination(cible.position);
            nav.speed = speed; // Set the speed here

            animator.SetBool("isPunching", false);
            animator.SetBool("isWalking", true);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, portéeVue);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, plageAttaque);
    }

    public void PrendreDommage(float amount)
    {
        ennemiVie -= amount;
        //barreVie.UpdateBarreVie(ennemiVie, vieMax);

        if (barreVie != null)
        {
            barreVie.UpdateBarreVie(ennemiVie, vieMax);
            if (ennemiVie <= 0)
            {
                Destroy(barreVie.gameObject);
            }
        }

        if (textFlotantPrefab)
        {
            montrerTextFlotantPrefab(amount);
        }

        if (ennemiVie <= 0f)
        {
            Mourir();
        }
    }

    void montrerTextFlotantPrefab(float dommage)
    {
        var go = Instantiate(textFlotantPrefab, transform.position, Quaternion.identity, transform);
        go.GetComponent<TextMesh>().text = dommage.ToString();

        Vector3 directionVersJoueur = (cible.position - transform.position).normalized;

        Quaternion regardRotation = Quaternion.LookRotation(directionVersJoueur);

        go.transform.rotation = regardRotation;
        go.transform.Rotate(Vector3.up, 180f);
    }

    void Mourir()
    {
        Debug.Log("Ragdoll mort");
        nav.enabled = false;

        //rigidbody1 = zombie.GetComponent<Rigidbody>();
        //rigidbody1.isKinematic = false;
        //rigidbody1.AddForce(-transform.forward * 1500);

        Destroy(collider1);
        Destroy(rigidbody1);
        GetComponent<Animator>().enabled = false;

        SetKinematic(false);
        SetColliderTrigger(false);


        Invoke(nameof(DeleteObject), 5);
    }

    void DeleteObject()
    {
        Destroy(gameObject);
    }
}
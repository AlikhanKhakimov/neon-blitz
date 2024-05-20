using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Ce script gère le comportement d'un ennemi dans le jeu.
// Il utilise le système de navigation pour suivre et attaquer le joueur lorsque celui-ci est à portée.

public class Poupée : MonoBehaviour
{
    // Prefab pour le texte flottant des dégâts
    public GameObject textFlotantPrefab;

    // Points de vie de l'ennemi et sa valeur maximale
    public float ennemiVie, vieMax = 30f;

    // Composants nécessaires pour la navigation de l'ennemi
    public NavMeshAgent nav;
    public Transform cible;

    // GameObject du zombie (ennemi)
    public GameObject zombie;

    // Portée de vue et plage d'attaque de l'ennemi
    public float portéeVue = 10f;
    public float plageAttaque = 2.5f;

    // Vitesse de déplacement de l'ennemi
    public float vitesse = 1.5f;

    // Variables pour la gestion des attaques
    private float prochaineTempsAttaque = 0f;
    private float tauxAttaque = 0.5f;

    // Référence à la statistique du joueur
    public StatistiqueJoueur statistiqueJoueur;

    // Collider et Rigidbody de l'ennemi
    public Collider collider1;
    public Rigidbody rigidbody1;

    // Animator de l'ennemi
    public Animator animator;

    // Booléens pour contrôler les états de l'ennemi
    public bool chasserJoueur = false;
    public bool attackPlayer = false;

    // Référence à la barre de vie de l'ennemi
    [SerializeField] AffichageVieZombie barreVie;

    private void Awake()
    {
        rigidbody1 = GetComponent<Rigidbody>();
        barreVie = GetComponentInChildren<AffichageVieZombie>();
    }

    // Méthode appelée au démarrage de l'exécution du jeu.
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        SetKinematic(true);
        SetColliderTrigger(true);
        animator = GetComponent<Animator>();
    }

    // Désactive la physique des composants Rigidbody de l'ennemi.
    void SetKinematic(bool newValue)
    {
        Rigidbody[] bodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in bodies)
        {
            rb.isKinematic = newValue;
        }
    }

    // Désactive les collisions physiques des colliders de l'ennemi.
    void SetColliderTrigger(bool newValue)
    {
        Collider[] colliders = GetComponentsInChildren<Collider>();
        foreach (Collider coll in colliders)
        {
            coll.isTrigger = newValue;
        }
    }

    // Méthode appelée à chaque frame pour mettre à jour le comportement de l'ennemi.
    void Update()
    {
        float distance = Vector3.Distance(cible.position, transform.position);

        // Si l'ennemi est en vie et que le joueur est à portée de vue, commence à le chasser.
        if (ennemiVie > 0 && distance <= portéeVue)
        {
            chasserJoueur = true;
        }

        // Si l'ennemi est en vie et que le joueur est à portée d'attaque, attaque le joueur.
        if (ennemiVie > 0 && distance <= plageAttaque && Time.time >= prochaineTempsAttaque)
        {
            prochaineTempsAttaque = Time.time + 1f / tauxAttaque;
            statistiqueJoueur.PrendreDommageJoueur(20);

            animator.SetBool("isPunching", true);
            animator.SetBool("isWalking", false);
        }

        // Si l'ennemi est en mode chasse et que le joueur est hors de portée d'attaque, poursuit le joueur.
        if (chasserJoueur && ennemiVie > 0 && distance >= plageAttaque)
        {
            nav.SetDestination(cible.position);
            nav.speed = vitesse;

            animator.SetBool("isPunching", false);
            animator.SetBool("isWalking", true);
        }
    }

    // Dessine des gizmos pour visualiser la portée de vue et la plage d'attaque de l'ennemi dans l'éditeur Unity.
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, portéeVue);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, plageAttaque);
    }

    // Méthode appelée lorsque l'ennemi subit des dégâts.
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
            NombreEnnemi.textNbrEnnemi.TuerEnnemi();
        }
    }

    // Affiche un texte flottant pour indiquer les dégâts infligés à l'ennemi.
    void montrerTextFlotantPrefab(float dommage)
    {
        var go = Instantiate(textFlotantPrefab, transform.position, Quaternion.identity, transform);
        go.GetComponent<TextMesh>().text = dommage.ToString();

        Vector3 directionVersJoueur = (cible.position - transform.position).normalized;

        Quaternion regardRotation = Quaternion.LookRotation(directionVersJoueur);

        go.transform.rotation = regardRotation;
        go.transform.Rotate(Vector3.up, 180f);
    }

    // Méthode appelée lorsque l'ennemi est tué.
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

        // Détruit l'objet de l'ennemi après un certain délai.
        Invoke(nameof(DeleteObject), 5);
    }

    // Détruit l'objet de l'ennemi après un certain délai.
    void DeleteObject()
    {
        Destroy(gameObject);
    }
}
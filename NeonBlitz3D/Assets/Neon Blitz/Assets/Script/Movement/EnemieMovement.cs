using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemieMovement : MonoBehaviour
{
    public GameObject joueur;
    public Transform[] pointsPatrouille;
    public float vitesse;
    public float distanceEntreJoueurEnnemi;

    private int pointCible;
    private bool àLaPoursuite = false;

    //Source: https://www.youtube.com/watch?v=ouzkNDIXg3I
    public GameObject balleEnnemi;
    private float tempsEntreBalles;
    public float commencerFusillade;

    void Start()
    {
        pointCible = 0;
    }

    void Update()
    {

        if (!àLaPoursuite)
        {
            Patrouille();
        }
        else
        {
            Poursuite();
            Tiré();
        }
    }

    void Patrouille()
    {
        // On prend la position actuelle de l'ennemi et on lui donne les points ou allez avec une certaine vitesse
        transform.position = Vector2.MoveTowards(transform.position, pointsPatrouille[pointCible].position, vitesse * Time.deltaTime);

        // Quand l'ennemi se rend sur le premier point de patrouille, il va aller vers le prochain, deuxième, puis troisième(on a juste 3 points pour chaque en)
        if (Vector2.Distance(transform.position, pointsPatrouille[pointCible].position) < 0.1f)
        {
            // Quand l'ennemi a passé sur le troisième point, le dernier, il va vers le premier
            pointCible = (pointCible + 1) % pointsPatrouille.Length;
        }

        // Si le joueur est proche, l'ennemi passe en mode poursuite
        if (Vector2.Distance(transform.position, joueur.transform.position) < distanceEntreJoueurEnnemi)
        {
            àLaPoursuite = true;
        }
    }

    void Poursuite()
    {
        // Déplacez l'ennemi vers le joueur
        transform.position = Vector3.MoveTowards(transform.position, joueur.transform.position, vitesse * Time.deltaTime);

        // Calculer la direction vers le joueur
        Vector3 direction = (joueur.transform.position - transform.position).normalized;

        // Faire face à l'ennemi vers le joueur
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Vérifiez si le joueur est hors de portée de détection pour revenir en mode patrouille
        if (Vector2.Distance(transform.position, joueur.transform.position) > distanceEntreJoueurEnnemi * 1.2f)
        {
            àLaPoursuite = false;
        }
    }

    void Tiré() {
        // L'ennemi, quand il est en face du joueur, la balle va allez en face de l'ennemi vers le joueur
        Vector2 direction = new Vector2(joueur.transform.position.x - transform.position.x, joueur.transform.position.y - transform.position.y);
        transform.up = direction;
        if (tempsEntreBalles <= 0)
        {
            Instantiate(balleEnnemi, transform.position, transform.rotation);
            tempsEntreBalles = commencerFusillade;
        }
        else {
            tempsEntreBalles -= Time.deltaTime;
        }
    }
}

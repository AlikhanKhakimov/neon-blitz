using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class JoueurrMouvement : MonoBehaviour
{
    public CharacterController controller;

    public float vitesse;
    public float gravité = -9.81f;
    public float hauteurSaut = 3f;

    public Transform contrôleTerre;
    public float distanceSol = 0.4f;
    public LayerMask masqueSol;    

    Vector3 velocité;
    bool estMisTerre;

    public static bool isDoubleSpeed = false;
    public float vitesseNormal;

    void Start()
    {
        vitesseNormal = vitesse;
    }
    void Update()
    {
        estMisTerre = Physics.CheckSphere(contrôleTerre.position, distanceSol, masqueSol);

        if (isDoubleSpeed)
        {
            vitesse = vitesseNormal * 1.2f;
        }
        else
        {
            vitesse = vitesseNormal;
        }

        if (estMisTerre && velocité.y < 0)
        {
            velocité.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * vitesse * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && estMisTerre)
		{
            velocité.y = Mathf.Sqrt(hauteurSaut * -2f * gravité);
		}

        velocité.y += gravité * Time.deltaTime;

        controller.Move(velocité * Time.deltaTime);
    }

    public static void SetDoubleVitesse(bool value)
    {
        isDoubleSpeed = value;
    }
}

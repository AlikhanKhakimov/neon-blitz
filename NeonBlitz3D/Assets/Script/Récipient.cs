using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Récipient : MonoBehaviour
{
    public GameObject container;
    public float rotationVitesse = 180f;

    void Update()
    {
        container.transform.Rotate(Vector3.up * rotationVitesse * Time.deltaTime);
    }
}
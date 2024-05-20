using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Source: https://www.youtube.com/watch?v=YUcvy9PHeXs
public class TextSecVerte : MonoBehaviour
{
    public static TextSecVerte textSecVerte;


    public Image imagePilule;
    public Image bgText;
    // Start is called before the first frame update
    void Start()
    {
        MontrerUISecVerte(false);
    }

    // Update is called once per frame
    void Update()
    {
       
    }


    private void Awake()
    {
        textSecVerte = this;
    }

    public void MontrerUISecVerte(bool cas)
    {
        bgText.enabled = cas;
        imagePilule.enabled = cas;
    }
}

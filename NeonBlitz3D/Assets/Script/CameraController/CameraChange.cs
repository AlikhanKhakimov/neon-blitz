using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
    public GameObject troisieme;
    public GameObject premiere;
    public int CamMode;

    void Update()
    {
        if (Input.GetButtonDown("Camera")) {
            if (CamMode == 1)
            {
                CamMode = 0;
            }
            else {
                CamMode += 1;
            }
            StartCoroutine(CamChange());
        }
    }

    IEnumerator CamChange() {
        yield return new WaitForSeconds(0.01f);
        if (CamMode == 0) {
            troisieme.SetActive(true);
            premiere.SetActive(false);
        }
        if (CamMode == 1) {
            troisieme.SetActive(false);
            premiere.SetActive(true);
        }
    }
}

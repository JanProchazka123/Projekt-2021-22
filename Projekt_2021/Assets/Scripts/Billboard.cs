using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public GameObject cam;

    private void Awake()
    {
        //billboard hled� GameObject kterej se jmenuje p�esn� PlayerCamera - to by se mohlo rozb�t
        cam = GameObject.Find("Player Camera");
    }

    void LateUpdate()
    {
        transform.LookAt(transform.position + cam.transform.forward);
    }
}

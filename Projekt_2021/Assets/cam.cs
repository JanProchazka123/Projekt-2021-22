using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cam : MonoBehaviour
{
  public Transform target;
    

    private Vector3 offset = new Vector3(0, 6, -15);
  // some offset from the target, in this case it's behind the object



    void Update()

    {



        transform.position = target.TransformPoint(offset);

        transform.rotation = target.rotation;



    }
}

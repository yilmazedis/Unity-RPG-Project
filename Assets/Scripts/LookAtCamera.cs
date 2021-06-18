using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{

    Transform perspective;
    private Camera targetCamera;

    void Start()
    {
        targetCamera = Camera.main;    
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.rotation = targetCamera.transform.rotation;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector3 cameraOffset = new Vector3(0, 1.3f, -3.0f);
    private Transform target;

    private void Start()
    {
        this.target = GameObject.Find("Player").transform;
    }

    // Ultimo que se ejecuta
    private void LateUpdate()
    {
        this.transform.position = this.target.TransformPoint(this.cameraOffset);
        // Gira la camara a la dirección que mira el personaje
        this.transform.LookAt(this.target);
    }
}

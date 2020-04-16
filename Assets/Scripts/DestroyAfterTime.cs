using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float destroyDelay = 1.5f;

    private void Start()
    {
        Destroy(this.gameObject, this.destroyDelay);
    }
}

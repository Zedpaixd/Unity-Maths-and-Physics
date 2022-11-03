using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewCamera : MonoBehaviour
{
    private Transform planetTarget;
    private Vector3 offset;

    public void Initialize(Transform target, Vector3 offset_) 
    {
        planetTarget = target;
        offset = offset_;
    }

    private void Update()
    {
        transform.position = planetTarget.position + offset;
        transform.LookAt(planetTarget);
    }
}

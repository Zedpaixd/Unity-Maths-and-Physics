using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RayTransform
{
    public Vector3 rayPosition;
    public Vector3 rayDirection;
    public float rayDistance;
    public Transform transform;

    public RayTransform() { }

    public RayTransform(Vector3 pos, Vector3 dir, float dist)
    {
        rayPosition = pos;
        rayDirection = dir;
        rayDistance = dist;
    }

    public RayTransform(Vector3 pos, Vector3 dir, float dist, Transform transform)
    {
        rayPosition = pos;
        rayDirection = dir;
        rayDistance = dist;
        this.transform = transform;
    }

}
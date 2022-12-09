using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    public Transform cam;

    private void FixedUpdate()
    {
        transform.LookAt(cam);
        transform.Rotate(new Vector3(1, 0, 0), 90);
    }
}

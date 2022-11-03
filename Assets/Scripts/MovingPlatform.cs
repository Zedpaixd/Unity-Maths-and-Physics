using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float range;
    public float period;

    // Update is called once per frame
    void Update()
    {
        transform.localPosition += Vector3.right * range * Mathf.Cos(Time.time * period);
    }
}

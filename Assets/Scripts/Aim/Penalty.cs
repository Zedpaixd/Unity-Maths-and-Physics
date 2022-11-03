using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Penalty : MonoBehaviour
{
    public float rotationSpeed = 20f;
    public Vector3 hitPoint;
    public Transform target;
    Vector3 positionInOtherSpace;
    private void Update()
    {
        //the side of the cube that is the penalty uses the same script,
        //but without the target assigned 
        if (!target)
            return;

        Rotate();
        Move();
    }

    public void Initialize(Vector3 hitPos, Transform parent)
    {
        hitPoint = hitPos;
        target = parent;

        //convert hitPoint from world space coords to local space
        //where the "parent" is the target (so hitPoint is converted from
        //world space to local space in regards to the target)
        positionInOtherSpace = target.InverseTransformPoint(hitPoint);
    }

    void Rotate()
    {
        //Vector3 rand = new Vector3(Random.value * Random.Range(0, rotationSpeed),
        //    Random.value * Random.Range(0, rotationSpeed), 
        //    Random.value * Random.Range(0, rotationSpeed));

        //transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        transform.rotation = target.rotation;
    }

    void Move()
    {
        //convert the position calculated in the Initialize method
        // back to world space coords (again in regards to the target)
        transform.position = target.TransformPoint(positionInOtherSpace);
    }
}

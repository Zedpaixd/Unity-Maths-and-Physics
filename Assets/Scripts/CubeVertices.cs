using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CubeVertices : MonoBehaviour
{
    public Vector3[] vertices;
    private Vector3 prevPos;
    private Quaternion prevRot;

    private void Start()
    {
        //RecalculateVertices();
        //prevPos = transform.position;
        //prevRot = transform.rotation;
    }

    private void Update()
    {
        //for (int i = 0; i < vertices.Length; i++)
        //{
        //    Debug.DrawLine(transform.position, vertices[i], Color.red);
        //}

        ////if (transform.position == prevPos || transform.rotation == prevRot)
        ////    return;

        //RecalculateVertices();
        //prevRot = transform.rotation;
        //prevPos = transform.position;

        GameObject t = GameObject.CreatePrimitive(PrimitiveType.Cube);
        t.transform.position = transform.onCube();
        t.transform.localScale = Vector3.one * 0.2f;


    }

    //void RecalculateVertices()
    //{
    //    vertices = new Vector3[8];
    //    //vertices of top side
    //    vertices[0] = new Vector3(0.5f, 0.5f, 0.5f) + transform.position;
    //    vertices[1] = new Vector3(-0.5f, 0.5f, 0.5f) + transform.position;
    //    vertices[2] = new Vector3(0.5f, 0.5f, -0.5f) + transform.position;
    //    vertices[3] = new Vector3(-0.5f, 0.5f, -0.5f) + transform.position;

    //    //vertices of bottom side:
    //    vertices[4] = new Vector3(0.5f, -0.5f, 0.5f) + transform.position;
    //    vertices[5] = new Vector3(-0.5f, -0.5f, 0.5f) + transform.position;
    //    vertices[6] = new Vector3(0.5f, -0.5f, -0.5f) + transform.position;
    //    vertices[7] = new Vector3(-0.5f, -0.5f, -0.5f) + transform.position;

    //    Debug.Log("recalculated vertices");
    //}
}

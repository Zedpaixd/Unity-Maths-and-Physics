using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{
    public static Vector3 onCube(this Transform transform)
    {
        Vector3 position = new Vector3();
        Vector3[] vertices = new Vector3[8];
        //vertices of top side
        vertices[0] = new Vector3(
            0.5f * transform.localScale.x,
            0.5f * transform.localScale.y,
            0.5f * transform.localScale.z);
        vertices[1] = new Vector3(
            -0.5f * transform.localScale.x,
            0.5f * transform.localScale.y,
            0.5f * transform.localScale.z);
        vertices[2] = new Vector3(
            0.5f * transform.localScale.x,
            0.5f * transform.localScale.y,
            -0.5f * transform.localScale.z);
        vertices[3] = new Vector3(
            -0.5f * transform.localScale.x,
            0.5f * transform.localScale.y,
            -0.5f * transform.localScale.z);

        //vertices of bottom side:
        vertices[4] = new Vector3
            (0.5f * transform.localScale.x,
            -0.5f * transform.localScale.y,
            0.5f * transform.localScale.z);
        vertices[5] = new Vector3(
            -0.5f * transform.localScale.x,
            -0.5f * transform.localScale.y,
            0.5f * transform.localScale.z);
        vertices[6] = new Vector3(
            0.5f * transform.localScale.x,
            -0.5f * transform.localScale.y,
            -0.5f * transform.localScale.z);
        vertices[7] = new Vector3(
            -0.5f * transform.localScale.x,
            -0.5f * transform.localScale.y,
            -0.5f * transform.localScale.z);

        int side = Random.Range(0, 6);
        //cube has 6 sides but range is max exclusive
        //0 = top, 1 = bottom, 2 = right, 3 = left, 4 = front, 5 = back
        //we interpolate between the 2 edges of each side linearly
        //then we interpolate linearly between the 2 points we got (1 point per side)

        //interpolation values
        float interpolateOnFirstEdge = Random.value;
        float interpolateOnSecondEdge = Random.value;
        float interpolateBetweenEdges = Random.value;

        //edges
        Vector3 firstEdge;
        Vector3 secondEdge;
        switch (side)
        {
            case 0:
                firstEdge = vertices[0] * interpolateOnFirstEdge + vertices[1] * (1 - interpolateOnFirstEdge);
                secondEdge = vertices[2] * interpolateOnSecondEdge + vertices[2] * (1 - interpolateOnSecondEdge);
                position = firstEdge * interpolateBetweenEdges + secondEdge * (1 - interpolateBetweenEdges);
                position = transform.rotation * position;
                position += transform.position;
                break;
            case 1:
                firstEdge = vertices[4] * interpolateOnFirstEdge + vertices[5] * (1 - interpolateOnFirstEdge);
                secondEdge = vertices[6] * interpolateOnSecondEdge + vertices[7] * (1 - interpolateOnSecondEdge);
                position = firstEdge * interpolateBetweenEdges + secondEdge * (1 - interpolateBetweenEdges);
                position = transform.rotation * position;
                position += transform.position;
                break;
            case 2:
                firstEdge = vertices[0] * interpolateOnFirstEdge + vertices[2] * (1 - interpolateOnFirstEdge);
                secondEdge = vertices[4] * interpolateOnSecondEdge + vertices[6] * (1 - interpolateOnSecondEdge);
                position = firstEdge * interpolateBetweenEdges + secondEdge * (1 - interpolateBetweenEdges);
                position = transform.rotation * position;
                position += transform.position;
                break;
            case 3:
                firstEdge = vertices[1] * interpolateOnFirstEdge + vertices[3] * (1 - interpolateOnFirstEdge);
                secondEdge = vertices[5] * interpolateOnSecondEdge + vertices[7] * (1 - interpolateOnSecondEdge);
                position = firstEdge * interpolateBetweenEdges + secondEdge * (1 - interpolateBetweenEdges);
                position = transform.rotation * position;
                position += transform.position;
                break;
            case 4:
                firstEdge = vertices[0] * interpolateOnFirstEdge + vertices[1] * (1 - interpolateOnFirstEdge);
                secondEdge = vertices[4] * interpolateOnSecondEdge + vertices[5] * (1 - interpolateOnSecondEdge);
                position = firstEdge * interpolateBetweenEdges + secondEdge * (1 - interpolateBetweenEdges);
                position = transform.rotation * position;
                position += transform.position;
                break;
            case 5:
                firstEdge = vertices[2] * interpolateOnFirstEdge + vertices[3] * (1 - interpolateOnFirstEdge);
                secondEdge = vertices[6] * interpolateOnSecondEdge + vertices[7] * (1 - interpolateOnSecondEdge);
                position = firstEdge * interpolateBetweenEdges + secondEdge * (1 - interpolateBetweenEdges);
                position = transform.rotation * position;
                position += transform.position;
                break;
        }

        return position;
    }

    public static Vector3 insideCube(this Transform transform)
    {
        float interpolate = Random.Range(0.3f, 0.7f);

        Vector3 firstPoint = transform.onCube();

        Vector3 secondPoint = transform.onCube();

        return (firstPoint * interpolate + secondPoint * (1 - interpolate));
    }

    public static Vector3 RandomPointOnPlane(this Transform plane)
    {
        List<Vector3> VerticeList = new List<Vector3>(plane.GetComponent<MeshFilter>().sharedMesh.vertices);
        Vector3 leftTop = plane.TransformPoint(VerticeList[0]);
        Vector3 rightTop = plane.TransformPoint(VerticeList[10]);
        Vector3 leftBottom = plane.TransformPoint(VerticeList[110]);
        Vector3 rightBottom = plane.TransformPoint(VerticeList[120]);
        Vector3 XAxis = rightTop - leftTop;
        Vector3 ZAxis = leftBottom - leftTop;
        return leftTop + XAxis * Random.value + ZAxis * Random.value;
    }

    public static int ToInt(this bool b)
    {
        return b ? 1 : -1;
    }

    public static int ToInt01(this bool b)
    {
        return b ? 1 : 0;
    }

    public static Vector3 Abs(this Vector3 v)
    {
        return new Vector3(Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z));
    }
}

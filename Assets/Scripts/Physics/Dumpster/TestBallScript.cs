using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TestBallScript : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "CollisionTest")
        {
            collision.gameObject.TryGetComponent(out MeshRenderer rend);
            GeneralFileWriter.writeLine(rend.material.name);
        }

    }
}

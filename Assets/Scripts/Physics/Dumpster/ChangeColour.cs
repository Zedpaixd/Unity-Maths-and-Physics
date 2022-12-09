using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChangeColour : MonoBehaviour
{
    // change colour to material of what hits this
    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.TryGetComponent(out MeshRenderer rend);

        if (rend && collision.gameObject.tag == "ChangeableColour")
        {
            rend.material.color = gameObject.GetComponent<MeshRenderer>().material.color;
        }
    }
}

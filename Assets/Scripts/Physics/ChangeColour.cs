using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChangeColour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.TryGetComponent(out MeshRenderer rend);

        if (rend && collision.gameObject.tag == "ChangeableColour")
        {
            rend.material.color = gameObject.GetComponent<MeshRenderer>().material.color;
        }
    }
}

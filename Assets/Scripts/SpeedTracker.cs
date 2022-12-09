using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedTracker : MonoBehaviour
{

    float lastS;

    private void Start()
    {
        GeneralFileWriter.reset("CBallSpeed.txt");
    }
    void Update()
    {
        if (lastS != GetComponent<Rigidbody>().velocity.magnitude) 
            {   lastS = GetComponent<Rigidbody>().velocity.magnitude; 
                GeneralFileWriter.writeLine(string.Format("{0} | {1}", this.GetComponent<Rigidbody>().velocity.magnitude, Time.time), "CBallSpeed.txt");
            }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TrackCoordonates : MonoBehaviour
{

    private StreamWriter Swriter;
    public Vector3 prevCoords;
    public float StartTime = 0;

    [SerializeField] public string fileName;


    // Start is called before the first frame update
    void Start()
    {
        StartTime = Time.time;
        Swriter = new StreamWriter("C:\\Users\\Armand\\Desktop\\"+Time.time+".txt", false);
    }

    public void Writer(Vector3 line)
    {
        Swriter.Write(string.Format("{0} | {1} | {2} \n", line, this.GetComponent<Rigidbody>().velocity.magnitude, Time.time-StartTime));//line + " | " + this.GetComponent<Rigidbody>().velocity + "\n");
    }

    // Update is called once per frame
    void Update()
    {
        if (this && this.transform.position != prevCoords) { prevCoords = this.transform.position; Writer(this.transform.position); }
    }
}

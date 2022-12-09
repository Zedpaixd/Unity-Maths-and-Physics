using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCubes : MonoBehaviour
{
    public GameObject _sampleCubePrefab;
    GameObject[] _sampleCube = new GameObject[Visualiser.circleSize];
    public float maxScale;
    void Start()
    {
        for (int i = 0; i< Visualiser.circleSize; i++)
        {
            GameObject _instantiateSampleCube = (GameObject)Instantiate(_sampleCubePrefab,
                                                this.transform.position,
                                                this.transform.rotation,
                                                this.transform);
            this.transform.eulerAngles = new Vector3(0,
                                                    -((float)360/Visualiser.circleSize) * i,
                                                    0);
            _instantiateSampleCube.transform.position = Vector3.forward * Visualiser.circleSize/5;
            _sampleCube[i] = _instantiateSampleCube;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Visualiser.circleSize; i++ )
        {
            if ( _sampleCube[i] != null )
            {
                _sampleCube[i].transform.localScale = new Vector3(10, (Visualiser._samples[i] * maxScale) + 2, 10);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAlongsideNado : MonoBehaviour
{
    public GameObject[] particleArray;
    private float[] dir;
    private int i = 0;
    void Start()
    {
        dir = new float[particleArray.Length];
        for (int i = 0; i < particleArray.Length; i++)
        {
            dir[i] = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {

        foreach (GameObject particles in particleArray)
        {
            if (particles != null)
            {
                if (particles.transform.position.y <= 9.75 && particles.transform.position.y > -0.1)
                    particles.transform.Translate(new Vector3(0, Random.Range(0.3f * dir[i], 0.5f * dir[i]), 0));
                else
                {
                    dir[i] = -1 * dir[i];
                    particles.transform.Translate(new Vector3(0, 0.6f * dir[i], 0));
                }

            }
            i++;
        }
        i = 0;

        
    }
}

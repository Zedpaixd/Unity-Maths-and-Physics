using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleInitializer : MonoBehaviour
{
    public Transform floor;
    public float baseScore;
    public ScoreManager manager;
    public int obstacleCount;
    public GameObject cubePrefab, spherePrefab, capsulePrefab;

    private void Start()
    {
        //if there are no children to this transform, there are no obstacles
        if (transform.childCount == 0)
        {
            for (int i = 0; i < obstacleCount; i++)
            {
                //randomly generate obstacle
                int r = Random.Range(0, 2);
                GameObject temp = null;
                if (r == 0) //cube
                {
                    temp = Instantiate(cubePrefab, floor.RandomPointOnPlane(), Quaternion.Euler(Vector3.up * Random.Range(0, 90)), transform);
                    temp.transform.localScale = Vector3.one * Random.Range(0.5f, 0.8f);
                    temp.transform.localPosition += Vector3.up * temp.transform.localScale.y * 0.5f;
                }
                else if (r == 1)//capsule
                {
                    temp = Instantiate(capsulePrefab, floor.RandomPointOnPlane(), Quaternion.identity, transform);
                    temp.transform.localScale = Vector3.one * Random.Range(0.5f, 0.8f);
                    temp.transform.localPosition += Vector3.up * temp.transform.localScale.y * 0.5f;
                }
                //not using sphere because spheres would make the pinball fly outside the arena

                temp.GetComponent<IPinballObstacle>().Initialize(manager, baseScore);
            }
        }
        else
        {
            //obstacles exist, loop through all of them and initialize
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<IPinballObstacle>().Initialize(manager, baseScore);
            }
        }
    }
}

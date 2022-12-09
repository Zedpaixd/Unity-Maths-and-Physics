using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{

    float t;
    int direction = 1;                  //Direction of rotation the wind object makes
    public static float windForce;      //Global variable - force of the wind. 
    public float windTimeVar = 5;       //Time between updates.
    public float minWindForce = 10;     //Absolute minimum the windforce can be
    public float maxWindForce = 10;     //Absolute maximum the windforce can be
    public float dWindForce = 0.1f;     //Change in windforce.
    public float forcePickUp;           //Amount of force needed before objects react to wind
    public int directionVariance = 5;   // Range of change out of 10
    public bool dynamicMovement = false;
    public string taggedGameObjects;
    //public enum Direction {Away, Towards};
    //public Direction forceDirection;

    //Randomly pick starting values and transforms.
    void Start()
    {
        windForce = (int)Random.Range(minWindForce, maxWindForce);
        if (dynamicMovement == true)
        {
            transform.localRotation = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
        };
    }

    //Randomly adjust values.
    void Update()
    {
        StartCoroutine(rand());
        if (dynamicMovement == true)
        {
            transform.RotateAround(new Vector3(0, 0, 0), new Vector3(0, direction, 0), 20 * Time.deltaTime);
        };

        //Objects tagged will look in the direction of the wind
        if (windForce >= forcePickUp)
        {
            if (taggedGameObjects != null)
            {
                GameObject[] tagged;
                tagged = GameObject.FindGameObjectsWithTag(taggedGameObjects);
                foreach (GameObject obj in tagged)
                {
                    Vector3 relativePos = new Vector3(transform.position.x, obj.transform.position.y, transform.position.z) - obj.transform.position;
                    Quaternion rotation = Quaternion.LookRotation(relativePos);
                    obj.transform.rotation = Quaternion.Slerp(obj.transform.rotation, rotation, Time.deltaTime * 2.0f);
                }
            }
            else
            {
                UnityEngine.Debug.Log("No Tag set, you can however still use the script");
            }
        }

        UnityEngine.Debug.Log(windForce);
    }

    //Determine wind force and direction randomly. Update variables accordingly.
    IEnumerator rand()
    {
        int rand = Random.Range(1, 10);
        if (rand > directionVariance)
        {
            direction = Random.Range(-1, 2);
        }

        //Time before update
        t = Random.Range(windTimeVar, 2 * windTimeVar);

        //Change in windforce variables
        windForce += Random.Range(-dWindForce, dWindForce);
        windForce = Mathf.Clamp(windForce, minWindForce, maxWindForce);

        yield return new WaitForSeconds(t);
    }

}


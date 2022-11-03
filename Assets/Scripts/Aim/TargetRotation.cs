using UnityEngine;

public class TargetRotation : MonoBehaviour
{
    public float rotationSpeed = 20f;

    private float xSpeed, ySpeed, zSpeed;
    private int frames;
    private int updateInterval = 200;

    private void Start()
    {
        RandomizeSpeed();
    }
    private void Update()
    {
        frames++;
        Rotate();
        if (frames % updateInterval == 0)
        {
            RandomizeSpeed();
        }
    }

    void Rotate()
    {
        //Vector3 rand = new Vector3(Random.value * Random.Range(0, rotationSpeed),
        //    Random.value * Random.Range(0, rotationSpeed), 
        //    Random.value * Random.Range(0, rotationSpeed));

        //transform.Rotate(Vector3.one * rotationSpeed * Time.deltaTime);
        transform.rotation *= Quaternion.Euler(new Vector3(xSpeed,ySpeed,zSpeed) * Time.deltaTime);
    }

    void RandomizeSpeed()
    {
        xSpeed = rotationSpeed * Random.Range(-5f, 5f);
        ySpeed = rotationSpeed * Random.Range(-5f, 5f);
        zSpeed = rotationSpeed * Random.Range(-5f, 5f);
    }

}

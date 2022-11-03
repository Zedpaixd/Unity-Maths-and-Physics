using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{ 
    [Header("Shooting")]
    public GameObject projectilePrefab;
    public GameObject noRBprojectilePrefab;
    public float shootForce;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ShootB();
    }

    void ShootB()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject temp = Instantiate(projectilePrefab, transform.position + transform.forward * 2, Quaternion.identity);
            temp.GetComponent<Rigidbody>().AddForce(transform.forward * shootForce, ForceMode.Impulse);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            /*GameObject temp = Instantiate(noRBprojectilePrefab, transform.position + transform.forward * 2, Quaternion.identity);
            temp.GetComponent<ProjectileMovement>().Initialize(transform.forward * shootForce, shootForce, 20);*/
            GameObject bullet = Instantiate(noRBprojectilePrefab, transform.position, transform.rotation);
            BulletScript bulletS = bullet.GetComponent<BulletScript>();
            if (bulletS)
            {
                bulletS.Initialize(transform, 900, 9.81f);
            }
            Destroy(bullet, 50f);
        }
    }
}

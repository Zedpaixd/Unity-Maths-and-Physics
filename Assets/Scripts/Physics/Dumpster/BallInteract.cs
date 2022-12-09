using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BallInteract : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float projectileSpeed;
    public int projectiles;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Moveable")
        {
            GeneralFileWriter.reset();
            GeneralFileWriter.writeLine(string.Format("Projectiles: {0}  -  ProjectileSpeed: {1}",projectiles,projectileSpeed));
            for (int i = 0; i < projectiles;i++)
            {
                GameObject projectile = Instantiate(projectilePrefab, new Vector3(0 + (float)UnityEngine.Random.Range(-80 , 80)/10, 10 + (float)UnityEngine.Random.Range(-40, 40)/10, 41f), Quaternion.identity);
                projectile.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, 1f)*projectileSpeed, ForceMode.Impulse);
            }
            
        }
    }
}

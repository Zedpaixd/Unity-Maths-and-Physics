using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushObject : MonoBehaviour
{
    Rigidbody body;
    private void Start()
    {
        body = GetComponent<Rigidbody>();
        if (!body)
            gameObject.SetActive(false);
    }

    private void OnCollisionStay(Collision collision)
    {
        //if the player collides with this object
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collide");
            //direction of collision
            Vector3 dir = collision.gameObject.transform.position - transform.position;
            //Rigidbody body = GetComponent<Rigidbody>();
            float speed = new Vector3(collision.relativeVelocity.x, 0, collision.relativeVelocity.z).magnitude;
            body.AddForce(dir * speed * 200);
        }
    }
}

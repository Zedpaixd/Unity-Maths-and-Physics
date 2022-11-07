using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    #region Variables
    private Vector3 direction;
    private float speed;
    private Vector3 velocity;
    #endregion


    public void Initialize(Vector3 dir, float s)
    {
        direction = dir;
        speed = s;

        velocity = dir * speed;
    }

    void FixedUpdate()
    {
        velocity += Physics.gravity * Time.deltaTime;
        transform.position = transform.position + velocity * Time.deltaTime;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) { return; }
        Debug.Log("hit " + collision.transform.name);
        //add force to other rigidbody
        collision.transform.TryGetComponent(out Rigidbody body);
        if (body)
        {
            body.AddForce(direction * speed, ForceMode.Force);
        }

        //collision.gameObject.TryGetComponent(out MeshRenderer rend);

        //if (rend)
        //{
        //    rend.material.color = Random.ColorHSV();   
        //}

        //Destroy(gameObject);
    }

}

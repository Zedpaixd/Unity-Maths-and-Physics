using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovementDrag : MonoBehaviour
{
    #region Variables
    private Vector3 direction;
    private float speed;
    private float lifeTime;
    private Vector3 velocity;
    private bool inAir = true;
    [SerializeField]private float drag = 0.01f;
    private float dragLerp;
    #endregion


    public void Initialize(Vector3 dir, float s)
    {
        direction = dir;
        speed = s;

        velocity = dir * speed;
    }

    void FixedUpdate()
    {
        CheckCollisions();
        MoveProjectile();

        //lifeTime -= Time.fixedDeltaTime;
        //if (lifeTime <= 0)
        //    Destroy(gameObject);

    }

    void MoveProjectile()
    {
        if (inAir)
            velocity -= Physics.gravity.Abs() * Time.deltaTime;
        else
        {
            dragLerp = Mathf.Clamp((drag + Time.deltaTime) * (drag + Time.deltaTime), 0, 1);
            float x = Mathf.Lerp(velocity.x, 0, dragLerp);
            float z = Mathf.Lerp(velocity.z, 0, dragLerp);
            velocity = new Vector3(x, 0, z);
        }
        transform.position += velocity * Time.deltaTime;
    }

    void CheckCollisions()
    {
        inAir = !Physics.CheckSphere(transform.position, transform.localScale.x * 0.5f);
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Player")) { return; }
    //    Debug.Log("hit " + collision.transform.name);
    //    //add force to other rigidbody
    //    collision.transform.TryGetComponent(out Rigidbody body);
    //    if (body)
    //    {
    //        body.AddForce(direction * speed, ForceMode.Force);
    //    }

    //    //collision.gameObject.TryGetComponent(out MeshRenderer rend);

    //    //if (rend)
    //    //{
    //    //    rend.material.color = Random.ColorHSV();   
    //    //}

    //    //Destroy(gameObject);
    //}

}

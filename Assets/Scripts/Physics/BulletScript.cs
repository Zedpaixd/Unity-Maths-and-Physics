using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class BulletScript : MonoBehaviour
{
    private float speed;
    private float gravity;
    private Vector3 startPosition;
    private Vector3 startForward;

    private bool isInitialized = false;
    private float startTime = -1;

    public void Initialize(Transform startPoint, float speed, float gravity)
    {
        startPosition = startPoint.position;
        startForward = startPoint.forward;
        this.speed = speed;
        this.gravity = gravity;
        startTime = Time.time;
        isInitialized = true;

    }

    private Vector3 findPathPoint(float time)
    {
        Vector3 point = startPosition + (startForward * speed * time);
        Vector3 gravityVec =Vector3.down * gravity * time * time;
        return point + gravityVec;
    }

    private bool CastRay(Vector3 startPoint, Vector3 endPoint, out RaycastHit hit)
    {
        return Physics.Raycast(startPoint, endPoint - startPoint, out hit, (endPoint - startPoint).magnitude);
    }

    private void FixedUpdate()
    {
        if (!isInitialized) return;
        /*if (startTime < 0)
        {
            startTime = Time.time;
        }*/

        //RaycastHit hit;
        float currentTime = Time.time - startTime;
        float nextTime = currentTime - Time.fixedDeltaTime;

        Vector3 currentPoint = findPathPoint(currentTime);
        Vector3 nextPoint = findPathPoint(nextTime);

        /*if (CastRay(currentPoint,nextPoint, out hit))
        {
            Destroy(gameObject);
        }*/
    }

    private void Update()
    {
        if (!isInitialized || startTime < 0) return;
        float currentTime = Time.time - startTime;
        Vector3 currentPoint = findPathPoint(currentTime);
        transform.position = currentPoint;
    }
}




/*
eFORCE parameter has unit of mass * distance/ time^2, i.e. a force
eIMPULSE          parameter has unit of mass * distance /time
eVELOCITY_CHANGE  parameter has unit of distance / time, i.e. the effect is mass independent: a velocity change.
eACCELERATION     parameter has unit of distance/ time^2, i.e. an acceleration. It gets treated just like a force except the mass is not divided out before integration.

I tried to convert these formulas above to a rigidbody.velocity += syntax, but I am unable to get the Maths right.

Here is what I got working:

float force = 3f;
public void FixedUpdate()
{
    // Same as r.AddForce(force, ForceMode.Force)
    Vector2 distance = (force / r.mass) * Time.fixedDeltaTime;
    r.velocity += distance;
}
float force = 3f;
public void FixedUpdate()
{
    // Same as r.AddForce(force, ForceMode.Impulse)
    Vector2 distance = (force / r.mass);
    r.velocity += distance;
}
I tested both of them, by superposing two rigidbodies, one using AddForce, while the other used r.velocity +=.So far, it seem my convertion is correct as they move exactly the same.

As for the Maths I am not getting right, here is what I came with:

eFORCE = mass * distance / time ^ 2
eFORCE / mass = distance / time ^ 2
(eFORCE / mass) * time ^ 2 = distance
distance = (eFORCE / mass) * time ^ 2
Same as ?

Vector2 distance = (force / r.mass) * Time.fixedDeltaTime;
That woud mean time^2 is equal to Time.fixedDeltaTime.

Then let's do the same for ForceMode.Impulse:

eIMPULSE = mass * distance /time
eIMPULSE / mass = distance / time
(eIMPULSE / mass) *time = distance
Meaning time is Mathf.Sqrt(Time.fixedDeltaTime) ? Well, as you guessed, on the terrain this doesn't check out.*/
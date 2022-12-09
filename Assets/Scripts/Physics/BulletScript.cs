using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class BulletScript : MonoBehaviour
{
    [SerializeField] public GameObject hitSpot; 
    private float speed;
    private float gravity;
    private Vector3 startPosition;
    private Vector3 startForward;
    private Vector3 oldPosition;

    public bool isInitialized = false;

    private float startTime = -1;
    #region attempt
    /*
        public LayerMask layerMask; //make sure we aren't in this layer 
        public float skinWidth = 0.1f; //probably doesn't need to be changed 

        private float minimumExtent;
        private float partialExtent;
        private float sqrMinimumExtent;
        private Vector3 previousPosition;
        private Rigidbody myRigidbody;

        void Awake()
        {
            myRigidbody = GetComponent<Rigidbody>();
            previousPosition = myRigidbody.position;
            minimumExtent = Mathf.Min(Mathf.Min(GetComponent<Collider>().bounds.extents.x, GetComponent<Collider>().bounds.extents.y), GetComponent<Collider>().bounds.extents.z);
            partialExtent = minimumExtent * (1.0f - skinWidth);
            sqrMinimumExtent = minimumExtent * minimumExtent;
        }*/
    #endregion
    public void Initialize(Transform startPoint, float speed, float gravity)
    {
        this.startPosition = startPoint.position + startPoint.forward;
        this.startForward = startPoint.forward.normalized;
        this.speed = speed;
        this.gravity = gravity;
        isInitialized = true;
    }

    private Vector3 FindPointOnParabola(float time)
    {
        Vector3 point = startPosition + (startForward * time * speed);
        Vector3 gravityVec = Vector3.down * time * time * gravity;
        return point + gravityVec;
    }

    private bool CastRayBetweenPoints(Vector3 startPoint, Vector3 endPoint, out RaycastHit hit)
    {
        Debug.DrawRay(startPoint, endPoint - startPoint, Color.green, 5);
        return Physics.Raycast(startPoint, endPoint - startPoint, out hit, (endPoint - startPoint).magnitude);
    }

    private void OnHit(RaycastHit hit)
    {
        ShootableObject shootableObject = hit.transform.GetComponent<ShootableObject>();
        if (shootableObject)
        {
            shootableObject.OnHit(hit);
        }
        //Destroy(gameObject);
    }

    private void FixedUpdate()
    {

        if (!isInitialized) return;
        if (startTime < 0) startTime = Time.time;

        #region attempt
        /*Vector3 movementThisStep = myRigidbody.position - previousPosition;
        float movementSqrMagnitude = movementThisStep.sqrMagnitude;

        if (movementSqrMagnitude > sqrMinimumExtent)
        {
            float movementMagnitude = Mathf.Sqrt(movementSqrMagnitude);
            RaycastHit hitInfo;

            if (Physics.Raycast(previousPosition, movementThisStep, out hitInfo, movementMagnitude, layerMask.value))
            //myRigidbody.position = hitInfo.point - (movementThisStep / movementMagnitude) * partialExtent;
            { 
                isInitialized = false;
            }

        }

        previousPosition = myRigidbody.position;*/
        #endregion



        float currentTime = Time.time - startTime;
        float prevTime = currentTime - Time.fixedDeltaTime;
        float nextTime = currentTime + Time.fixedDeltaTime;

        RaycastHit hit;
        Vector3 currentPoint = FindPointOnParabola(currentTime);

        if (prevTime > 0)
        {
            Vector3 prevPoint = FindPointOnParabola(prevTime);
            if (CastRayBetweenPoints(prevPoint, currentPoint, out hit))
            {
                OnHit(hit);
            }
        }

        Vector3 nextPoint = FindPointOnParabola(nextTime);
        if (CastRayBetweenPoints(currentPoint, nextPoint, out hit))
        {
            OnHit(hit);
        }
    }

    void Update()
    {
        if (!isInitialized || startTime < 0) return;

        float currentTime = Time.time - startTime;
        Vector3 currentPoint = FindPointOnParabola(currentTime);
        oldPosition = transform.position;
        transform.position = currentPoint;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("??");

        if (collision.transform.tag == Globals.TARGET_TAG)
        {
            isInitialized = false;
            speed = 0;
            gravity = 0;
            Instantiate(hitSpot, oldPosition ,transform.rotation);
            Destroy(this);
        }
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
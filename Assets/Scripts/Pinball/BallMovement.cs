using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    //private float rayBounceDelay;
    private float ballSpeed;
    private int maxBounces;
    private float _rayBounceDelay;
    [SerializeField] public RayTransform startingRayTransform;
    [SerializeField] private float sphereScale;
    //audio
    public AudioSource audioSource;
    public AudioClip scoreGain, loss;
    public float SphereScale { get { return sphereScale; } set { sphereScale = value; } }

    public LayerMask layers;

    public void Initialize(float speed, Vector3 startPos, Vector3 startDir, int maxBounces, float scale, LayerMask layersToHit, out List<RayTransform> bounces, AudioSource source)
    {
        //rayBounceDelay = timeBetweenBounces;
        ballSpeed = speed;
        this.maxBounces = maxBounces;
        layers = layersToHit;
        sphereScale = scale;
        transform.localScale = Vector3.one * sphereScale;

        //_rayBounceDelay = rayBounceDelay;
        //save start ray transform for displaying bounces
        startingRayTransform = new RayTransform(startPos, startDir, 100f);

        bounces = BouncePoints(maxBounces);

        audioSource = source;

        StartCoroutine(RaycastLoop(startingRayTransform));
    }

    RayTransform ShootRay(RayTransform currentRayTransform)
    {
        //spherecast hit
        if (Physics.SphereCast(currentRayTransform.rayPosition, sphereScale, currentRayTransform.rayDirection, out RaycastHit rayHit, currentRayTransform.rayDistance, layers))
        {
            rayHit.transform.TryGetComponent(out IPinballObstacle obs);
            //movement on the ray
            StartCoroutine(MoveTrailAlongTheRay(currentRayTransform.rayPosition, rayHit.point + rayHit.normal * 0.5f * sphereScale, obs));

            //Next ray transform
            return new RayTransform(rayHit.point + rayHit.normal * 0.5f * sphereScale, /*Vector3.*/Reflect(currentRayTransform.rayDirection, rayHit.normal).normalized, currentRayTransform.rayDistance, rayHit.transform);
        }
        //spherecast didnt hit
        else
        {
            //keep moving towards the ray direction until the next raycast
            StartCoroutine(MoveTrailAlongTheRay(currentRayTransform.rayPosition, currentRayTransform.rayPosition + currentRayTransform.rayDirection * currentRayTransform.rayDistance, null));

            //Next ray transform
            return new RayTransform(currentRayTransform.rayPosition + currentRayTransform.rayDirection * currentRayTransform.rayDistance, currentRayTransform.rayDirection, currentRayTransform.rayDistance, rayHit.transform);
        }
    }

    RayTransform BounceRay(RayTransform currentRayTransform)
    {
        //spherecast hit
        if (Physics.SphereCast(currentRayTransform.rayPosition, sphereScale, currentRayTransform.rayDirection, out RaycastHit rayHit, currentRayTransform.rayDistance, layers))
        {
            //Next ray transform
            return new RayTransform(rayHit.point + rayHit.normal * 0.5f * sphereScale, /*Vector3.*/Reflect(currentRayTransform.rayDirection, rayHit.normal).normalized, currentRayTransform.rayDistance, rayHit.transform);
        }
        //spherecast didnt hit
        else
        {
            //Next ray transform
            return new RayTransform(currentRayTransform.rayPosition + currentRayTransform.rayDirection * currentRayTransform.rayDistance, currentRayTransform.rayDirection, currentRayTransform.rayDistance, rayHit.transform);
        }
    }

    IEnumerator MoveTrailAlongTheRay(Vector3 startPosition, Vector3 toPosition, IPinballObstacle obs)
    {
        float timer = 0f;

        _rayBounceDelay = /*Vector3.*/Distance(startPosition, toPosition) / ballSpeed;
        while (timer < _rayBounceDelay)
        {
            transform.position = Vector3.Lerp(startPosition, toPosition, timer / _rayBounceDelay);

            timer += Time.deltaTime;
            yield return null;
        }

        //if we hit an obstacle
        if (obs != null)
        {
            //if it's not an edge, add score
            if (!obs.isArenaEdge)
            {
                audioSource.PlayOneShot(scoreGain);
                obs.AddScore();
            }
            //if it is an edge, lose
            else
            {
                //loss logic
                //gonna need some ui stuff here
                audioSource.PlayOneShot(loss);
                Debug.Log("You hit the wall and lost");
                FindObjectOfType<TurretController>().DestroyBall();
            }
        }

        //since the lerp in the while loop might end a bit below 1, set to 1
        transform.position = Vector3.Lerp(startPosition, toPosition, 1f);

    }
    IEnumerator RaycastLoop(RayTransform rayTransform)
    {
        int bounces = 0;

        while (bounces < maxBounces || maxBounces == -1)
        {
            rayTransform = ShootRay(rayTransform);

            bounces++;
            yield return new WaitForSeconds(_rayBounceDelay);
            if (bounces == maxBounces)
            {
                FindObjectOfType<TurretController>().DestroyBall();
            }
        }
    }

    public List<RayTransform> BouncePoints(int amount, RayTransform startingRayTransform = null)
    {
        if (amount == 0)
        {
            Debug.LogError("Cannot calculate 0 bounces.");
            return null;
        }

        List<RayTransform> bounces = new List<RayTransform>();
        //first element should be the starting transform
        if (startingRayTransform == null)
            bounces.Add(this.startingRayTransform);
        else
            bounces.Add(startingRayTransform);

        for (int i = 1; i < amount; i++)
        {
            RayTransform nextBounce = BounceRay(bounces[i - 1]);
            bounces.Add(nextBounce);
            if (nextBounce.transform.CompareTag(Globals.REDWALL_TAG))
                break;
        }

        return bounces;
    }

    Vector3 Reflect(Vector3 inDirection, Vector3 inNormal)
    {
        return -2F * Dot(inNormal, inDirection) * inNormal + inDirection;
    }

    float Dot(Vector3 first, Vector3 second)
    {
        return (first.x * second.x + first.y * second.y + first.z * second.z);
    }

    float Distance(Vector3 first, Vector3 second)
    {
        Vector3 diff = first - second;
        return Mathf.Sqrt(diff.x * diff.x + diff.y * diff.y + diff.z * diff.z);
    }
    private void OnDestroy()
    {
        //    MeshFilter[] meshes = GetComponentsInChildren<MeshFilter>();
        //    for (int i = 0; i < meshes.Length; i++)
        //    {
        //        Mesh m = meshes[i].mesh;
        //        meshes[i].mesh = null;
        //        Destroy(m);
        //    }

        //    MeshRenderer[] rends = GetComponentsInChildren<MeshRenderer>();
        //    for (int i = 0; i < rends.Length; i++)
        //    {
        //        Material mat = rends[i].material;
        //        rends[i].material = null;
        //        Destroy(mat);
        //    }
        Resources.UnloadUnusedAssets();
    }
}

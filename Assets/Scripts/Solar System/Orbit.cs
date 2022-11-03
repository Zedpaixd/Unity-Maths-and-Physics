using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : ManagedUpdateBehaviour
{
    [Header("References")]
    [SerializeField] private Transform orbitAround;

    [Header("Orbit details")]
    [SerializeField] private Vector3 orbitOrientation;
    [SerializeField] private Vector3 orbitOffset;
    [SerializeField] [Range(0f, 500f)] private float orbitRadius = 1f;
    [SerializeField] [Range(1f, 3f)] private float xAxisModifier = 1f;
    [SerializeField] [Range(1f, 3f)] private float zAxisModifier = 1f;
    [SerializeField] private float orbitSpeed = 100f;
    [SerializeField] [Range(0f, 360f)] private float orbitStartAngle;

    [Header("Rotation")]
    [SerializeField] private bool rotateAroundSelf;
    [SerializeField] private float rotationSpeed = 100f;

    [Header("Gizmos")]
    [SerializeField] private bool showGizmos = true;
    [SerializeField] [Range(0, 50)] private int orbitResolution = 50;
    [SerializeField] private Color orbitColor = Color.grey;
    [SerializeField] private GameObject lineRendererPrefab;
    private GameObject lineRenderer;
    private float angle = 0;


    public void Initialize(Transform orbitAround, Vector3 orbitOrientation, 
        Vector3 orbitOffset, float orbitRadius, float xMod, float zMod, 
        float orbitSpeed, float orbitStartAngle, bool rotateAroundSelf = true, float rotationSpeed = 100f) 
    {
        this.orbitAround = orbitAround;
        this.orbitOrientation = orbitOrientation;
        this.orbitOffset = orbitOffset;

        this.orbitRadius = orbitRadius;
        this.xAxisModifier = xMod;
        this.zAxisModifier = zMod;

        this.orbitSpeed = orbitSpeed;

        this.orbitStartAngle = orbitStartAngle;

        this.rotateAroundSelf = rotateAroundSelf;
        this.rotationSpeed = rotationSpeed;

        Updater.AddToList(this);
    }

    public override void OnUpdate()
    {
        OrbitAroundTarget();
        RotateSelf();
    }

    void OrbitAroundTarget()
    {
        if (!orbitAround)
            return;

        angle += Time.deltaTime;
        //polar coords, if x and z mods are different then we'll have an elliptical orbit
        float xPos = orbitRadius * xAxisModifier * Mathf.Sin(angle * orbitSpeed + orbitStartAngle * Mathf.Deg2Rad);
        float zPos = orbitRadius * zAxisModifier * Mathf.Cos(angle * orbitSpeed + orbitStartAngle * Mathf.Deg2Rad);

        //calculate next pos on orbit from the x and z calculated above + the offset
        Vector3 nexPos = new Vector3(xPos, 0, zPos) + orbitOffset;

        //rotate nexPos according to orbit orientation and then add the target's position
        transform.position = Quaternion.Euler(orbitOrientation) * nexPos + orbitAround.position;
    }

    void RotateSelf()
    {
        if (rotateAroundSelf)
        {
            //up axis might have changed (normally should be the Y axis, vector3.up)
            //rotate the up axis by the orientation
            Vector3 upAxis = Quaternion.Euler(orbitOrientation) * Vector3.up;

            //rotate this object in world space coords
            transform.Rotate(upAxis, Time.deltaTime * rotationSpeed, Space.World);
        }
    }

    void OnDrawGizmos()
    {
        if (showGizmos == false || orbitAround == null)
            return;

        Gizmos.color = orbitColor;
        Gizmos.matrix = Matrix4x4.TRS(orbitAround.position + (Quaternion.Euler(orbitOrientation) * orbitOffset), Quaternion.Euler(orbitOrientation), new Vector3(1f, 1f, 1f));

        Gizmos.DrawLine(Vector3.down * orbitRadius, Vector3.up * orbitRadius);

        float x;
        float z;
        float previousX = 0;
        float previousZ = 0;

        int segments = orbitResolution;
        float angle = 0f;

        for (int i = 0; i < segments + 1; i++)
        {
            x = xAxisModifier * Mathf.Sin(Mathf.Deg2Rad * angle) * orbitRadius;
            z = zAxisModifier * Mathf.Cos(Mathf.Deg2Rad * angle) * orbitRadius;

            if (i != 0)
                Gizmos.DrawLine(new Vector3(previousX, 0f, previousZ), new Vector3(x, 0f, z));

            previousX = x;
            previousZ = z;

            angle += (360f / segments);
        }
    }

    private void OnDestroy()
    {
        Updater.RemoveFromList(this);
        if (lineRenderer)
            Destroy(lineRenderer.gameObject);

        Resources.UnloadUnusedAssets();
    }
}

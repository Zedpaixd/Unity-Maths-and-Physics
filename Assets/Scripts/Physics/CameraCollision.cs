using UnityEngine;

/// <summary>
/// Usage:
///     CameraBase gameobject (with camera movement script)
///         Camera gameobject (with this script)
/// </summary>
public class CameraCollision : MonoBehaviour
{
    [Header("Zooming")]
    public float maxZoomOut = 20f;
    public float maxZoomIn = 1f;
    [Range(0.01f, 20f)]
    public float scrollSensitivity = 5f;

    [Header("Prevent clipping")]
    public LayerMask obstacleLayer;
    public float minDistance = 1f;
    public float maxDistance = 20f;

    private float distance;
    private Vector3 dollyDir;
    private Vector3 dollyDirAdjusted;

    private void Start()
    {
        dollyDir = (transform.localPosition + 1.5f * Vector3.down).normalized;
        distance = transform.localPosition.magnitude;

        if (minDistance > maxZoomIn)
            maxZoomIn = minDistance;
        else
            minDistance = maxZoomIn;
    }

    private void Update()
    {
        HandleCollision();
        Zoom();
    }


    private void HandleCollision()
    {
        Vector3 desiredCameraPosition = transform.parent.TransformPoint(dollyDir * maxDistance) + 1.5f * Vector3.down;

        if (Physics.Linecast(transform.parent.position, desiredCameraPosition, out RaycastHit hit, obstacleLayer))
        {
            distance = Mathf.Clamp(hit.distance, minDistance, maxDistance);
        }
        else
        {
            distance = maxDistance;
        }

        Vector3 velocity = Vector3.zero;
        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, dollyDir * distance, ref velocity, 0.001f);
    }

    private void Zoom() 
    {
        float currentDistance = maxDistance;
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (currentDistance <= maxZoomOut && currentDistance >= maxZoomIn)
        {
            currentDistance -= scroll * scrollSensitivity;
        }

        currentDistance = Mathf.Clamp(currentDistance, maxZoomIn, maxZoomOut);
        maxDistance = currentDistance;
    }

}

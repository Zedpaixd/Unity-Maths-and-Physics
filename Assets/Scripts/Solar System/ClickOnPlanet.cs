using System;
using UnityEngine;

public class ClickOnPlanet : MonoBehaviour
{
    public PreviewCamera previewCam;
    public PreviewPanel previewPanel;
    public Vector3 previewCameraOffset;
    public LayerMask planetLayer;

    private Vector3 startPosition;
    private Quaternion startRotation;

    private bool isPreviewActive;
    private bool isOnPlanet;
    private Vector3 positionOnPlanet;
    private Transform planetTarget;

    [SerializeField] private float rotationSpeed;
    private Vector3 rotationAxis;
    private float rotationX;
    private float rotationY;

    private void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;

        Updater.onUpdaterReset.AddListener(() => 
        {
            previewCam.gameObject.SetActive(false);
            previewPanel.gameObject.SetActive(false);
            ReturnToStart();
        });
    }

    private void Update()
    {
        ClickToInspect();
        OnPlanet();
        Return();
    }

    void ClickToInspect()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, planetLayer))
            {
                //initialize cam pos
                previewCam.Initialize(hit.transform, hit.normal + previewCameraOffset * hit.transform.localScale.x);
                previewCam.gameObject.SetActive(true);

                planetTarget = hit.transform;
                positionOnPlanet = hit.transform.InverseTransformPoint(hit.point + hit.normal * 0.75f);
                //set preview panel stuff
                Action visitAction = () =>
                {
                    isOnPlanet = true;
                };
                Action closeAction = () =>
                {
                    previewPanel.gameObject.SetActive(false);
                    previewCam.gameObject.SetActive(false);
                    ReturnToStart();
                };
                previewPanel.Set(hit.transform.name, visitAction, closeAction);
                previewPanel.gameObject.SetActive(true);
                isPreviewActive = true;

                rotationAxis = hit.transform.up;
            }

        }
    }

    void Return()
    {
        if (Input.GetButtonDown(Globals.CANCEL_BUTTON) && (isPreviewActive || isOnPlanet))
        {
            ReturnToStart();
        }
    }


    void ReturnToStart() 
    {
        previewCam.gameObject.SetActive(false);
        previewPanel.gameObject.SetActive(false);
        isPreviewActive = false;
        isOnPlanet = false;

        transform.position = startPosition;
        transform.rotation = startRotation;
    }

    void OnPlanet()
    {
        if (!isOnPlanet)
            return;

        transform.position = planetTarget.TransformPoint(positionOnPlanet);

        if (Input.GetMouseButton(1))
        {
            float mouseY = Input.GetAxis(Globals.MOUSE_Y_AXIS);
            float mouseX = Input.GetAxis(Globals.MOUSE_X_AXIS);
            rotationX += mouseX * rotationSpeed * Time.deltaTime;
            rotationY += mouseY * rotationSpeed * Time.deltaTime;

            transform.localRotation = Quaternion.Euler(rotationY, rotationX, 0);
        }
        //transform.rotation = planetTarget.rotation;
    }
}

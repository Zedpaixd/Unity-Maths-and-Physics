using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;
public class TurretController : MonoBehaviour
{
    [Header("Turret options")]
    public float rotationSpeed;
    public Transform spawnPoint;
    [Space]
    [Header("Pinball options")]
    public float sphereScale;
    [Range(-1, 100), Tooltip("Max amount of bounces for the ball. Set to -1 for unlimited.")]
    public int maxBounces;
    [Range(10, 500), Tooltip("Ball speed. Values below 100 might be too slow.")]
    public float ballSpeed = 100;
    public LayerMask layersToHit;
    public GameObject ballPrefab;

    //reference to the ball
    private GameObject ball;
    private BallMovement ballMovement;
    private bool ballExists = false;

    //bounces
    private List<RayTransform> bounces;

    //bounces to show the player
    [Header("Display bounces")]
    public int amountToDisplay;
    public LineRenderer line;
    [SerializeField]private List<GameObject> bouncesToDisplay = new List<GameObject>();
    private List<RayTransform> bounceRayTransforms = new List<RayTransform>();
    private Quaternion previousRotation;

    //used to show the bounces
    private BallMovement displayBallMovement;
    private RayTransform startRayTransform;

    //reference to rotation y
    private float rotationY;

    [Header("Audio")]
    public AudioSource sfxAudioSource;
    public AudioSource bgMusicAudioSource;
    public AudioClip bgMusic;

    [Header("Adjust sphere scale")]
    public Slider slider;
    public TextMeshProUGUI scaleAdjustText;

    private void Start()
    {
        rotationY = transform.localRotation.eulerAngles.y;
        //previousRotation = transform.localRotation;
        displayBallMovement = GetComponent<BallMovement>();
        displayBallMovement.SphereScale = sphereScale;


        sfxAudioSource = gameObject.AddComponent<AudioSource>();
        //lower volume to something lower
        sfxAudioSource.volume = 0.35f;

        //bg music
        bgMusicAudioSource = gameObject.AddComponent<AudioSource>();
        bgMusicAudioSource.clip = bgMusic;
        bgMusicAudioSource.loop = true;
        bgMusicAudioSource.Play();

        //initialize display list
        for (int i = 0; i < amountToDisplay; i++)
        {
            GameObject temp = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            temp.transform.localScale = sphereScale * Vector3.one;
            bouncesToDisplay.Add(temp);
            temp.SetActive(false);
        }

        slider.onValueChanged.AddListener((float value) =>
        {
            sphereScale = value;
            scaleAdjustText.text = string.Format("Sphere size: {0}", value.ToString("f2").Replace(",","."));
            displayBallMovement.SphereScale = value;
        });
    }

    void Update()
    {
        if (!ballExists)
        {
            Rotate();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                SpawnBall();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                DestroyBall();
            }
        }
    }


    private void LateUpdate()
    {
        DisplayBounces();
    }


    void DisplayBounces()
    {
        //only update when the turret is rotated
        if (transform.localRotation == previousRotation)
            return;

        //update previous rotation
        previousRotation = transform.localRotation;
        //clear bounce ray transform list
        bounceRayTransforms.Clear();

        //disable all display spheres
        for (int i = 0; i < bouncesToDisplay.Count; i++)
        {
            bouncesToDisplay[i].SetActive(false);
        }

        //calculate first ray transform
        startRayTransform = new RayTransform(transform.position, spawnPoint.transform.forward, 100f);
        //assign to display
        displayBallMovement.startingRayTransform = startRayTransform;
        //calculate the bounces
        bounceRayTransforms = displayBallMovement.BouncePoints(amountToDisplay + 1);
        //keep only amount + 1 points (+1 because the first point is always the start point)
        bounceRayTransforms = bounceRayTransforms.Take(amountToDisplay + 1).ToList();
        //set positions for line renderer
        line.positionCount = bounceRayTransforms.Count;
        //the rest of the loop ignores 0 so just assign it here
        line.SetPosition(0, startRayTransform.rayPosition);
        //loop all positions and instantiate a sphere at the hit position, also add position to line renderer
        for (int i = 1; i < bounceRayTransforms.Count; i++)
        {
            if (i > bouncesToDisplay.Count)
                break;

            GameObject currentSphere = bouncesToDisplay[i - 1];
            currentSphere.transform.position = bounceRayTransforms[i].rayPosition;
            currentSphere.SetActive(true);
            currentSphere.transform.localScale = Vector3.one * sphereScale;
            //set line vertices
            line.SetPosition(i, bounceRayTransforms[i].rayPosition);
        }

    }

    void Rotate()
    {
        //use horizontal axis to rotate
        float horz = Input.GetAxis(Globals.HORIZONTAL_AXIS);
        rotationY -= horz * rotationSpeed * Time.deltaTime;
        //clamp betwen -90 and 90
        rotationY = Mathf.Clamp(rotationY, -90, 90);
        transform.localRotation = Quaternion.Euler(Vector3.up * rotationY);
    }

    void SpawnBall()
    {
        ballExists = true;

        Vector3 ballSpawnPos = transform.position;

        ball = Instantiate(ballPrefab, ballSpawnPos, Quaternion.identity);

        //initialize
        ballMovement = ball.GetComponent<BallMovement>();
        bounces = new List<RayTransform>();
        ballMovement.Initialize(ballSpeed, startRayTransform.rayPosition, startRayTransform.rayDirection, 
            maxBounces, sphereScale, layersToHit, out bounces, sfxAudioSource);

    }
    public void DestroyBall()
    {
        Destroy(ball);
        ballExists = false;
        FindObjectOfType<ScoreManager>().EndRound();
        Debug.Log("Round ended because either a wall is hit or max bounces were reached");
    }

    private void OnDrawGizmos()
    {
        if (bounces != null && maxBounces != -1)
            //if max bounces are -1 dont display all of them
            for (int i = 1; i < bounces.Count; i++)
            {
                //skip 0 because it's the starting point
                Gizmos.DrawSphere(bounces[i].rayPosition, sphereScale);
                Gizmos.DrawLine(bounces[i - 1].rayPosition, bounces[i].rayPosition);
            }
    }

}

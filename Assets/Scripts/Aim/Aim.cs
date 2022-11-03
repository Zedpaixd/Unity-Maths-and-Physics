using UnityEngine;

public class Aim : MonoBehaviour
{
    private Camera _camera;
    public GameObject penaltyPrefab;
    public AudioClip gain, loss;
    [Header("Score stuff")]
    public float add;
    public float subtract;
    [SerializeField]
    private ScoreSystem scoreSystem;

    private Transform target;

    private void Start()
    {
        _camera = Camera.main;

        if (!scoreSystem)
        {
            scoreSystem = FindObjectOfType<ScoreSystem>();
            Debug.Log("Found and assigned score system.");
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AimFromMousePosition();
        }
    }

    void AimFromMousePosition()
    {
        //create a ray in the current mouse position
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        //cast ray 
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            if (hitInfo.transform.CompareTag(Globals.TARGET_TAG))
            {
                if (target == null)
                    target = hitInfo.transform;
                //checks if there are other penalty objects 
                //by using a cube of size (1,1,1)
                //if (Physics.CheckBox(hitInfo.point, Vector3.one * 0.5f, Quaternion.identity, penaltyLayer))
                //{
                //    Debug.Log($"Hit another penalty!");
                //    return;
                //}

                Debug.Log($"Hit the target! Gained {add} points!");
                scoreSystem.AddScore(add);
                AudioManager.Instance.PlayEffect(gain);

                //spawn the penalty prefab, at hitInfo.point + hitInfo.normal * 0.1f, without rotation
                GameObject temp = Instantiate(penaltyPrefab, hitInfo.point + hitInfo.normal * 0.1f, Quaternion.identity);
                //no parenting
                //temp.transform.parent = hitInfo.transform;
                //initialize penalty component
                temp.GetComponent<Penalty>().Initialize(hitInfo.point + hitInfo.normal * 0.1f, hitInfo.transform);
            }
            else if (hitInfo.transform.CompareTag(Globals.PENALTY_TAG))
            {
                //hit penalty object
                Debug.Log($"Hit the penalty! Lost {subtract} points!");
                scoreSystem.SubtractScore(subtract);
                AudioManager.Instance.PlayEffect(loss);

                GameObject temp = Instantiate(penaltyPrefab, hitInfo.point + hitInfo.normal * 0.1f, Quaternion.identity);
                if (target != null)
                    temp.GetComponent<Penalty>().Initialize(target.onCube(), hitInfo.transform);
            }
        }
    }
}

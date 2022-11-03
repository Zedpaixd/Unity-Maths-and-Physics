using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public Transform cam;
    public Rigidbody body;
    //public Animator anim;

    [Header("Movement")]
    public float walkSpeed;
    public float runSpeed;
    private float currentSpeed;
    private bool isRunning;
    public bool canMove;
    public bool isMoving;

    [Header("Jumping")]
    public LayerMask groundLayer;
    public float walkJumpHeight, runJumpHeight;
    private float currentJumpHeight;
    public bool isHeadColliding;
    public float gravity;

    public Vector3 velocity;

    //initialization
    private void OnEnable()
    {
        cam = Camera.main.transform;
        body = GetComponent<Rigidbody>();
        // anim = GetComponentInChildren<Animator>();
    }


    private void Update()
    {
        Movement();
        Jump();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //tags
        if (hit.gameObject.CompareTag("Moveable"))
        {
            Vector3 dir = hit.point - transform.position;

            Rigidbody otherBody = hit.gameObject.GetComponent<Rigidbody>();
            if (otherBody == null)
            {
                //no rigibody attached, move transform
                hit.transform.position = Vector3.MoveTowards(hit.transform.position, hit.transform.position + dir, Time.deltaTime);
            }
            else
            {
                //rigibody exists apply force
                otherBody.AddForce(dir);
            }
        }

        //layers
        //layermask.compareto returns 0 if it hits the layers you want
        if (hit.gameObject.layer.CompareTo(LayerMask.NameToLayer("Cube")) == 0)
        {
            Debug.Log("Layer hit");
        }
    }

    private void Movement()
    {
        //get user input
        //a,d and left,right
        float x = Input.GetAxis("Horizontal");
        //w,s and up,down
        float z = Input.GetAxis("Vertical");

        Vector3 inputDirection = new Vector3(x, 0, z).normalized;

        //anim.SetFloat("Speed", currentSpeed * inputDirection.magnitude);
        //anim.SetFloat("SpeedMult",currentSpeed * 2f);
        if (inputDirection.magnitude >= 0.1f)
        {
            isMoving = true;
            float targetAngle = cam.eulerAngles.y + Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;

            if (canMove)
                transform.localRotation = Quaternion.Euler(0, targetAngle, 0);

            Vector3 moveDirection = transform.localRotation * Vector3.forward;
            //running code
            if (Input.GetKey(KeyCode.LeftShift))
                currentSpeed = runSpeed;
            else
                currentSpeed = walkSpeed;

            if (!canMove)
                currentSpeed = 0;

            velocity = new Vector3((moveDirection.normalized * currentSpeed).x , body.velocity.y, (moveDirection.normalized * currentSpeed).z);
            body.velocity = velocity;
        }
        else
        {
            velocity = Vector3.zero;
            isMoving = false;
        }

    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                currentJumpHeight = runJumpHeight;
            }
            else 
            {
                currentJumpHeight = walkJumpHeight;
            }

            velocity += Vector3.up * Mathf.Sqrt(-2 * Physics.gravity.y * currentJumpHeight);
            body.velocity = velocity;
            //body.AddForce(Vector3.up * Mathf.Sqrt(-2 * Physics.gravity.y * currentJumpHeight), ForceMode.Impulse);
        }

    }

    //public void JumpAnimation()
    //{
    //    anim.SetBool("Jump", false);
    //}   

}

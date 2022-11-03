using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform cam;

    [Header("Mouse")]
    public float mouseSensitivity;

    [Header("Movement")]
    public float walkSpeed;
    public float runSpeed;
    private float currentSpeed;

    public float movementDeadzone = 0.1f;
    private float sqrMovementDeadzone = 0;

    [Header("Rotation")]
    public float maxAngle;
    private float rotationX;

    [Header("Jumping")]
    public float gravity = 9.81f;
    public float walkJumpHeight = 1.5f;
    public float runJumpHeight = 2.5f;
    private float currentJumpHeight = 0f;
    [SerializeField] private float verticalVelocity = 0f;

    [Header("Pushing")]
    public float pushForce = 20f;

    [Header("Shooting")]
    public GameObject projectilePrefab;
    public GameObject noRBprojectilePrefab;
    public float shootForce;

    public StreamWriter Swriter;
    public StreamWriter SwriterRecreated;

    public GameObject testBall;
    public Vector3 prevCoords;
    public GameObject testBallRecreated;
    public Vector3 prevCoordsRecreated;

    public enum InvertCamera
    {
        inverted = -1,
        non_inverted = 1
    }

    public InvertCamera invertCameraStatus;

    private void Start()
    {
        sqrMovementDeadzone = movementDeadzone * movementDeadzone;
        //ensure gravity is positive
        gravity = Mathf.Abs(gravity);

        if (!cam)
            cam = Camera.main.transform;

        if (!controller)
            controller = GetComponent<CharacterController>();

        Swriter = new StreamWriter("C:\\Users\\Armand\\Desktop\\Ball.txt", false);
        SwriterRecreated = new StreamWriter("C:\\Users\\Armand\\Desktop\\BallRecreated.txt", false);
    }

    private void Update()
    {
        Movement();
        
        Jump();
        Shoot();


        if (testBall && testBall.transform.position != prevCoords) { prevCoords = testBall.transform.position; Writer(testBall.transform.position); }
        if (testBallRecreated && testBallRecreated.transform.position != prevCoordsRecreated) { prevCoordsRecreated = testBallRecreated.transform.position; WriterRecreated(testBallRecreated.transform.position); }
    }

    private void LateUpdate()
    {
        Rotation();
    }

    void Movement()
    {
        //get input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(x, 0, z);

        //apply movement to controller
        if (direction.sqrMagnitude > sqrMovementDeadzone)
        {
            //Quatiernion.AngleAxis creates a rotation of degrees around axis
            //Quaternion * Vector3 = rotate vector3 by quaternion
            direction = Quaternion.AngleAxis(transform.rotation.eulerAngles.y, Vector3.up) * direction;

            if (Input.GetKey(KeyCode.LeftShift))
                currentSpeed = runSpeed;
            else
                currentSpeed = walkSpeed;

            controller.Move(currentSpeed * direction.normalized * Time.deltaTime);
        }
    }

    void Rotation()
    {
        //get input from mouse
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        //add to rotation
        rotationX += (int)invertCameraStatus * mouseY;
        //clamp camera rotation
        rotationX = Mathf.Clamp(rotationX, -maxAngle, maxAngle);
        //apply rotations
        transform.Rotate(Vector3.up, mouseSensitivity * mouseX * Time.deltaTime);
        cam.transform.localRotation = Quaternion.AngleAxis(rotationX, Vector3.right);//Quaternion.Euler(new Vector3(rotationX, 0, 0));
    }

    void Jump()
    {
        if (!controller.isGrounded)
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }
        else //controller.isGrounded = true
        {
            verticalVelocity = 0f;

            if (Input.GetKey(KeyCode.Space))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                    currentJumpHeight = runJumpHeight;
                else
                    currentJumpHeight = walkJumpHeight;

                verticalVelocity = Mathf.Sqrt(2 * gravity * currentJumpHeight);
            }
        }

        //check for head collision
        if (controller.collisionFlags == CollisionFlags.Above)
        {
            verticalVelocity = -gravity * 0.2f;
        }

        controller.Move(Vector3.up * verticalVelocity * Time.deltaTime);
    }

    void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (testBall != null)
            {
                GameObject temp = Instantiate(projectilePrefab, transform.position + cam.transform.forward * 2, Quaternion.identity);
                temp.GetComponent<Rigidbody>().AddForce(cam.transform.forward * shootForce, ForceMode.Impulse);
                Destroy(temp, 20f);
            }
            else
            {
                testBall = Instantiate(projectilePrefab, transform.position + cam.transform.forward * 2, Quaternion.identity);
                testBall.GetComponent<Rigidbody>().AddForce(cam.transform.forward * shootForce, ForceMode.Impulse);
                Destroy(testBall, 20f);
            }
            
        }
        else if (Input.GetMouseButtonDown(1))
        {
            if (testBallRecreated != null)
            {
                GameObject bullet = Instantiate(noRBprojectilePrefab, transform.position + cam.transform.forward * 2, Quaternion.identity);
                var bulletS = bullet.GetComponent<ProjectileMovement>();
                if (bulletS)
                {
                    bulletS.Initialize(cam.transform.forward, shootForce);
                }
                Destroy(bullet, 20f);
            }
            else
            {
                /*testBallRecreated = Instantiate(noRBprojectilePrefab, transform.position + cam.transform.forward * 2, Quaternion.identity);
                BulletScript bulletS = testBallRecreated.GetComponent<BulletScript>();
                if (bulletS)
                {
                    bulletS.Initialize(cam.transform, shootForce, gravity);
                }*/
                testBallRecreated = Instantiate(noRBprojectilePrefab, transform.position + cam.transform.forward * 2, Quaternion.identity);
                var bulletS = testBallRecreated.GetComponent<ProjectileMovement>();
                if (bulletS)
                {
                    bulletS.Initialize(cam.transform.forward, shootForce);
                }
                Destroy(testBallRecreated, 20f);
            }
        }
    }

    public void Writer(Vector3 line)
    {
        Swriter.Write(line+"\n");
    }
    public void WriterRecreated(Vector3 line)
    {
        SwriterRecreated.Write(line + "\n");
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //tags
        if (hit.gameObject.CompareTag("Moveable"))
        {
            // Vector3 dir = (hit.point - transform.position) * pushForce;
            Vector3 dir = (hit.transform.position - controller.ClosestPoint(hit.transform.position)) * pushForce;

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
}

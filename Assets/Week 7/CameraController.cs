using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed = 5;
    public float center_repulsion_start = 1;
    public float center_repulsion_strength = 1f;

    public bool cap_to_60_fps = false;
    //public SimpleCameraControl inputAct;

    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        //inputAct = new SimpleCameraControl();
        //inputAct.Camera.Enable();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        //Vector3 inputVector = inputAct.Camera.Move.ReadValue<Vector3>();
    }
}

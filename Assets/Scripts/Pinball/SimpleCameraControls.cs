using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCameraControls : MonoBehaviour
{
    public float speed = 5f;
    public Transform target;
    // Update is called once per frame
    void Update()
    {
        //use right click to rotate
        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis(Globals.MOUSE_X_AXIS);
            transform.Translate(Vector3.right * mouseX * speed * Time.deltaTime);
        }
        transform.LookAt(target);
    }
}

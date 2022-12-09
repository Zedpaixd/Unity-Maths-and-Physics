using System;
using System.Collections.Generic;
using UnityEngine;

public class TornadoScript : MonoBehaviour
{

    Rigidbody ObjRB;

    #region tornadoVariables

    [SerializeField] private Vector3 rotationVector = new Vector3(0, 1, 0);
    [SerializeField] public float rotationPower = 50;
    [Range(1, 90)][SerializeField] public float Ystrength = 45;
    [SerializeField] public float tornadoPower = 2;
    [SerializeField] public bool displayTornadoRange = true;
    [SerializeField] public float tornadoDistance = 20;
    [HideInInspector] public int inwards = 1;

    [HideInInspector]
    public float RotationPower
    {
        get { return rotationPower; }
        set { rotationPower = value; }
    }

    [HideInInspector]
    public Vector3 Rotation
    {
        get { return rotationVector; }
        set { rotationVector = value; }
    }

    #endregion

    List<RotateWithTornado> inTornado = new List<RotateWithTornado>();

    void Start()
    {
        rotationVector.Normalize();
        ObjRB = GetComponent<Rigidbody>();
        ObjRB.isKinematic = true;
    }

    void FixedUpdate()
    {
        for (int i = 0; i < inTornado.Count; i++)
        {
            if (inTornado[i] == null) continue;

            Vector3 toPull = transform.position - inTornado[i].transform.position;
            if (toPull.magnitude > tornadoDistance)
                inTornado[i].objectRigidBody.AddForce((toPull.magnitude * toPull.normalized) * inwards, ForceMode.Force);

            inTornado[i].enabled = toPull.magnitude > tornadoDistance ? false : true;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        //try
        //{
        if (col.GetComponent<Rigidbody>() != null)
            if (!col.attachedRigidbody.isKinematic && col.attachedRigidbody)
            {
                RotateWithTornado _object = col.GetComponent<RotateWithTornado>();

                if (_object == null) _object = col.gameObject.AddComponent<RotateWithTornado>();
                _object.Initialize(this, ObjRB, tornadoPower);

                if (inTornado.Contains(_object)) Debug.Log("in");   //sanity check
                else inTornado.Add(_object);
            }
        //}
        //catch (Exception e)
        //{
        //    Debug.Log(e);
        //}

    }

    void OnTriggerExit(Collider col)
    {
        RotateWithTornado _object = col.GetComponent<RotateWithTornado>();

        if (!_object) return;
        else _object.EscapeTornado();

        if (inTornado.Contains(_object) == false) return;
        else inTornado.Remove(_object);


    }

    void OnDrawGizmosSelected()
    {
        if (!displayTornadoRange) return;

        Vector3[] positions = new Vector3[55]; // amt of points in the circle; this should be enough probably

        for (int pointNum = 0; pointNum < positions.Length; pointNum++)
        {
            float angle = (float)(pointNum * 2) / positions.Length * Mathf.PI * 2;

            Vector3 pos = new Vector3(
                Mathf.Sin(angle) * tornadoDistance,
                0,
                Mathf.Cos(angle) * tornadoDistance) + transform.position;

            positions[pointNum] = pos;
        }

        Gizmos.color = Color.magenta;
        for (int i = 0; i < positions.Length - 1; i++)
        {
            Gizmos.DrawLine(positions[i], positions[i + 1]);
        }
        Gizmos.DrawLine(positions[0], positions[positions.Length - 1]);
    }
}
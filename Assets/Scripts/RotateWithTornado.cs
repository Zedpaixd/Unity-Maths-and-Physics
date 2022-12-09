using Newtonsoft.Json.Linq;
using UnityEngine;

public class RotateWithTornado : MonoBehaviour
{
    #region variables
    private TornadoScript TS;
    private SpringJoint joint;
    [HideInInspector]
    public Rigidbody objectRigidBody;
    private int inv = -1;
    #endregion

    void Start()
    {
        objectRigidBody = GetComponent<Rigidbody>();
    }
    void Update()
    {
        setAnchor();
    }

    void FixedUpdate()   // should this be in Update?
    {
        Vector3 newDir = (TS.transform.position - transform.position) * inv; // rotate around
        Vector3 toProject = PlaneCoords(newDir, TS.Rotation);

        float magnitude = Vector3.Magnitude(toProject);   // normalising to avoid weird cases
        if (magnitude > 0.00001f)
            toProject /= magnitude;

        Vector3 _objnorm = Quaternion.AngleAxis(130, TS.Rotation) * toProject;
        _objnorm = Quaternion.AngleAxis(TS.Ystrength, toProject) * _objnorm;
        objectRigidBody.AddForce(TS.RotationPower * _objnorm, ForceMode.Force);

        Debug.DrawRay(transform.position, _objnorm * 10, Color.cyan);
    }

    private Vector3 PlaneCoords(Vector3 init, Vector3 plane)
    {
        float sqrMag = Vector3.Dot(plane, plane);
        if (sqrMag < Mathf.Epsilon) return init;
        return new Vector3(
            init.x - plane.x * Vector3.Dot(init, plane) / sqrMag,
            init.y - plane.y * Vector3.Dot(init, plane) / sqrMag,
            init.z - plane.z * Vector3.Dot(init, plane) / sqrMag
            );
    }

    public void configureSpring(SpringJoint joint, float _Force, Rigidbody _Rigidbody)
    {
        joint.spring = _Force;
        joint.connectedBody = _Rigidbody;
        joint.autoConfigureConnectedAnchor = false;
    }

    public void Initialize(TornadoScript _Script, Rigidbody _RigidBody, float _Force)
    {
        enabled = true;
        TS = _Script;

        joint = gameObject.AddComponent<SpringJoint>();

        #region configuration
        /*joint.spring = _Force;
        joint.connectedBody = _RigidBody;
        joint.autoConfigureConnectedAnchor = false;*/
        #endregion

        configureSpring(joint, _Force, _RigidBody);

        Vector3 startPos = new Vector3(0, 0, 0);
        startPos.y = transform.position.y;
        joint.connectedAnchor = startPos;
    }

    void setAnchor()
    {
        Vector3 Pos = joint.connectedAnchor;
        Pos.y = transform.position.y;
        joint.connectedAnchor = Pos;
    }

    public void EscapeTornado()
    {
        enabled = false;
        Destroy(joint);
    }
}
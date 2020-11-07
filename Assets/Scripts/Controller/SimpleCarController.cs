using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCarController : MonoBehaviour
{
    protected float m_horizontalInput;
    protected float m_verticalInput;
    protected float m_steeringAngle;

    public WheelCollider frontDriverW, frontPassengerW;
    public WheelCollider rearDriverW, rearPassengerW;
    public Transform frontDriverT, frontPassengerT;
    public Transform rearDriverT, rearPassengerT;
    public float maxSteerAngle = 30;
    public float motorForce = 50;

    [SerializeField] protected Transform laneNode;

    protected virtual void Start()
    {
    }

	protected virtual void Steer()
	{
        if (laneNode)
        {
            var lanePos = laneNode.position;
            var relativeVector = transform.InverseTransformPoint(transform.position.x + 3.0f, lanePos.y, lanePos.z);
            m_horizontalInput = (relativeVector.x / relativeVector.magnitude);
        }
        m_steeringAngle = maxSteerAngle * m_horizontalInput;
		frontDriverW.steerAngle = m_steeringAngle;
		frontPassengerW.steerAngle = m_steeringAngle;
	}

	private void Accelerate()
	{
        frontDriverW.motorTorque = m_verticalInput * motorForce;
        frontPassengerW.motorTorque = m_verticalInput * motorForce;
	}

	private void UpdateWheelPoses()
	{
		UpdateWheelPose(frontDriverW, frontDriverT);
		UpdateWheelPose(frontPassengerW, frontPassengerT);
		UpdateWheelPose(rearDriverW, rearDriverT);
		UpdateWheelPose(rearPassengerW, rearPassengerT);
	}

	private void UpdateWheelPose(WheelCollider _collider, Transform _transform)
	{
		Vector3 _pos = _transform.position;
		Quaternion _quat = _transform.rotation;

		_collider.GetWorldPose(out _pos, out _quat);

		_transform.position = _pos;
		_transform.rotation = _quat;
	}

    public void SetLane(Transform t)
    {
        laneNode = t;
    }

	private void FixedUpdate()
	{
        //GetInput();
        Steer();
        Accelerate();
		UpdateWheelPoses();
	}
}

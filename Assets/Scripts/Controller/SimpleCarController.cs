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

    protected readonly Vector2 RANGE_EULER_Y = new Vector2(-60.0f, 60.0f);

    [SerializeField]
    protected PropertyMap curPropMap;
    [SerializeField]
    protected Transform curLane;

    protected virtual void Start()
    {
    }

    protected virtual void Update()
    {
        if (curPropMap != null && transform.position.x >= curPropMap.farLimitPntT.position.x)
        {
            var nextPropMap = MapMgr.Instance.GetNextProperty(curPropMap);
            SetCurPropertyMap(nextPropMap);
        }
    }

	protected virtual void Steer()
	{
        if (curLane)
        {
            var lanePos = curLane.position;
            var relativeVector = transform.InverseTransformPoint(transform.position.x + 3.0f, lanePos.y, lanePos.z);
            m_horizontalInput = (relativeVector.x / relativeVector.magnitude);
        }
        m_steeringAngle = maxSteerAngle * m_horizontalInput;
		frontDriverW.steerAngle = m_steeringAngle;
		frontPassengerW.steerAngle = m_steeringAngle;
	}

	protected virtual void Accelerate()
	{
        rearDriverW.motorTorque = m_verticalInput * motorForce;
        rearPassengerW.motorTorque = m_verticalInput * motorForce;
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

    public virtual void SetCurPropertyMap(PropertyMap propMap)
    {
        curPropMap = propMap;
        // set default lane = mid lane
        curLane = curPropMap.GetLanePosition(PropertyMap.LaneId.middle);
    }

    private void FixedUpdate()
	{
        //GetInput();
        Steer();
        Accelerate();
        UpdateWheelPoses();
    }
}

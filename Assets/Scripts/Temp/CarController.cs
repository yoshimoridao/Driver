using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CarController : MonoBehaviour
{
    public static CarController Instance;
    public static Action<bool> actOnControlWheel;
    public static Action<bool> actOnControlGas;
    public static Action actOnReleaseWheel;
    public static Action actOnReleaseGas;

    private static float LERP_STRAIGHT_WHEEL = 0.1f;
    private static float LERP_TURN_WHEEL = 10.0f;
    private static float MVM_SPEED = 1.0f;
    private static float LERP_STOP = 0.5f;

    [SerializeField]
    Transform wheelFrontRight;
    [SerializeField]
    Transform wheelFrontLeft;
    [SerializeField]
    public int maxRotationY;

    private bool isTurningLeft = false;
    private bool isTurningRight = false;
    private float xVel = 0.0f;

    private bool isPressGas = false;
    private bool isBrake = false;
    private float zVel = 0.0f;

    private void Awake()
    {
        Instance = this;

        actOnControlWheel += OnControlWheel;
        actOnReleaseWheel += OnReleaseWheel;

        actOnControlGas += OnControlGas;
        actOnReleaseGas += OnReleaseGas;
    }

    void Start()
    {
    }

    void Update()
    {
        // control x velocity
        //if (isTurningRight || isTurningLeft)
        //{
        //    xVel = Mathf.Clamp(xVel + ((isTurningRight ? 1 : -1) * Time.deltaTime * LERP_TURN_WHEEL), -1.0f, 1.0f);
        //}
        //else if (xVel != 0)
        //{
        //    xVel = Mathf.Lerp(xVel, 0.0f, LERP_STRAIGHT_WHEEL);
        //}

        //// rotate wheels
        //float rotateY = maxRotationY * xVel;
        //var angle = wheelFrontRight.transform.eulerAngles;
        //angle.y = rotateY;
        //wheelFrontRight.transform.eulerAngles = angle;
        //wheelFrontLeft.transform.eulerAngles = angle;

        //// control z velocity
        //if (isBrake || isPressGas)
        //{
        //    zVel = Mathf.Clamp(zVel + ((isPressGas ? 1 : -1) * Time.deltaTime * LERP_TURN_WHEEL), -1.0f, 1.0f);
        //}
        //else if (zVel != 0)
        //{
        //    zVel = Mathf.Lerp(zVel, 0.0f, LERP_STOP);
        //}

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if (vertical != 0)
        {
            transform.Translate(Vector3.forward * vertical * Time.deltaTime * 5.0f);
            transform.Rotate(Vector3.up, horizontal * 100.0f * Time.deltaTime);
        }

        horizontal = Math.Abs(horizontal);
        wheelFrontRight.transform.Rotate(Vector3.up, horizontal * 100.0f * Time.deltaTime);
        wheelFrontLeft.transform.Rotate(Vector3.up, horizontal * 100.0f * Time.deltaTime);

        // update position
        if (zVel != 0)
        {
            

            //var pos = transform.position;
            //pos.x += xVel * 0.01f;
            //pos.z += zVel * 0.01f;
            //transform.position = pos;
        }
    }

    private void OnDestroy()
    {
        actOnControlWheel -= OnControlWheel;
        actOnReleaseWheel -= OnReleaseWheel;

        actOnControlGas -= OnControlGas;
        actOnReleaseGas -= OnReleaseGas;
    }

    public bool IsControlGas()
    {
        return isPressGas || isBrake;
    }
    public bool IsControlWheel()
    {
        return isTurningLeft || isTurningRight;
    }

    private void OnControlWheel(bool isOnTurnRight)
    {
        isTurningRight = isOnTurnRight;
        isTurningLeft = !isOnTurnRight;
    }
    private void OnReleaseWheel()
    {
        if (isTurningLeft)
            isTurningLeft = false;

        if (isTurningRight)
            isTurningRight = false;
    }

    private void OnControlGas(bool isPressGas)
    {
        this.isPressGas = isPressGas;
        this.isBrake = !isPressGas;
    }
    private void OnReleaseGas()
    {
        if (isPressGas)
            isPressGas = false;

        if (isBrake)
            isBrake = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CarController : MonoBehaviour
{
    public static Action<bool> actOnControlWheel;
    public static Action actOnReleaseWheel;

    [SerializeField]
    Transform wheelFrontRight;
    [SerializeField]
    Transform wheelFrontLeft;
    [SerializeField]
    public int maxRotationY;

    private float wheelHoldTime = 0.0f;
    private bool isTurningLeft = false;
    private bool isTurningRight = false;

    private void Awake()
    {
        actOnControlWheel += OnControlWheel;
        actOnReleaseWheel += OnReleaseWheel;
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (isTurningRight || isTurningLeft)
        {
            wheelHoldTime += (isTurningRight ? 1 : -1) * Time.deltaTime;
            wheelHoldTime = Mathf.Clamp(wheelHoldTime, -1.0f, 1.0f);
        }
        else if (wheelHoldTime != 0)
        {
            wheelHoldTime = Mathf.Lerp(wheelHoldTime, 0.0f, 0.1f);
        }

        float rotateY = maxRotationY * wheelHoldTime;
        var angle = wheelFrontRight.transform.eulerAngles;
        angle.y = rotateY;
        wheelFrontRight.transform.eulerAngles = angle;
        wheelFrontLeft.transform.eulerAngles = angle;
    }

    private void OnDestroy()
    {
        actOnControlWheel -= OnControlWheel;
        actOnReleaseWheel -= OnReleaseWheel;
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
}

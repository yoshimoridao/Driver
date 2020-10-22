using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WheelButton : Button
{
    public bool isRightWheel;
    private void Start()
    {
        isRightWheel = gameObject.name.Contains("right");
    }

    public override void OnPointerDown(PointerEventData e)
    {
        CarController.actOnControlWheel(isRightWheel);
    }

    public override void OnPointerUp(PointerEventData e)
    {
        CarController.actOnReleaseWheel();
    }
}

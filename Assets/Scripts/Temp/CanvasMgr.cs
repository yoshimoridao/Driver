using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasMgr : MonoBehaviour
{
    [SerializeField] Button turnLeftBtn;
    [SerializeField] Button turnRightBtn;

    void Start()
    {
    }

    void Update()
    {
        
    }

    public void OnTurnLeft()
    {
        CarController.actOnControlWheel(false);
    }

    public void OnTurnRight()
    {
        CarController.actOnControlWheel(true);
    }
}

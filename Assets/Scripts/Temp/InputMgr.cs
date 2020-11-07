using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMgr : MonoBehaviour
{
    private CarController carController;

    void Start()
    {

    }

    void Update()
    {
        if (carController == null)
            carController = CarController.Instance;

        // control wheel
        if (Input.GetKey(KeyCode.A))
        {
            CarController.actOnControlWheel(false);
            Debug.Log("press A");
        }
        else if (Input.GetKey(KeyCode.D))
        {
            CarController.actOnControlWheel(true);
        }
        else if (carController && carController.IsControlWheel())
        {
            CarController.actOnReleaseWheel();
        }

        // control gas
        if (Input.GetKey(KeyCode.W))
        {
            CarController.actOnControlGas(true);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            CarController.actOnControlGas(false);
        }
        else if (carController && carController.IsControlGas())
        {
            CarController.actOnReleaseGas();
        }
    }
}

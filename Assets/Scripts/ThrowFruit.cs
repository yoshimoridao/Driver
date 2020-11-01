using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ThrowFruit : MonoBehaviour
{
    Vector2 startPos, endPos, vel; // touch start position, touch end position, swipe direction
    float touchTimeStart, touchTimeFinish, timeInterval; // to calculate swipe time to control throw force in Z direction

    [SerializeField] List<GameObject> prefFruits;
    [SerializeField] Transform projectileT;
    [SerializeField] float reloadTime = 0.5f;
    [SerializeField] float destroyTime = 5.0f;
    [SerializeField] float throwForceInXandY = 1f; // to control throw force in X and Y directions
    [SerializeField] float throwForceInZ = 50f; // to control throw force in Z direction

    [SerializeField] TMP_InputField xyField;
    [SerializeField] TMP_InputField zField;

    Rigidbody rbFruit;
    float spawnTime;

    public void SetThrowXY()
    {
        throwForceInXandY = float.Parse(xyField.text);
    }
    public void SetThrowZ()
    {
        throwForceInZ = float.Parse(zField.text);
    }

    void Start()
    {
        xyField.text = throwForceInXandY.ToString();
        zField.text = throwForceInZ.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (rbFruit)
        {
            InputPC();
        }
        else if (spawnTime == 0.0f)
        {
            spawnTime = reloadTime;
        }

        UpdateSpawnFruits();
    }

    void UpdateSpawnFruits()
    {
        if (spawnTime == 0.0f)
            return;

        spawnTime -= Time.deltaTime;
        if (spawnTime <= 0.0f)
        {
            spawnTime = 0.0f;
            SpawnFruits();
        }
    }

    void InputTouch()
    {
        // if you touch the screen
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // getting touch position and marking time when you touch the screen
            touchTimeStart = Time.time;
            startPos = Input.GetTouch(0).position;
        }

        // if you release your finger
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            touchTimeFinish = Time.time;
            timeInterval = touchTimeFinish - touchTimeStart;

            endPos = Input.GetTouch(0).position;
            vel = startPos - endPos;

            // add force to balls rigidbody in 3D space depending on swipe time, direction and throw forces
            rbFruit.isKinematic = false;
            rbFruit.AddForce(-vel.x * throwForceInXandY, -vel.y * throwForceInXandY, throwForceInZ / timeInterval);

            Destroy(rbFruit.gameObject, destroyTime);
            rbFruit = null;
        }
    }

    void InputPC()
    {
        if (rbFruit == null)
            return;

        // if you touch the screen
        if (Input.GetMouseButtonDown(0))
        {
            // getting touch position and marking time when you touch the screen
            touchTimeStart = Time.time;
            startPos = Input.mousePosition;
        }

        // if you release your finger
        if (Input.GetMouseButtonUp(0))
        {
            touchTimeFinish = Time.time;
            timeInterval = touchTimeFinish - touchTimeStart;

            endPos = Input.mousePosition;
            vel = startPos - endPos;

            // add force to balls rigidbody in 3D space depending on swipe time, direction and throw forces
            rbFruit.isKinematic = false;
            rbFruit.AddForce(-(vel.magnitude * throwForceInZ), -vel.y * throwForceInXandY, -vel.x * throwForceInXandY);  // note: revert x - z

            Destroy(rbFruit.gameObject, destroyTime);
            rbFruit = null;
        }
    }

    void SpawnFruits()
    {
        var newFruit = Instantiate(prefFruits[0], transform);
        newFruit.transform.position = projectileT.position;
        rbFruit = newFruit.GetComponent<Rigidbody>();
    }
}

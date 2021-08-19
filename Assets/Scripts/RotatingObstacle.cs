using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingObstacle : MonoBehaviour
{

    [Header("Rotation on Axis")]
    [SerializeField] float rotationSpeed = 100f;
    [SerializeField] bool x = true;
    [SerializeField] bool y = true;
    [SerializeField] bool z = true;

    [Header("Movement on Axis")]
    [SerializeField] float period = 2f;
    float movementRange;
    float timeSinceStarted = 0f;
    [SerializeField] Vector3 destinationPos;
    [SerializeField] float startDelay;
    Vector3 originPos;


    float rotX, rotY, rotZ;

    // Start is called before the first frame update
    void Start()
    {
        originPos = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        GenerateRotation();
        GenerateMovement();
    }

    void GenerateRotation()
    {
        if (x) { rotX = 1; } else { rotX = 0; }
        if (y) { rotY = 1; } else { rotY = 0; }
        if (z) { rotZ = 1; } else { rotZ = 0; }
        float rotAmount = rotationSpeed * Time.deltaTime;
        transform.Rotate(rotAmount * rotX, rotAmount * rotY, rotAmount * rotZ, Space.World);
    }

    void GenerateMovement()
    {
        if (Time.timeSinceLevelLoad > startDelay)
        {
            timeSinceStarted += Time.deltaTime;
            if (period <= Mathf.Epsilon) { return; } //If period is less or equal 0 (mathf.epsilon is the tiniest float) we return so we don't make calculations.
            //float cycle = Time.time / period; //constantly growing manages cycle duration
            float cycle = timeSinceStarted / period; //constantly growing manages cycle duration
            const float tau = Mathf.PI * 2; //constant for sin calculating

            float rawSinWave = Mathf.Sin(cycle * tau); //Going from -1 to 1 

            movementRange = (rawSinWave + 1f) / 2f; // recalculate to go from 0 to 1.

            Vector3 offset = destinationPos * movementRange;
            transform.position = originPos + offset;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class steeringwheel : MonoBehaviour
{

    // Choose the axis you want to constrain (X, Y, or Z)
    public bool constrainX;
    public bool constrainY;
    public bool constrainZ;

    // Update is called once per frame
    void Update()
    {
        // Store current rotation
        Vector3 currentRotation = transform.rotation.eulerAngles;

        // Constrain rotation on selected axis
        if (constrainX)
            currentRotation.x = -90;
        if (constrainY)
            currentRotation.y = 0;
        if (constrainZ)
            currentRotation.z = 0;

        // Apply constrained rotation
        transform.rotation = Quaternion.Euler(currentRotation);
    }
}

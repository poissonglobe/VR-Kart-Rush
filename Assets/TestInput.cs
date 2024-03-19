using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestInput : MonoBehaviour
{

    public InputActionProperty accelerationAction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //wheel.wheelCollider.motorTorque = 600 * maxAcceleration * Time.deltaTime;
        float rightTriggerValue = accelerationAction.action.ReadValue<float>();
        Debug.Log(rightTriggerValue);
    }
}

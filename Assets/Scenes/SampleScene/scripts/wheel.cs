using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class Carcontroller : MonoBehaviour

{
    public InputActionProperty accelerationAction;
    public InputActionProperty brakeAction;

    public enum Axel
    {
        Front,
        Rear
    }

    [Serializable]
    public struct Wheel
    {
        public GameObject wheelModel;
        public WheelCollider wheelCollider;
        public Axel axel;

    }

    public float maxAcceleration = 30.0f;
    public float brakeAcceleration = 50.0f;
    public GameObject steeringWheel;

    public float turnSensibility = 1.0f;
    public float maxSteerAngle = 30.0f;

    public Vector3 _centerOfMass;


    public List<Wheel> wheels;
    public float moveInput = 1.0f;
    public float steerInput = 1.0f;
    private Rigidbody carRb;
    public float LeftAccel;
    public float rightAccel;



    // Start is called before the first frame update
    void Start()
    {
        carRb = GetComponent<Rigidbody>();
        carRb.centerOfMass = _centerOfMass;
    }

    private void Update()
    {
        GetInputs();
    }
    void LateUpdate()
    {
        Move();
        Steer();
    }

    void GetInputs()
    {
        steerInput = Input.GetAxis("Horizontal");
    }

    void Move()
    {
        //value of the trigger
        float rightTriggerValue = accelerationAction.action.ReadValue<float>();
        rightAccel = rightTriggerValue;

        foreach (var wheel in wheels)
        {
            wheel.wheelCollider.motorTorque = rightTriggerValue * 600 * maxAcceleration * Time.deltaTime;
        }

        float leftTriggerValue = brakeAction.action.ReadValue<float>();
        Debug.Log(leftTriggerValue);
        LeftAccel = leftTriggerValue;

        foreach (var wheel in wheels)
        {
            if (wheel.wheelCollider.motorTorque > 0)
            {
                wheel.wheelCollider.brakeTorque = leftTriggerValue * 300 * brakeAcceleration * Time.deltaTime;
            }
            else
            {
                wheel.wheelCollider.motorTorque = - leftTriggerValue * 100 * brakeAcceleration * Time.deltaTime;
            }
        }
    }

    void Steer ()
    {
        foreach(var wheel in wheels)
        {
            if(wheel.axel == Axel.Front)
            {
                float _steeringWheel = steeringWheel.transform.rotation.eulerAngles.y;
                var _steerAngle = _steeringWheel * steerInput * turnSensibility * maxSteerAngle;
                wheel.wheelCollider.steerAngle = Mathf.Lerp(wheel.wheelCollider.steerAngle, _steerAngle, 0.6f);
            }
        }
    }
}

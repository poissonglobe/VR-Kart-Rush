using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class carcontroller : MonoBehaviour

{
    // Vos axes X et Y du joystick Oculus

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

    public InputActionProperty accelerationAction;
    public InputActionProperty brakeAction;
    public InputActionProperty direction;

    public float maxAcceleration = 30.0f;
    public float brakeAcceleration = 50.0f;
    public float turnSensibility = 0.75f;
    public float maxSteerAngle = 20.0f;
    public float moveInput = 1.0f;
    public float LeftAccel;
    public float rightAccel;
    public float steerdirection;

    public GameObject steeringWheel;

    public Vector3 _centerOfMass;

    public List<Wheel> wheels;
    
    private Rigidbody carRb;
    

    // Start is called before the first frame update
    void Start()
    {
        carRb = GetComponent<Rigidbody>();
        carRb.centerOfMass = _centerOfMass;
    }

    private void Update()
    {
        Move();
        Steer();
    }

    void Move()
    {
        //value of the trigger
        float rightTriggerValue = accelerationAction.action.ReadValue<float>();
        rightAccel = rightTriggerValue;

        foreach (var wheel in wheels)
        {
            if (wheel.wheelCollider.motorTorque < 1000)
            {
                wheel.wheelCollider.motorTorque = rightTriggerValue * 600 * maxAcceleration * Time.deltaTime;
            }
            
        }

        float leftTriggerValue = brakeAction.action.ReadValue<float>();
        LeftAccel = leftTriggerValue;

        foreach (var wheel in wheels)
        {
            if (wheel.wheelCollider.motorTorque > 0)
            {
                wheel.wheelCollider.brakeTorque = LeftAccel * 600 * brakeAcceleration * Time.deltaTime;
            }
            else
            {
                wheel.wheelCollider.motorTorque = -LeftAccel * 300 * brakeAcceleration * Time.deltaTime;
            }
        }
    }

    void Steer()
    {
        Vector2 directionTriggerValue = direction.action.ReadValue<Vector2>();
        steerdirection = directionTriggerValue.x;
        foreach (var wheel in wheels)
        {
            if (wheel.axel == Axel.Front)
            {
                //float _steeringWheel = steeringWheel.transform.rotation.eulerAngles.y;
                var _steerAngle = steerdirection * turnSensibility * maxSteerAngle;
                wheel.wheelCollider.steerAngle = Mathf.Lerp(wheel.wheelCollider.steerAngle, _steerAngle, 0.6f);
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Carcontroller : MonoBehaviour

{ 
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
        Brake();
    }

    void GetInputs()
    {

        moveInput = Input.GetAxis("Vertical");
        steerInput = Input.GetAxis("Horizontal");
    }

    void Move()
    {
        foreach (var wheel in wheels)
        {
            wheel.wheelCollider.motorTorque = moveInput * 600 * maxAcceleration * Time.deltaTime;
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
                Debug.Log(_steeringWheel);
            }
        }
    }

    void Brake()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            foreach(var wheel in wheels)
            {
                wheel.wheelCollider.brakeTorque = 300 * brakeAcceleration * Time.deltaTime;

            }
        }else
        {
            foreach(var wheel in wheels)
            {
                wheel.wheelCollider.brakeTorque = 0;
            }
        }
    }
    
}

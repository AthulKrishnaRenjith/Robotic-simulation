using System;
 using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 using System.Threading.Tasks;
 public class RobotController : MonoBehaviour
 {
    // naming constraints do not change
    [SerializeField] private WheelCollider FLC;
    [SerializeField] private WheelCollider FRC;
    [SerializeField] private WheelCollider RLC;
    [SerializeField] private WheelCollider RRC;
    [SerializeField] private Transform FLT;
    [SerializeField] private Transform FRT;
    [SerializeField] private Transform RLT;
    [SerializeField] private Transform RRT;
    [SerializeField] private Transform FRS;
    [SerializeField] private Transform L1S;
    [SerializeField] private Transform L2S;
    [SerializeField] private Transform L3S;
    [SerializeField] private Transform R1S;
    [SerializeField] private Transform R2S;
    [SerializeField] private Transform R3S;
    [SerializeField] private Transform ORS;
    private float maxSteeringAngle = 40f;
    [SerializeField] private float motorForce;
    [SerializeField] private float brakeForce;
    private Rigidbody rb;
    [SerializeField] private float angle_x;
    [SerializeField] private float angle_z;
    [SerializeField] private float velocity;
    private float steerAngle;
    private bool isBreaking;
    private float s1dist = 5;
    private float s3dist = 6;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        float s1x = 0; float s1y = 10; float s1z = 0;
        float s3x = 14; float s3y = 50; float s3z = 0;
        AdjustSensors(FRS, 25, 0, 0);
        AdjustSensors(L1S, s1x, -s1y, s1z);
        AdjustSensors(L3S, s3x, -s3y, s3z);
        AdjustSensors(R1S, s1x, s1y, s1z);
        AdjustSensors(R3S, s3x, s3y, s3z);
        AdjustSensors(ORS, 50, 180, 0);
    }
    private void FixedUpdate()
    {
        isBreaking = false;
        StayOnRoad();
        AvoidObstacles();
        AdjustSpeed();
        HandleMotor();
        UpdateWheels();
        angle_x = ORS.eulerAngles.x;
        angle_z = NormalizeAngle(ORS.eulerAngles.z);
        velocity = rb.velocity.magnitude;
    }
    private void AdjustSensors(Transform sensor, float x_angle, float y_angle, float z_angle)
    {
        sensor.transform.Rotate(x_angle, y_angle, z_angle);
    }
        
    private void HandleMotor()
    {
        FLC.motorTorque = motorForce;
        FRC.motorTorque = motorForce;
        RLC.motorTorque = motorForce;
        RRC.motorTorque = motorForce;
        brakeForce = isBreaking ? 2000f : 0f;
        FLC.brakeTorque = brakeForce;
        FRC.brakeTorque = brakeForce;
        RLC.brakeTorque = brakeForce;
        RRC.brakeTorque = brakeForce;
    }
    private void UpdateWheels()
    {
        UpdateWheelPos(FLC, FLT);
        UpdateWheelPos(FRC, FRT);
        UpdateWheelPos(RLC, RLT);
        UpdateWheelPos(RRC, RRT);
    }
    private void UpdateWheelPos(WheelCollider wheelCollider, Transform trans)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        trans.position = pos;
        trans.rotation = rot;
    }
    private void HandleSteering(float direction)
    {
        if (velocity > 5 && motorForce > 0)
        {
            motorForce -= 5f;
        }
        steerAngle = maxSteeringAngle * direction;
        FLC.steerAngle = steerAngle;
        FRC.steerAngle = steerAngle;
    }
    private bool sense(Transform sensor, float dist)
    {
        RaycastHit hit;
        if (Physics.Raycast(sensor.position, sensor.TransformDirection(Vector3.forward), out hit, dist))
        {
            Debug.DrawRay(sensor.position, sensor.TransformDirection(Vector3.forward) * dist, Color.yellow);
            if (hit.transform.parent.name == "CheckPoints")
            {
                if (sensor == L1S || sensor == R1S) { return false; }
                else { return true; }
            }
            if (hit.transform.parent.name == "Ground")
            {
                return false;
            }
            return true;
        }
        else
        {
            Debug.DrawRay(sensor.position, sensor.TransformDirection(Vector3.forward) * dist, Color.white);
            return false;
        }
    }
    private void StayOnRoad()
    {
        if (!sense(FRS, s3dist))
        {
            isBreaking = true;
        }
        if (!sense(L3S, s3dist) || !sense(R3S, s3dist))
        {
            if (!sense(L3S, s3dist) && sense(R3S, s3dist))
            {
                HandleSteering(1);
            }
            else if (!sense(R3S, s3dist) && sense(L3S, s3dist))
            {
                HandleSteering(-1);
            }
        }
        else
        {
            HandleSteering(0);
        }
    }
    private float NormalizeAngle(float angle)
    {
        return (angle > 180) ? angle - 360 : angle;
    }
    private void AdjustSpeed()
    {
        if (velocity < 1 && motorForce < 200)
        {
            motorForce = motorForce + 0.5f;
        }
        if (velocity > 4 && motorForce > 0)
        {
            motorForce = motorForce - 0.5f;
        }
        if ((angle_z > 1 && angle_z < 6 && motorForce < 400) || (angle_z < -1 && angle_z > -60 && motorForce < 400))
        {
            motorForce = motorForce + 2.5f;
        }
        if (velocity > 5 && motorForce > 0)
        {
            motorForce -= 5f;
        }
    }
    private void AvoidObstacles()
    {
        if (sense(L1S, s1dist))
        {
            HandleSteering(1);
        }
        if (sense(R1S, s1dist))
        {
            HandleSteering(-1);
        }
    }
 }
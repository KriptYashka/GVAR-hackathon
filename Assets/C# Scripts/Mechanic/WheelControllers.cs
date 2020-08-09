using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelControllers : MonoBehaviour
{
    [Header("Физические колеса")]
    public WheelCollider[] WColForward;
    public WheelCollider[] WColBack;
    [Header("Видимые колеса")] 
    public Transform[] wheelsF;
    public Transform[] wheelsB;
    [Tooltip("Макс угол поворота колес")]
    public float maxSteer = 30;
    [Tooltip("Макс крутящий момент")]
    public float maxAccel = 25;
    [Tooltip("Макс тормозной момент")]
    public float maxBrake = 50;
    [Header("CenterOfMass(COM)")] 
    public Transform COM;
    public float wheelOffset = 0.1f; 
    public float wheelRadius = 0.13f;
    public class WheelData
    {
        public Transform wheelTransform;
        public WheelCollider col;
        public Vector3 wheelStartPos;
        public float rotation = 0.0f;
    }

    protected WheelData[] wheels;
    private Rigidbody rigidBody;
  
    
        void Start()
        {
            rigidBody = GetComponentInParent<Rigidbody>();
            rigidBody.centerOfMass = COM.localPosition;
            wheels = new WheelData[WColForward.Length+WColBack.Length];
            for (int i = 0; i < WColForward.Length; i++)
            {
                wheels[i] = SetupWheels(wheelsF[i], WColForward[i]);
            }

            for (int i = 0; i < WColBack.Length; i++)
            {
                wheels[i + WColForward.Length] = SetupWheels(wheelsB[i], WColBack[i]);
            }
        }

        private WheelData SetupWheels(Transform wheel, WheelCollider col)
        {
            WheelData result = new WheelData();
            result.col = col;
            result.wheelStartPos = wheel.transform.localPosition;
            //result.wheelTransform = wheel.transform;
            return result;
        }

        private void FixedUpdate()
        {
            float accel = 0;
            float steer = 0;
            accel = Input.GetAxis("Vertical");
            steer = Input.GetAxis("Horizontal");
            //_wheelControllers.CmdFixedUpdateThree(accel, steer);
            CmdFixedUpdateThree(accel, steer);
            //UpdateWheels();
        }
        // Update is called once per frame
  
    public void CmdFixedUpdateThree(float accel, float steer)
    {
        FixedUpdateThree(accel, steer);
    }
    public void UpdateWheels()
    {
        float delta = Time.fixedDeltaTime;
        foreach (WheelData w in wheels)
        {
            WheelHit hit;
            print(w.wheelTransform.position);
            Vector3 lp = w.wheelTransform.transform.localPosition;
            if (w.col.GetGroundHit(out hit))
            {
                lp.y -= Vector3.Dot(w.wheelTransform.position - hit.point, transform.up) - wheelRadius;
                
            }
            else
            {
                lp.y = w.wheelStartPos.y - wheelOffset;
            }

            w.wheelTransform.transform.localPosition = lp;
            w.rotation = Mathf.Repeat(w.rotation + delta * w.col.rpm * 360.0f / 60.0f, 360.0f); 
            w.wheelTransform.transform.localRotation = Quaternion.Euler(w.rotation, w.col.steerAngle, 90.0f);
        }
    }

    public void FixedUpdateThree(float accel, float steer)
    {
        foreach (WheelCollider col in WColForward)
        {
            col.steerAngle = steer * maxSteer;
            
        }

        if (accel == 0)
        {
            foreach (WheelCollider col in WColBack)
            {
                col.brakeTorque = maxBrake;
                
            }
        }
        else
        {
            foreach (WheelCollider col in WColBack)
            {
                col.brakeTorque = 0;
                col.motorTorque = accel * maxAccel;
            }
        }
    }
}

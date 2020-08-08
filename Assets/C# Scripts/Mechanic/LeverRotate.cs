using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverRotate : MonoBehaviour
{
    Transform wheel;

    public float wheelYposMax = -0.0006f;
    public float wheelYposMin = -0.001f;
    public float LeverAngleMax = 40;
    public float LeverAngleMin = -45;

    Vector3 startpos = new Vector3();
    private void Start()
    {

        wheel = this.transform.parent;
        this.transform.parent = transform.parent.parent;
        startpos = this.transform.localPosition;
    }
    private void FixedUpdate()
    {
        
        float x_angle = Map(wheel.localPosition.y, wheelYposMin, wheelYposMax, LeverAngleMax, LeverAngleMin);
        float x_percentage = Map(x_angle, LeverAngleMax, LeverAngleMin, 0, 1);
        

        this.transform.localRotation = Quaternion.AngleAxis(x_angle, new Vector3(1, 0, 0));
        this.transform.localPosition = Vector3.Slerp(new Vector3(startpos.x, startpos.y, startpos.z), new Vector3(startpos.x, startpos.y + Mathf.Abs(wheelYposMax), startpos.z), x_percentage);
        
    }
    public float Map(float value, float fromSource, float toSource, float fromTarget, float toTarget)
    {
        return (value - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget;
    }
}
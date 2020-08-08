using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTank : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float lookSpeed = 2f;

    private TestMove motor;

    void Start()
    {
        motor = GetComponent<TestMove>();
    }


    void Update()
    {
        //Move
        float xMov = Input.GetAxisRaw("Horizontal");
        float zMov = Input.GetAxisRaw("Vertical");

        Vector3 movHor = transform.right * xMov;
        Vector3 movVer = transform.forward * zMov;
        Vector3 velocity = (movHor + movVer).normalized * speed;

        motor.Move(velocity);

        //Rotation
        float yRot = Input.GetAxisRaw("Mouse X");
        float xRot = Input.GetAxisRaw("Mouse Y");

        Vector3 rotation = new Vector3(0f, yRot, 0f) * lookSpeed;
        Vector3 camRotation = new Vector3(xRot, 0f, 0f) * lookSpeed;

        motor.Rotate(rotation);
        motor.RotateCam(-camRotation);
    }
}

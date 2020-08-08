using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TestMove : NetworkBehaviour
{
    [SerializeField]
    private Rigidbody rb;
    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private Vector3 camRotation = Vector3.zero;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (!isLocalPlayer) return;
        PerformMove();
        PerformRotation();
    }

    public void Move(Vector3 vel)
    {
        velocity = vel;
    }

    public void Rotate(Vector3 rot)
    {
        rotation = rot;
    }

    public void RotateCam(Vector3 camRot)
    {
        camRotation = camRot;
    }

    void PerformMove()
    {
        if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
    }

    void PerformRotation()
    {
        if (rotation != Vector3.zero)
        {
            rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        }
    }
}

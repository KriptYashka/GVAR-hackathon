using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMoving : MonoBehaviour
{
    public GameObject platform;
    public float x;
    public float y;
    public float z;
    public float speed = 1f;
    void Start()
    {

    }

    void Update()
    {
        transform.RotateAround(platform.transform.position, new Vector3(x, y, z), speed * Time.deltaTime);
    }
}
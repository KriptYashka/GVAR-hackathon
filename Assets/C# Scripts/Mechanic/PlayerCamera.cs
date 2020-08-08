using System;
using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour
{

   
 //   public Transform Rotate;
    public Transform CameraPosition;
    public float speedRotateCamera = 5f;
    [Space]
    [Tooltip("приближение камеры при соприкосновении с LayerMask")]
    public Transform HitPositionOfCamera ;
    public LayerMask MaskObstacles;
    void Start()
    {
        transform.parent = null;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Cursor.visible = true; 
        }

        RaycastHit hit;
        if (Physics.Raycast(HitPositionOfCamera.position, transform.position - HitPositionOfCamera.position, out hit,
            Vector3.Distance(transform.position, HitPositionOfCamera.position), MaskObstacles))
        {
            transform.position = hit.point;
            transform.LookAt(HitPositionOfCamera);
        }
    }
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, CameraPosition.position, 1);
        transform.rotation = Quaternion.Lerp(transform.rotation, CameraPosition.rotation, speedRotateCamera);
    }
    void OnGUI()
    {
        //GUI.DrawTexture(camera_aim_position, camera_aim);
    }
    public Vector3 GetAimPoint()
        /// <summary>
        /// Функция поиска точки экранного прицела
        /// </summary>
        /// <returns>Vector3 точки в пространстве</returns>
    {
        Ray ray = new Ray(transform.position, transform.forward);
        Vector3 currentCamTarget;
        if (Physics.Raycast(ray, out RaycastHit hit, 500))
        {
            currentCamTarget = hit.point;
        }
        else
        {
            currentCamTarget = transform.TransformPoint(Vector3.forward * 100);
        }
        Debug.DrawLine(ray.origin, currentCamTarget, Color.red);

        return currentCamTarget;
    }
}

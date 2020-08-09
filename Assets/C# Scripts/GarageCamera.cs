using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Для управления курсором, ибо Unity не позволяет ставить курсор в свою позицию
public class Win32
{
    [DllImport("user32.dll")]
    public static extern long SetCursorPos(int x, int y);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool GetCursorPos(out POINT lpPoint);

    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int X;
        public int Y;

        public POINT(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}

// Скрипт вращающейся камеры для гаража
public class GarageCamera : MonoBehaviour
{
    // // //

    [SerializeField, Header("Настройки камеры"), Tooltip("Камера, с которой будут производиться все действия, по умолчанию находит камеру в компонентах")]
    private Camera TargetCamera;
    [SerializeField, Tooltip("Центральная точка, вокруг которой камера будет вращаться")]
    private Vector3 CameraCenter = Vector3.zero;
    [SerializeField, Tooltip("Стартовая дистанция камеры от центральной точки")]
    private float StartDistance = 20.0f;
    [SerializeField, Tooltip("Максимальная дистанция камеры от центральной точки")]
    private float MaxDistance = 50.0f;
    [SerializeField, Tooltip("Минимальная дистанция камеры от центральной точки")]
    private float MinDistance = 1.0f;
    [SerializeField, Tooltip("Стартовые углы Эйлера камеры")]
    private Vector2 StartAngles = new Vector2(0.0f, 45.0f);
    [SerializeField, Tooltip("Скорость движения камеры по углу X Эйлера")]
    private float AngleXSpeed = 1.0f;

    // // //

    [SerializeField, Header("Мышь"), Tooltip("Включает/выключает режим самостоятельного управления мышью")]
    private bool MouseControlEnable = true;
    [SerializeField, Tooltip("Чувствительность мыши при самостоятельном управлении камерой")]
    private float MouseSensivity = 10.0f;
    [SerializeField, Tooltip("Чувствительность колёсика мыши при самостоятельном управлении камерой")]
    private float MouseScrollSensivity = 10.0f;
    [SerializeField, Tooltip("Инвертировать ли X координату мыши?")]
    private bool InvertX = false;
    [SerializeField, Tooltip("Инвертировать ли Y координату мыши?")]
    private bool InvertY = false;
    [SerializeField, Tooltip("Закреплять ли курсор при самостоятельном управлении мышью?")]
    private bool MouseLockEnable = true;

    // // //

    [SerializeField, Header("Плавное движение камеры"), Tooltip("Включает/выключает плавное движение камеры")]
    private bool SmoothingEnable = true;
    [SerializeField, Tooltip("Фактор плавного движения камеры"), Range(0.0f, 1.0f)]
    private float SmoothingFactor = 0.2f;

    // // //

    float TargetDistance;
    float CurrentDistance;

    Vector2 TargetAngles;
    Vector2 CurrentAngles;

    Vector2 PrelockMousePos;
    bool MouseLocked;
    bool MouseUsed;

    // // //

    void Start()
    {
        if (TargetCamera == null)
            TargetCamera = GetComponent<Camera>();

        CurrentDistance = TargetDistance = StartDistance;
        CurrentAngles = TargetAngles = StartAngles / Mathf.Rad2Deg; // Переводим углы Эйлера из градусов в радианы

        PrelockMousePos = Vector2.zero;
        MouseLocked = false;
    }

    void Update()
    {
        if (MouseControlEnable)
            ProcessInput();
        if (!MouseUsed)
            TargetAngles.x += AngleXSpeed * Time.deltaTime;

        SmoothParameters();
        UpdateCameraPosition();
    }

    void ProcessInput()
    {
        void ProcessMouseMove()
        {
            // Обновляем углы эйлера движением мыши и ограничиваем её
            if (InvertX)
                TargetAngles.x -= Input.GetAxis("Mouse X") * MouseSensivity * 0.01f;
            else
                TargetAngles.x += Input.GetAxis("Mouse X") * MouseSensivity * 0.01f;

            if (InvertY)
                TargetAngles.y += Input.GetAxis("Mouse Y") * MouseSensivity * 0.02f;
            else
                TargetAngles.y -= Input.GetAxis("Mouse Y") * MouseSensivity * 0.02f;

            TargetAngles.y = Mathf.Clamp(TargetAngles.y, 0.0f, Mathf.PI / 2.0f);
        }

        if (!EventSystem.current.IsPointerOverGameObject()) // Находится ли курсор мыши над элементом интерфейса?
        {
            // Обновляем дистанцию колёсиком мыши и ограничиваем её
            TargetDistance -= Input.GetAxis("Mouse ScrollWheel") * MouseScrollSensivity;
            TargetDistance = Mathf.Clamp(TargetDistance, MinDistance, MaxDistance);

            if (MouseLockEnable && Input.GetMouseButtonDown(0))
            {
                MouseLocked = true;
                MouseUsed = true;

                Win32.POINT CursorPos;
                Win32.GetCursorPos(out CursorPos);
                PrelockMousePos = new Vector2((float)CursorPos.X, (float)CursorPos.Y);

                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else if (!MouseLockEnable && Input.GetMouseButton(0))
            {
                MouseUsed = true;
                ProcessMouseMove();
            }
        }
        if (MouseLockEnable && MouseLocked)
        {
            if (Input.GetMouseButton(0))
            {
                ProcessMouseMove();
            }
            if (Input.GetMouseButtonUp(0))
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                Win32.SetCursorPos((int)PrelockMousePos.x, (int)PrelockMousePos.y);
                MouseLocked = false;
            }
        }
        if (MouseUsed && Input.GetMouseButtonUp(0))
        {
            MouseUsed = false;
        }
    }

    void SmoothParameters()
    {
        if (SmoothingEnable) // Плавно интерполируем значения Current к значениям Target с помощью SmoothingFactor
        {
            float CurrentFactor = SmoothingFactor * 50.0f * Time.deltaTime;

            CurrentDistance = Mathf.Lerp(CurrentDistance, TargetDistance, CurrentFactor);
            CurrentAngles = Vector2.Lerp(CurrentAngles, TargetAngles, CurrentFactor);
        }
        else // Не интерполируем значения
        {
            CurrentDistance = TargetDistance;
            CurrentAngles = TargetAngles;
        }
    }

    void UpdateCameraPosition()
    {
        // Угловая позиция камеры
        float SinY = Mathf.Sin(CurrentAngles.y);
        Vector3 AngledPosition = Vector3.Normalize(new Vector3(Mathf.Sin(CurrentAngles.x) * (1.0f - SinY), SinY, Mathf.Cos(CurrentAngles.x) * (1.0f - SinY)));

        // Финальная позиция камеры
        Vector3 FinalPosition = AngledPosition * CurrentDistance + CameraCenter;

        // Ставим значения к камере
        TargetCamera.transform.position = FinalPosition;
        TargetCamera.transform.LookAt(CameraCenter);
    }
}

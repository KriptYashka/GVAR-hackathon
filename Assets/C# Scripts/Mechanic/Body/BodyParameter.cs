using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyParameter : MonoBehaviour
{
    [HideInInspector] public string title = "Нет названия";
    [HideInInspector] public string description = "Нет описания";
    [HideInInspector] public string typeMovement = "Тип движения неопределен";
    [HideInInspector] public string typeBody = "Тип корпуса неопределен";

    [HideInInspector] public float health = -1;
    [HideInInspector] public float maxSpeedMove = 0;
    [HideInInspector] public float acceleration = 0;
    [HideInInspector] public float speedRotate = 0;
    [HideInInspector] public float mass = 0;
}

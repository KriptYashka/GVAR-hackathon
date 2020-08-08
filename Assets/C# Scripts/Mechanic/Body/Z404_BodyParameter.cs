using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Z404_BodyParameter : BodyParameter
{
    
    void Awake()
    {
        title = "Тестовый Корпус";
        description = "Корпус, созданный по заказу GVAR Inc. Используется для тестирования функционала скриптов.";
        typeBody = "Medium";


        health = 1200f;
        typeMovement = "Track";
        maxSpeedMove = 30f;
        acceleration = 5f;
        speedRotate = 30f;
        mass = 2000f;
    }
}

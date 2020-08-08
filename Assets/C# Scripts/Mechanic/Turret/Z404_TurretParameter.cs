using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Z404_TurretParameter : TurretParameter
{
    void Awake()
    {
        title = "Тестовая башня";
        description = "Шестиугольная башня придает танку более футуристический вид и отлично защищает от снарядов, благодаря приведенной броне.";
        typeTurret = "Long";
        minDamage = 370f;
        maxDamage = 400f;
        minAngle = -5f;
        maxAngle = 20f;
        speedRotationTurret = 30f;
        speedRotationBarrel = 5f;
        reloadTime = 1f;
        recoil = 50f;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretParameter : MonoBehaviour
{
    /* Основные параметры */

    [HideInInspector] public string title = "Нет названия";
    [HideInInspector] public string description = "Нет описания";
    [HideInInspector] public string typeTurret = "Тип башни неопределен";
    [HideInInspector] public float minDamage = 0;
    [HideInInspector] public float maxDamage = 0;
    [HideInInspector] public float speedRotationTurret = 0;

    /* Ближний бой */

    [HideInInspector] public float gallonFill = 0;
    [HideInInspector] public float gallonWaste = 0;
    [HideInInspector] public float gallonRefresh = 0;
    [HideInInspector] public float angleImpact = 0;
    [HideInInspector] public float disanceImpact = 0;

    /* Дальний бой */

    [HideInInspector] public float minAngle = 0;
    [HideInInspector] public float maxAngle = 0;
    [HideInInspector] public float speedRotationBarrel = 0;
    [HideInInspector] public float reloadTime = 0;
    [HideInInspector] public float recoil = 0;
}

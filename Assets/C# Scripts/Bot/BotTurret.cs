using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotTurret : MonoBehaviour
{
    public Transform turret;
    public Transform barrel;
    public Transform aim;
    public float findRadius = 30f;
    public float speedRotateTurret = 10f;

    public GameObject enemy;
    private bool isFind = false;
    void Start()
    {
        
    }

    void Update()
    {
        FindEnemy();
        if (!isFind) return;
        MoveTurret();
    }

    void FindEnemy()
    {
        isFind = (Vector3.Distance(enemy.transform.position, this.transform.position) < findRadius);
    }

    void MoveTurret()
    {
        Vector3 target = enemy.transform.position;
        Quaternion directionGun = Quaternion.LookRotation(target - transform.position);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, directionGun, speedRotateTurret * Time.deltaTime);
        float tempAngleTowerY = transform.localEulerAngles.y;
        transform.localEulerAngles = new Vector3(0, tempAngleTowerY, 0);
    }
}

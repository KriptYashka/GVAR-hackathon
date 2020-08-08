using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    private float maxHealPoint;
    private float currentHealPoint;
    private float resist = 10f;
    private float reloadTime;
    private float reloadLeft;

    private TankTurret tankTurret;
    private BodyParameter bodyParameter;
    private TurretParameter turretParameter;
    PlayerInput PlayerInput;
    private LocalCanvas healthBar;

    void Start()
    {
        PlayerInput = this.GetComponent<PlayerInput>();
        bodyParameter = GetComponentInChildren<BodyParameter>();
        turretParameter = GetComponentInChildren<TurretParameter>();
        tankTurret = GetComponentInChildren<TankTurret>();

        maxHealPoint = bodyParameter.health;
        currentHealPoint = maxHealPoint;
        tankTurret.reloadTime = turretParameter.reloadTime;
    }

    public void TakeAwayHealth(float damage)
    {
        float totalDamage = damage * (100 - resist) / 100;
        currentHealPoint -= Mathf.Round(totalDamage);
        if (currentHealPoint <= 0)
        {
            currentHealPoint = 0;
            Dead();
        }
        healthBar.HealthBar(currentHealPoint);
    }

    private void Dead()
    {
        GetComponent<Rigidbody>().isKinematic = false;
        Destroy(this.gameObject, 3f);
    }


    private void InitializateTank(BodyParameter body, TurretParameter turret)
    {
        maxHealPoint = body.health;
        currentHealPoint = maxHealPoint; 
        // GetComponent<Rigidbody>().mass = body.mass; <-баг
        // Ещё будем думать. Нужно будет обговорить этот момент.
    }


    public float GetTankMaxHP()
    {
        print("Call");
        return maxHealPoint;
    }

    public float GetTankHP()
    {
        return currentHealPoint;
    }

    public float GetTankResist()
    {
        return resist;
    }

}

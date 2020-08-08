
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BulletController : MonoBehaviour
{
    public float destroyTime;
    public float speed;
    

    private Vector3 lastPos;
    private int TeammateMask = 11;
    private int EnemyMask=10;
    private int MapMask = 9;
    private int bitmask;
        //------
    public TankTurret _tankTurret;
    private float minDamage ;
    private float maxDamage ;
    void Start()
    {
        lastPos = transform.position;
        bitmask = ~(1 << TeammateMask) & ~(1 << MapMask) & (1 << EnemyMask);
        minDamage = _tankTurret.minDamage;
        maxDamage = _tankTurret.maxDamage;
    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        RaycastHit hit;

        if (Physics.Linecast(lastPos, transform.position, out hit, bitmask))
        {
            Debug.Log("-- Попадание по врагу "+ hit.transform.name +" -- {УРОН}: "+ RandDamage());
            hit.collider.GetComponent<Tank>().TakeAwayHealth(RandDamage());
            Destroy(this.gameObject);
        }
        lastPos = transform.position;
        destroyTime -= Time.deltaTime;
        if (destroyTime <= 0) Destroy(this.gameObject);
    }
    private float RandDamage()
    { 
        float bulletDamage = Random.Range(minDamage, maxDamage);
        return bulletDamage;
    }


}

using UnityEngine;
using System.Collections;

public class TankTurret : MonoBehaviour
{
    public GameObject cam;
    private PlayerCamera PlayerCamera;
    [Space]
    private Rigidbody tankRigidbody;
    public Transform barrel;
    public Transform gunAim;
    public GameObject bulletPrefab;

    [Header("Параметры башни")]
    public float speedRotateTurret = 15f;
    public float speedRotateGun = 0.3f;

    [Header("Префаб Огня")] 
    public GameObject firePrefab;
    public float spawnOffset = 1.0f;

    [Header("Отдача")] 
    public float recoilForce = 2000.0f;
    // [Header("Урон")]
    // public float minDamage = 365;
    // public float maxDamage = 400;
    
    public Transform AimTransform;

    [Header("Параметры орудия")]
    public float maximumAngleGun = 10f;
    public float minimumAngleGun = 5f;

    [Header("Перезарядка")]
    public float reloadTime = 1f;
    public bool fire = true;
    public bool recharge = false;

    void Start()
    {
        tankRigidbody = this.GetComponentInParent<Rigidbody>();
        PlayerCamera = cam.GetComponent<PlayerCamera>();
        
    }

    void FixedUpdate()
    {
        BarrelMove();
        TurretMove();
    }

    public void TurretMove()
    {
        Vector3 target = PlayerCamera.GetAimPoint();
        Quaternion directionGun = Quaternion.LookRotation(target - transform.position);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, directionGun, speedRotateTurret * Time.deltaTime);
        float tempAngleTowerY = transform.localEulerAngles.y;
        transform.localEulerAngles = new Vector3(0, tempAngleTowerY, 0);
    }

    public void BarrelMove()
    {

        Vector3 target = PlayerCamera.GetAimPoint();
        Quaternion directionGun = Quaternion.LookRotation(target - gunAim.position);
        barrel.rotation = Quaternion.RotateTowards(barrel.rotation, directionGun, speedRotateGun);

        float tempAngleGunX = barrel.localEulerAngles.x;
        if (barrel.localEulerAngles.x > 180f)
        {
            tempAngleGunX = Mathf.Clamp(tempAngleGunX, 360f - maximumAngleGun, 360f);
        }
        else
        {
            tempAngleGunX = Mathf.Clamp(tempAngleGunX, 0f, minimumAngleGun);
        }
        barrel.localEulerAngles = new Vector3(tempAngleGunX, 0, 0);

    }

    public IEnumerator Shoot()
    {
        GameObject fireObject = Instantiate(firePrefab, AimTransform.position, AimTransform.rotation) as GameObject;
        fireObject.transform.parent = AimTransform;

        GameObject bullet = Instantiate(bulletPrefab, AimTransform.position + AimTransform.forward * spawnOffset, AimTransform.rotation);
        
        // bullet.GetComponent<BulletController>().minDamage = minDamage;
        // bullet.GetComponent<BulletController>().maxDamage = maxDamage;
        
        tankRigidbody.AddForceAtPosition(-AimTransform.forward * recoilForce, AimTransform.position, ForceMode.Impulse);

        fire = false;
        recharge = true;
        yield return new WaitForSeconds(reloadTime);
        fire = true;
        recharge = false;
    }

    public Vector3 GetGunAimOnScreen()
    {
        Vector3 target = PlayerCamera.GetAimPoint();
        RaycastHit hit;
        Ray ray = new Ray(gunAim.transform.position, gunAim.transform.forward);

        // float distToAim = Vector3.Distance(target, gunAim.transform.position); На случай, если нужно знать дистанцию до цели

        Vector3 currentGunTarget;
        if (Physics.Raycast(ray, out hit, 100))
        {
            currentGunTarget = hit.point;
        }
        else
        {
            // TODO: Улучшить наведение в одну точку
            currentGunTarget = gunAim.transform.TransformPoint(Vector3.forward * 100);
        }
        Debug.DrawLine(ray.origin, currentGunTarget, Color.black);

        Vector3 screenPos = cam.GetComponent<Camera>().WorldToScreenPoint(currentGunTarget);

        return screenPos;
    }
}

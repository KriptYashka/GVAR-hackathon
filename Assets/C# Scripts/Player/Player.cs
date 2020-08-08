using System;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using Mirror;

public class Player : NetworkBehaviour
{
    public string nickname;
    public int dead = 0;
    public int killing = 0;

    public int teamId = -1;
    [SerializeField] private Color red;
    [SerializeField] private Color blue;

    [Header("Firing")]
    public KeyCode shootKey = KeyCode.Space;
    public GameObject bulletPrefab;
    public Transform bulletMount;

    [Header("Game Stats")]
    [SyncVar]
    public int health;
    [SyncVar]
    public int score;
    [SyncVar]
    public string playerName;
    [SyncVar]
    public bool allowMovement;
    [SyncVar]
    public bool isReady;

    [Header("Player Setting")]
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float lookSpeed = 2f;
    public Rigidbody rb;

    public bool isDead => health <= 0;
    public TextMesh nameText;

    void Update()
    {
        if (!isLocalPlayer) // Посылаем всех чужеродных нахрен от нас
            return;
        Move();

    }

    void Move()
    {
        //Move
        float xMov = Input.GetAxisRaw("Horizontal");
        float zMov = Input.GetAxisRaw("Vertical");

        Vector3 movHor = transform.right * xMov;
        Vector3 movVer = transform.forward * zMov;
        Vector3 velocity = (movHor + movVer).normalized * speed;

        if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }

        //Rotation
        float yRot = Input.GetAxisRaw("Mouse X");
        float xRot = Input.GetAxisRaw("Mouse Y");

        Vector3 rotation = new Vector3(0f, yRot, 0f) * lookSpeed;

        if (rotation != Vector3.zero)
        {
            rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        }
    }

    // Вызывается на сервере
    [Command]
    void CmdFire()
    {
        /*GameObject bullet = Instantiate(bulletPrefab, bulletMount.position, transform.rotation);
        bullet.GetComponent<BulletController>().source = gameObject;
        NetworkServer.Spawn(bullet);*/
    }



}

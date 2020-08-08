using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalCanvas : MonoBehaviour
{
    public Player playerScript;
    private Tank tank;
    private TankTurret TankTurret;

    [Header("Настройки орудийного прицела")]
    public GameObject GunAim;
    public float widthGunAim = 25f;
    public float heightGunAim = 25f;
    RectTransform rectGunAim;

    [Header("Image -> ХП, Резиста, Перезарядки")]
    public Image hpBar;
    public Image resistBar;
    public Image reloadBar;
    
    [Header("Text -> ХП, Резиста, Перезарядки")]
    public Text textReload;
    public Text textHp;
    public Text textResist;

    private float _maxTimeReload;
    private float _amountOfReloadTime;
    
    private float _timeLeft;

    private int _maxResist = 100;
    private float  _resistAmount;
    
    private float _maxHp;
    private float _hpAmount;
    
    private string resultOfReloadText;
    private string resultOfResistText;
    private string resultOfHpText;
    void Start()
    {

        /* Инициализация прицелов */
        //rectMainAim = MainAim.GetComponent<RectTransform>();
        rectGunAim = GunAim.GetComponent<RectTransform>();

        /*rectMainAim.sizeDelta = new Vector2(widthMainAim, heightMainAim);
        rectMainAim.anchorMin = new Vector2(0.5f, 0.5f);
        rectMainAim.anchorMax = new Vector2(0.5f, 0.5f);
        rectMainAim.pivot = new Vector2(0.5f, 0.5f);*/

        rectGunAim.sizeDelta = new Vector2(widthGunAim, heightGunAim);
        rectGunAim.anchorMin = new Vector2(0f, 0f);
        rectGunAim.anchorMax = new Vector2(0f, 0f);
        rectGunAim.pivot = new Vector2(0.5f, 0.5f);

        /* Конец */

        BodyParameter bodyParameter = transform.parent.GetComponentInChildren<BodyParameter>();
        TurretParameter turretParameter = transform.parent.GetComponentInChildren<TurretParameter>();

        tank = transform.parent.GetComponentInChildren<Tank>();
        TankTurret = transform.parent.GetComponentInChildren<TankTurret>();
        _amountOfReloadTime = turretParameter.reloadTime;
        _maxTimeReload = _amountOfReloadTime;
        
        //-------
        _resistAmount = tank.GetTankResist();
        resistBar.fillAmount = _maxResist;
        //-------
        _maxHp = bodyParameter.health;
        hpBar.fillAmount = _maxHp;
        _hpAmount = _maxHp;
    }
   
    void FixedUpdate()
    {
        RechargingBar();
        ResistBar();
        
        MoveTankAim();
    }

    void MoveTankAim()
    {
        Vector3 aim = TankTurret.GetGunAimOnScreen();
        rectGunAim.transform.position = aim;
    }
    void RechargingBar()
    {
        if (TankTurret.fire)
        {
            _maxTimeReload = 0f;
        }
        if(TankTurret.recharge)
        {
            _maxTimeReload += 1f* Time.deltaTime;
            reloadBar.fillAmount = _maxTimeReload / _amountOfReloadTime ;
        }
        resultOfReloadText =String.Format("{0:f}",_maxTimeReload );
        textReload.text = resultOfReloadText + " sec";
        if (textReload.text == "0,00 sec")
        {
            textReload.text = "Готов";
        }
    }

    void ResistBar()
    {
        resistBar.fillAmount =  _resistAmount / _maxResist;
        resultOfResistText = String.Format("{0:00}", _resistAmount);
        textResist.text = resultOfResistText;
    }

    public void HealthBar(float health)
    {
        hpBar.fillAmount = health;
    }
    
    
   
}



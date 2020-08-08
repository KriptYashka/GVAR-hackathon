using System;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.UI;

[RequireComponent(typeof(Player))]
public class PlayerInput : MonoBehaviour
{
    enum ChoiceControllers
    {
        WheelController ,
        TrackController 
    };

   // public TankTurret TankTurret;
    private BodyParameter bodyParameter;

    [Header("Переключатель механики езды")]
    [SerializeField]
    private ChoiceControllers _choice;
    [Space]
    public WheelControllers _wheelControllers;
    public TracksControllers _tracksController;

    void Start()
    {
        bodyParameter = GetComponentInChildren<BodyParameter>();
    }

    void Update()
    {
        if (1!=0)
        {
            
            // if (Input.GetMouseButtonDown(0) && TankTurret.fire)
            // {
            //     TankTurret.StartCoroutine("Shoot");
            // }

            if (_choice == ChoiceControllers.TrackController)
            {
                _tracksController.FixedUpdateTwo(Input.GetAxis("Ver"), Input.GetAxis("Hor"));
                _tracksController.CmdFixedUpdateTwo(Input.GetAxis("Ver"), Input.GetAxis("Hor"));
            }
            else if(_choice == ChoiceControllers.WheelController)
            {
                // float accel = 0;
                // float steer = 0;
                // accel = Input.GetAxis("Vertical");
                // steer = Input.GetAxis("Horizontal");
                // _wheelControllers.CmdFixedUpdateThree(accel, steer);
              
            }
        }
        else
        {
            
            _tracksController.FixedUpdateTwo(0, 0);
            _tracksController.CmdFixedUpdateTwo(0, 0);
            
            _wheelControllers.CmdFixedUpdateThree(0, 0);

        }
    }

}



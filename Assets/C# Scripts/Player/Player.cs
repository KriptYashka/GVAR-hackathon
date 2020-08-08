using System;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Player : MonoBehaviour
{
    public string nickname;
    public int dead = 0;
    public int killing = 0;

    [HideInInspector]
    public bool inBattle;
    public Collider[] colliders;

    [SerializeField] private GameObject[] disableGameObjectOnDeat;
    [SerializeField] private Behaviour[] disableOnDeath;

    public int teamId = -1;
    [SerializeField] private Color red;
    [SerializeField] private Color blue;

    [SerializeField] private MeshRenderer[] meshRenderersForColor;

    public void Dead()
    {
        dead++;
        Debug.Log("Уничтожен -> " + nickname);
    }
}

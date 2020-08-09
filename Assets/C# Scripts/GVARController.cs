using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GVARController : MonoBehaviour
{
    public GameObject TankZ4;
    public GameObject TankDizraptor;
    public GameObject TankSpider;
    public GameObject TankRail;

    private static int selectTank = 2;


    private void Start()
    {
        DontDestroyOnLoad(this);
    }
    public void SelectZ4()
    {
        selectTank = 0;
    }

    public void SelectDizraptor()
    {
        selectTank = 1;
    }

    public void SelectSpider()
    {
        selectTank = 2;
    }
    public void SelectRail()
    {
        selectTank = 3;
    }

    public void StartGame()
    {
        print(selectTank);
        if (selectTank == -1) return;
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

}

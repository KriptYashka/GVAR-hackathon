using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// скрипт не используетсся
public class ShowTanks : MonoBehaviour
{
    public GameObject[] tanks;
    private bool isTank=false;
    public Button[] imgTank;
    public GameObject SpawnPos;

    private void Start()
    {
        for (int i = 0; i < imgTank.Length; i++)
        {
            int closureIndex = i ; 
            imgTank[closureIndex].onClick .AddListener( () => TaskOnClick( closureIndex ) );
            
        }
    }

    public void TaskOnClick(int buttonIndex)
    {

        int a = -1;
      //  Debug.Log("You have clicked the button #" + buttonIndex, buttons[buttonIndex]);
        for (int i = 0; i < tanks.Length; i++)
        {
            a = i;
            if (a==buttonIndex)
            {
                Debug.Log("gb =>" + a);
                Debug.Log("buttonIndex =>" + buttonIndex);
                tanks[a].SetActive(true);
                



            }
            else
            {
                tanks[a].SetActive(false);
            }

        }
    }
 
}

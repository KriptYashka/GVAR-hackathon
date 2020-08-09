using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTanks : MonoBehaviour
{
   private List<Transform> models;

   private void Awake()
   {
      models = new List<Transform>();
      /*for (int i = 0; i < transform.ChildCount; i++)
      {
         Азат, не знаю, просто здесь было это и оно не работало
      }*/
   }
}

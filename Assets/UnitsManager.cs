using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitsManager : MonoBehaviour
{
   public GameObject[] Units; 
   public void Exit()
   {
        foreach (var unit in Units)
        {
            unit.SetActive(false);
        }
   }

    public void Open()
    {
        foreach (var unit in Units)
        {
            unit.SetActive(true);
        }
    }


}

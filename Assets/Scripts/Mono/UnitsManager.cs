using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UnitsManager : MonoBehaviour
{
    public GameObject Units; 
    public static UnitsManager instance;

    void Awake() => instance = this;

    public void Exit() =>  Units.SetActive(false);

    public void Open() =>  Units.SetActive(true);


}

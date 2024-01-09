using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedsFunctionality : MonoBehaviour
{
    public static LedsFunctionality instance;

    void Awake()
    {
        instance = this;
    }
        
    // Start is called before the first frame update
    void Start()
    {
        LedOff();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Turn the LED on
    public void LedOn()
    {
        gameObject.SetActive(true);
    }

    // Turn the LED off
    public void LedOff()
    {
        gameObject.SetActive(false);
    }
}

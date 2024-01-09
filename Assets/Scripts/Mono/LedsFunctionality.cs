using UnityEngine;

public class LedsFunctionality : MonoBehaviour
{
    public static LedsFunctionality instance;

    void Awake()
    {
        instance = this;
    }
        
    void Start()
    {
        LedOff();
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

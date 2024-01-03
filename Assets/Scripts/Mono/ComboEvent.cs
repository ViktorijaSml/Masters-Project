using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ComboEvent : MonoBehaviour
{
    public UnityEvent comboPressEvent;
    public static ComboEvent instance; 
    bool isAPressed = false, isBPressed=false;
    Button buttonA, buttonB;
    void Start()
    {
        instance = this;

         buttonA = GameObject.Find("M5 Button").GetComponent<Button>();
         buttonB = GameObject.Find("Right Button").GetComponent<Button>();
    }
    public void UpdatePressInfo() { 

        isAPressed = buttonA.GetComponent<EventsManager>().GetIsPressed();
        isBPressed = buttonB.GetComponent<EventsManager>().GetIsPressed();

        if (isAPressed && isBPressed)
        {
            comboPressEvent.Invoke();
        }
    }

  public void removeListeners()
    {
        EventsManager.instance.wasPressedEvent.RemoveAllListeners();
        EventsManager.instance.wasReleasedEvent.RemoveAllListeners();
        EventsManager.instance.wasDoublePressedEvent.RemoveAllListeners();
        EventsManager.instance.longPressEvent.RemoveAllListeners();
    }
  
}

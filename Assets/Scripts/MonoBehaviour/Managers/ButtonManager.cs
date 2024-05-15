using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using UBlockly;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public UnityEvent comboPressEvent;
    public List<Button> buttons;
    public static ButtonManager instance; 
    bool isAPressed = false, isBPressed=false;
    Button buttonA, buttonB;

    void Awake() => instance = this;

    void Start()
    {
        buttonA = buttons[0];
        buttonB = buttons[1];
    }

	private void Update()
	{
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = !UnityEngine.Input.GetKeyDown(KeyCode.Escape);
#else
        if (UnityEngine.Input.GetKeyDown(KeyCode.Escape) || UnityEngine.Input.GetKeyDown(KeyCode.Return))
        {
            Application.Quit();
        }
#endif
	}
	public void ResetScene() => SystemManager.RefreshApp();
    public void UpdatePressInfo() { 

        isAPressed = buttonA.GetComponent<EventsManager>().GetIsPressed();
        isBPressed = buttonB.GetComponent<EventsManager>().GetIsPressed();

        if (isAPressed && isBPressed)
        {
            comboPressEvent.Invoke();
        }
    }

    public Button GetButton(int index) => buttons[index];
 
    public void ClearAllListenersFromAllButtons() 
    {
        foreach(Button button in buttons)
        {
           ClearAllListeners(button.name);
        }
    }
    public void ClearAllListeners(string name)
    {
        EventsManager button = GameObject.Find(name).GetComponent<EventsManager>();
        List<UnityEvent> eventList = button.GetAllEvents();
        foreach (var myEvent in eventList)
        {
            myEvent.RemoveAllListeners();
        }
        comboPressEvent.RemoveAllListeners();
    }
}

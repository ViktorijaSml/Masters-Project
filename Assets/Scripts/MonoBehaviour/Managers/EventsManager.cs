using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class EventsManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public UnityEvent wasPressedEvent, wasReleasedEvent, longPressEvent, wasDoublePressedEvent;
    private bool isPressedFirstTime = false, isPressed = false;
    public bool eventSucces = false;
    private float pressTime = 0f, longPressDuration = 1f, doublePressDelay = 0.3f;
    public static EventsManager instance;
    private bool[] eventStates = {false, false, false, false}; //wasPressed, wasReleased, wasLongPress, wasDoublePress

    private void Awake() => instance = this;

	public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
        if (isPressedFirstTime)
        {
            if (Time.time - pressTime < doublePressDelay)
            {
                isPressedFirstTime = false;
                wasDoublePressedEvent.Invoke();
                eventStates[3] = true;
            }
        }
        else
        {
            isPressedFirstTime = true;
        }
        pressTime = Time.time;
        wasPressedEvent.Invoke();
        eventStates[0] = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        wasReleasedEvent.Invoke();
        eventStates[1] = true;

        isPressed = false;
        if (Time.time - pressTime > longPressDuration )
        {
            longPressEvent.Invoke();
            eventStates[2] = true;
        }
    }
 
    public void CheckEventSucces()
    {
        eventSucces = true;
    }
    public void UnCheckEventSucces()
    {
        eventSucces = false;
    }
    public void ResetEventStates()
    {
        for (int i = 0; i < eventStates.Length; i++)
        {
            eventStates[i] = false;
        }
    }
    public bool GetEventState(string eventName)
    {
        switch (eventName)
        {
            case "WAS_PRESSED":
                return eventStates[0];
            case "WAS_RELEASED":
                return eventStates[1];
            case "LONG_PRESS":
                return eventStates[2];
            case "DOUBLE_PRESS":
                return eventStates[3];
        }

        throw new System.Exception("Argument for this method must be one of the following: WAS_PRESSED, LONG_PRESS, DOUBLE_PRESS, WAS_RELEASED!");
    }
    public bool GetIsPressed() => isPressed;
    public bool GetIsReleased() => !isPressed;
    public List<UnityEvent> GetAllEvents() => new List<UnityEvent>()
                                                {
                                                    wasPressedEvent,
                                                    wasDoublePressedEvent,
                                                    wasReleasedEvent,
                                                    longPressEvent
                                                };
}
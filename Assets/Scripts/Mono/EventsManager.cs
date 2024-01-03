using System.Collections;
using UBlockly;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class EventsManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public UnityEvent wasPressedEvent, wasReleasedEvent, longPressEvent, wasDoublePressedEvent;
    private bool isPressedFirstTime = false, isPressed = false, isReleased = false;
    private float pressTime = 0f, longPressDuration = 1f, doublePressDelay = 0.3f;
    public static EventsManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        wasPressedEvent.AddListener(ComboEvent.instance.UpdatePressInfo);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
        isReleased = false;

        if (isPressedFirstTime)
        {
            if (Time.time - pressTime < doublePressDelay)
            {
                isPressedFirstTime = false;
                wasDoublePressedEvent.Invoke();
            }
        }
        else
        {
            isPressedFirstTime = true;
        }
        pressTime = Time.time;
        wasPressedEvent.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        wasReleasedEvent.Invoke();

        isReleased = true;
        isPressed = false;

        if (Time.time - pressTime > longPressDuration )
        {
            longPressEvent.Invoke();
        }
    }
    
    public bool GetIsPressed() => isPressed;
    public bool GetIsReleased() => isReleased;
    public IEnumerator RunBlocks  (Block block)
    {
        yield return CSharp.Interpreter.StatementRun(block, "DO");
    }
    public void WrapFunct(Block block)
    {
        StartCoroutine(RunBlocks(block));
    }
}
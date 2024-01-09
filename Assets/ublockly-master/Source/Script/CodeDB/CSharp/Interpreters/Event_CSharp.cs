using JetBrains.Annotations;
using System.Collections;
using UnityEngine;

namespace UBlockly
{
    [CodeInterpreter(BlockType = "event_buttonPress")]
    public class Event_ButtonPress_Cmdtor : EnumeratorCmdtor
    {
        protected override IEnumerator Execute(Block block)
        {
            string button = block.GetFieldValue("BUTTON");
            string condition = block.GetFieldValue("CONDITION");

            switch (button)
            {
                case "BUTTON_A":
                    button = "M5 Button";
                    break;
                case "BUTTON_B":
                    button = "Right Button";
                    break;
            }

            EventsManager eventsList = GameObject.Find(button).GetComponent<EventsManager>();

			switch (condition)
            {
                case "WAS_PRESSED":
                    eventsList.wasPressedEvent.AddListener(()=>EventsManager.instance.WrapFunct(block));
                    break;
                case "LONG_PRESS":
                    eventsList.longPressEvent.AddListener(() => EventsManager.instance.WrapFunct(block));
                    break;
                case "DOUBLE_PRESS":
                    eventsList.wasDoublePressedEvent.AddListener(() => EventsManager.instance.WrapFunct(block));
                    break;
                case "WAS_RELEASED":
                    eventsList.wasReleasedEvent.AddListener(() => EventsManager.instance.WrapFunct(block));
                    break;
            }

            yield return null;

        }

	}

    [CodeInterpreter(BlockType = "event_buttonComboPress")]
    public class Event_ButtonComboPress_Cmdtor : EnumeratorCmdtor
    {
        protected override IEnumerator Execute(Block block)
        { 
            ButtonManager.instance.comboPressEvent.RemoveAllListeners();
            EventsManager.instance.wasPressedEvent.RemoveAllListeners();
            EventsManager.instance.wasPressedEvent.AddListener(ButtonManager.instance.UpdatePressInfo);
            ButtonManager.instance.comboPressEvent.AddListener(() => EventsManager.instance.WrapFunct(block));
            yield return null;

        }
    }

    [CodeInterpreter(BlockType = "event_getPresentCondition")]
    public class Event_GetPresentCondition_Cmdtor : ValueCmdtor
    {
        protected override DataStruct Execute(Block block)
        {
            string condition = block.GetFieldValue("CONDITION");
            string button = block.GetFieldValue("BUTTON");
            bool value = false;
  
            switch (button)
            {
                case "BUTTON_A":
                    button = "M5 Button";
                    break;
                case "BUTTON_B":
                    button = "Right Button";
                    break;
            }
            EventsManager eventsList = GameObject.Find(button).GetComponent<EventsManager>();
            eventsList.wasPressedEvent.RemoveAllListeners();
            eventsList.wasPressedEvent.AddListener(() => Debug.Log("Listening..."));

            switch (condition)
            {
                case "IS_PRESSED":
                    value = eventsList.GetIsPressed();
                    break;
                case "IS_RELEASED":
                    value = eventsList.GetIsReleased();
                    break;
            }
            Debug.Log(value);
            Debug.Log(eventsList.GetIsPressed());
            return new DataStruct(value);
        }
    }

}

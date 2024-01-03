using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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
			GameObject.Find("M5 Button").GetComponent<Button>().onClick.RemoveAllListeners();
			GameObject.Find("Right Button").GetComponent<Button>().onClick.RemoveAllListeners();

			switch (condition)
            {
                case "WAS_PRESSED":
                    Debug.Log("dodao");
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
}

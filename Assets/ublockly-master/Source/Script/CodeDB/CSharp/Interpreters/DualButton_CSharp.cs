using System.Collections;
using UnityEngine;

namespace UBlockly
{
    [CodeInterpreter(BlockType = "dualbutton_buttonPress")]
    public class DualButton_ButtonPress_Cmdtor : EnumeratorCmdtor
    {
        protected override IEnumerator Execute(Block block)
        {
            string button = block.GetFieldValue("BUTTON");
            string condition = block.GetFieldValue("CONDITION");

            switch (button)
            {
                case "BUTTON_RED":
                    button = "Red Button";
                    break;
                case "BUTTON_BLUE":
                    button = "Blue Button";
                    break;
            }

            EventsManager eventsList = GameObject.Find(button).GetComponent<EventsManager>();

            switch (condition)
            {
                case "WAS_PRESSED":
                    eventsList.wasPressedEvent.AddListener(() => eventsList.CheckEventSucces());
                    break;
                case "LONG_PRESS":
                    eventsList.longPressEvent.AddListener(() => eventsList.CheckEventSucces());
                    break;
                case "DOUBLE_PRESS":
                    eventsList.wasDoublePressedEvent.AddListener(() => eventsList.CheckEventSucces());
                    break;
                case "WAS_RELEASED":
                    eventsList.wasReleasedEvent.AddListener(() => eventsList.CheckEventSucces());
                    break;
            }

            while (true)
            {
                if (eventsList.eventSucces)
                {
                    Debug.Log(button + " is pressed!");
                    yield return CSharp.Interpreter.StatementRun(block, "DO");
                    eventsList.UnCheckEventSucces();
                }
                yield return new WaitForSeconds(1 / 60);
            }
        }
    }


    [CodeInterpreter(BlockType = "dualbutton_getPastCondition")]
    public class DualButton_GetPastCondition_Cmdtor : ValueCmdtor
    {
        protected override DataStruct Execute(Block block)
        {
            string condition = block.GetFieldValue("CONDITION");
            string button = block.GetFieldValue("BUTTON");

            switch (button)
            {
                case "BUTTON_RED":
                    button = "Red Button";
                    break;
                case "BUTTON_BLUE":
                    button = "Blue Button";
                    break;
            }

            EventsManager eventsList = GameObject.Find(button).GetComponent<EventsManager>();

            bool value = eventsList.GetEventState(condition);
            eventsList.ResetEventStates();

            return new DataStruct(value);
        }
    }
}
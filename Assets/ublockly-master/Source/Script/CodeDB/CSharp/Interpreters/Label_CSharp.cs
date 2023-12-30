using System.Collections;
using TMPro;
using UnityEngine;

namespace UBlockly
{
    [CodeInterpreter(BlockType = "label_setColor")]
    public class Label_SetColor_Cmdtor : VoidCmdtor
    {
        protected override void Execute(Block block)
        {
            string value = block.GetFieldValue("COLOR");
            string label = block.GetFieldValue("LABEL");
            Color color = new Color();

            switch (value)
            {
                case "BLACK":
                    color = LabelManager.instance.SetColorBlack();
                    break;
                case "RED":
                    color = LabelManager.instance.SetColorRed();
                    break;
                case "BLUE":
                    color = LabelManager.instance.SetColorBlue();
                    break;
                case "YELLOW":
                    color = LabelManager.instance.SetColorYellow();
                    break;
                case "GREEN":
                    color = LabelManager.instance.SetColorGreen();
                    break;
                case "PURPLE":
                    color = LabelManager.instance.SetColorPurple();
                    break;
                case "WHITE":
                    color = LabelManager.instance.SetColorWhite();
                    break;
            }
            LabelManager.instance.SetLabelColor(label, color);
        }
    }

    [CodeInterpreter(BlockType = "label_setColorRGB")]
    public class Label_SetColorRGB_Cmdtor : VoidCmdtor
    {
        protected override void Execute(Block block)
        {
            string label = block.GetFieldValue("LABEL");
            Number colorRed = new Number(block.GetFieldValue("RGB_RED"));
            Number colorGreen = new Number(block.GetFieldValue("RGB_GREEN"));
            Number colorBlue = new Number(block.GetFieldValue("RGB_BLUE"));

            LabelManager.instance.SetLabelColorByRGB(label, colorRed.Value, colorGreen.Value, colorBlue.Value);
        }

    }

    [CodeInterpreter(BlockType = "label_showString")]
    public class Label_ShowString_Cmdtor : VoidCmdtor { 
        protected override void Execute(Block block)
        {
            string label = block.GetFieldValue("LABEL");
            string tekst = block.GetFieldValue("TEXT");

            LabelManager.instance.WriteText(label, tekst);
        }
    }

    [CodeInterpreter(BlockType = "label_showVariable")]
    public class Label_ShowVariable_Cmdtor : EnumeratorCmdtor
    {
        protected override IEnumerator Execute(Block block)
        {
            string label = block.GetFieldValue("LABEL");
            CmdEnumerator ctor = CSharp.Interpreter.ValueReturn(block, "TEXT", new DataStruct(0));
            yield return ctor;
            string tekst = ctor.Data.StringValue;

            LabelManager.instance.WriteText(label, tekst);


        }
    }

    [CodeInterpreter(BlockType = "label_hide")]
    public class Label_Hide_Cmdtor : VoidCmdtor
    {
        protected override void Execute(Block block)
        {
            string label = block.GetFieldValue("LABEL");
            string check = block.GetFieldValue("CHECK");
            bool checkValue = check == "TRUE" ? true: false;

            LabelManager.instance.HideLabel(checkValue, label);
        }
    }
}
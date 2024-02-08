using System.Collections;

namespace UBlockly
{
    [CodeInterpreter(BlockType = "screen_backgroundColor")]
    public class Screen_BackgroundColor_Cmdtor : VoidCmdtor
    {
        protected override void Execute(Block block)
        {
            string value = block.GetFieldValue("COLOR");

            switch (value)
            {
                case "BLACK":
                    ScreenManager.instance.SetColorBlack();
                    break;
                case "RED":
                    ScreenManager.instance.SetColorRed();
                    break;
                case "BLUE":
                    ScreenManager.instance.SetColorBlue();
                    break;
                case "YELLOW":
                    ScreenManager.instance.SetColorYellow();
                    break;
                case "GREEN":
                    ScreenManager.instance.SetColorGreen();
                    break;
                case "PURPLE":
                    ScreenManager.instance.SetColorPurple();
                    break;
                case "WHITE":
                    ScreenManager.instance.SetColorWhite();
                    break;
            }
        }
    }

    [CodeInterpreter(BlockType = "screen_backgroundBrightness")]
    public class Screen_BackgroundBrightness_Cmdtor : EnumeratorCmdtor
    {
        protected override IEnumerator Execute(Block block)
        {
            CmdEnumerator ctor = CSharp.Interpreter.ValueReturn(block, "BRIGHTNESS", new DataStruct(0));
            yield return ctor;
            float value = ctor.Data.NumberValue.Value;

            ScreenManager.instance.SetBrigthness(value);
        }
    }

    [CodeInterpreter(BlockType = "screen_setColorRGB")]
    public class Screen_BackgroundColorRGB_Cmdtor : VoidCmdtor
    {
        protected override void Execute(Block block)
        {
            Number colorRed = new Number(block.GetFieldValue("RGB_RED"));
            Number colorGreen = new Number(block.GetFieldValue("RGB_GREEN"));
            Number colorBlue = new Number(block.GetFieldValue("RGB_BLUE"));

            ScreenManager.instance.SetColorRGB(colorRed.Value, colorGreen.Value, colorBlue.Value);
            }
    }
}
using System.Collections;


namespace UBlockly
{
    [CodeInterpreter(BlockType = "screen_backgroundColor")]
    public class Screen_BackgroundColor_Cmdtor : VoidCmdtor
    {
        protected override void Execute(Block block)
        {
            var value = block.GetFieldValue("COLOR");

            switch (value)
            {
                case "BLACK":
                    ScreenColor.instance.SetColorBlack();
                    break;
                case "RED":
                    ScreenColor.instance.SetColorRed();
                    break;
                case "BLUE":
                    ScreenColor.instance.SetColorBlue();
                    break;
                case "YELLOW":
                    ScreenColor.instance.SetColorYellow();
                    break;
                case "GREEN":
                    ScreenColor.instance.SetColorGreen();
                    break;
                case "PURPLE":
                    ScreenColor.instance.SetColorPurple();
                    break;
                case "WHITE":
                    ScreenColor.instance.SetColorWhite();
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

            ScreenColor.instance.SetBrigthness(value);
        }
    }

    [CodeInterpreter(BlockType = "screen_setColorRGB")]
    public class Screen_BackgroundColorRGB_Cmdtor : EnumeratorCmdtor
    {
        protected override IEnumerator Execute(Block block)
        {
            CmdEnumerator ctor = CSharp.Interpreter.ValueReturn(block, "RGB_RED", new DataStruct(0));
            yield return ctor;
            float colorRed = ctor.Data.NumberValue.Value;

            CmdEnumerator ctor2 = CSharp.Interpreter.ValueReturn(block, "RGB_GREEN", new DataStruct(0));
            yield return ctor2;
            float colorGreen = ctor2.Data.NumberValue.Value;

            CmdEnumerator ctor3 = CSharp.Interpreter.ValueReturn(block, "RGB_BLUE", new DataStruct(0));
            yield return ctor3;
            float colorBlue = ctor3.Data.NumberValue.Value;

            ScreenColor.instance.SetColorRGB(colorRed, colorGreen, colorBlue);
            }

    }
}
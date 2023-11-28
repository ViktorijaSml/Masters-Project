using System;
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
            float value = Math.Clamp(ctor.Data.NumberValue.Value / 255f, 0f, 1f);

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
            float colorRed = Math.Clamp(ctor.Data.NumberValue.Value / 255f, 0f, 1f );

            CmdEnumerator ctor2 = CSharp.Interpreter.ValueReturn(block, "RGB_GREEN", new DataStruct(0));
            yield return ctor2;
            float colorGreen = Math.Clamp(ctor2.Data.NumberValue.Value / 255f, 0f, 1f);

            CmdEnumerator ctor3 = CSharp.Interpreter.ValueReturn(block, "RGB_BLUE", new DataStruct(0));
            yield return ctor3;
            float colorBlue = Math.Clamp(ctor3.Data.NumberValue.Value / 255f, 0f, 1f);

            ScreenColor.instance.SetColorRGB(colorRed, colorGreen, colorBlue);
            }

    }
}
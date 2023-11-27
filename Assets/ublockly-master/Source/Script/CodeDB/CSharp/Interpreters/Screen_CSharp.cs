using System.Diagnostics;
using UnityEngine;

namespace UBlockly
{
    // This next block of code is for one block, if the 
    //category has multiple blocks repeat this for each one
    [CodeInterpreter(BlockType = "screen_backgroundColor")]
    public class Screen_BackgroundColor_Cmdtor : VoidCmdtor
    {
        protected override void Execute(Block block)
        {
            var value = block.GetFieldValue("COLOR");

            switch (value)
            {
                case "BLACK":
                    ScreenColor.instance.ChangeToBlack();
                    break;
                case "RED":
                    ScreenColor.instance.ChangeToRed();
                    break;
                case "BLUE":
                    ScreenColor.instance.ChangeToBlue();
                    break;
                case "YELLOW":
                    ScreenColor.instance.ChangeToYellow();
                    break;
                case "GREEN":
                    ScreenColor.instance.ChangeToGreen();
                    break;
                case "PURPLE":
                    ScreenColor.instance.ChangeToPurple();
                    break;
                case "WHITE":
                    ScreenColor.instance.ChangeToWhite();
                    break;
            }
        }

       
    }
}
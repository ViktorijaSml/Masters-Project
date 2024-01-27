namespace UBlockly
{
    [CodeInterpreter(BlockType = "joystick_getXPosition")]
    public class Joystick_GetXPosition_Cmdtor : ValueCmdtor
    {
        protected override DataStruct Execute(Block block)
        {
            return new DataStruct(JoystickManager.instance.GetXPosition());
        }
    }

    [CodeInterpreter(BlockType = "joystick_getYPosition")]
    public class Joystick_GetYPosition_Cmdtor : ValueCmdtor
    {
        protected override DataStruct Execute(Block block)
        {
            return new DataStruct(JoystickManager.instance.GetYPosition());
        }
    }

    [CodeInterpreter(BlockType = "joystick_getReverseXPosition")]
    public class Joystick_GetReverseXPosition_Cmdtor : ValueCmdtor
    {
        protected override DataStruct Execute(Block block)
        {
            return new DataStruct(JoystickManager.instance.GetXReversedPosition());
        }
    }

    [CodeInterpreter(BlockType = "joystick_getReverseYPosition")]
    public class Joystick_GetReverseYPosition_Cmdtor : ValueCmdtor
    {
        protected override DataStruct Execute(Block block)
        {
            return new DataStruct(JoystickManager.instance.GetYReversedPosition());
        }
    }

    [CodeInterpreter(BlockType = "joystick_getIsPressed")]
    public class Joystick_GetIsPressed_Cmdtor : ValueCmdtor
    {
        protected override DataStruct Execute(Block block)
        {
            bool value = JoystickManager.instance.IsPressed;
            JoystickManager.instance.IsPressed = false;
            return new DataStruct(value);
        }
    }
}

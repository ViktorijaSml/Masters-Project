namespace UBlockly
{
    [CodeInterpreter(BlockType = "leds_TurnOn")]
    public class Leds_LEDOn_Cmdtor : VoidCmdtor
    {
        protected override void Execute(Block block)
        {
            LedManager.instance.LedOn();
        }
    }

    [CodeInterpreter(BlockType = "leds_TurnOff")]
    public class Leds_LEDOff_Cmdtor : VoidCmdtor
    {
        protected override void Execute(Block block)
        {
            LedManager.instance.LedOff();
        }
    }
}

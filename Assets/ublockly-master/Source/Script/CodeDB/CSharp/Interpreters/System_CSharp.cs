using UnityEngine;

namespace UBlockly
{
    [CodeInterpreter(BlockType = "system_reset")]
    public class System_Reset_Cmdtor : VoidCmdtor
    {
        protected override void Execute(Block block)
        {
            Debug.Log("System reset..");
            SystemManager.SystemReset();
        }
    }
}

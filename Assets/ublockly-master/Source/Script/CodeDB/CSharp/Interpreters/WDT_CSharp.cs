using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace UBlockly
{
    [CodeInterpreter(BlockType = "wdt_initWDT")]
    public class WDT_InitWDT_Cmdtor : VoidCmdtor
    {
        protected override void Execute(Block block)
        {
            Number timeout = new Number(block.GetFieldValue("TIMEOUT"));
            UnityEngine.Debug.Log("Init WDT");

            TimerFunctions.instance.InitWatchDogTimer((int)timeout.Value);
        }
    }

    [CodeInterpreter(BlockType = "wdt_feedWDT")]
    public class WDT_FeedWDT_Cmdtor : VoidCmdtor
    {
        protected override void Execute(Block block)
        {
			Number timeout = new Number(block.GetFieldValue("TIMEOUT"));
			TimerFunctions.instance.FeedWatchdogTimer();
			UnityEngine.Debug.Log("Reset");
        }
    }


    [CodeInterpreter(BlockType = "wdt_wait")]
    public class WDT_Wait_Cmdtor : VoidCmdtor
    {
        private int timer = 0;
		protected override async void Execute(Block block)
		{
			Number timeout = new Number(block.GetFieldValue("TIMEOUT"));
			timer += (int)timeout.Value;
			await Task.Delay(timer);
		}
	}

}
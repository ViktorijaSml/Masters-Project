using System.Collections;

namespace UBlockly
{
    [CodeInterpreter(BlockType = "rtc_setDate")]
    public class RTC_SetDate_Cmdtor : EnumeratorCmdtor
    {
        protected override IEnumerator Execute(Block block)
        {
            CmdEnumerator ctor = CSharp.Interpreter.ValueReturn(block, "YEAR", new DataStruct(0));
            yield return ctor;
            int year = (int)ctor.Data.NumberValue.Value;

            CmdEnumerator ctor2 = CSharp.Interpreter.ValueReturn(block, "MONTH", new DataStruct(0));
            yield return ctor2;
            int month = (int)ctor2.Data.NumberValue.Value;

            CmdEnumerator ctor3 = CSharp.Interpreter.ValueReturn(block, "DAY", new DataStruct(0));
            yield return ctor3;
            int day = (int)ctor3.Data.NumberValue.Value;

            CmdEnumerator ctor4 = CSharp.Interpreter.ValueReturn(block, "HOUR", new DataStruct(0));
            yield return ctor4;
            int hour = (int)ctor4.Data.NumberValue.Value;

            CmdEnumerator ctor5 = CSharp.Interpreter.ValueReturn(block, "MINUTE", new DataStruct(0));
            yield return ctor5;
            int minute = (int)ctor5.Data.NumberValue.Value;

            CmdEnumerator ctor6 = CSharp.Interpreter.ValueReturn(block, "SECOND", new DataStruct(0));
            yield return ctor6;
            int second = (int)ctor6.Data.NumberValue.Value;

            RTCFunctions.SetDateTime(year, month, day, hour, minute, second);
        }
    }

    [CodeInterpreter(BlockType = "rtc_getYear")]
    public class RTC_GetYear_Cmdtor : ValueCmdtor
    {
        protected override DataStruct Execute(Block block)
        {
            Number num = new Number(RTCFunctions.GetYear());
            return new DataStruct(num);
        }
    }

    [CodeInterpreter(BlockType = "rtc_getMonth")]
    public class RTC_GetMonth_Cmdtor : ValueCmdtor
    {
        protected override DataStruct Execute(Block block)
        {
            Number num = new Number(RTCFunctions.GetMonth());
            return new DataStruct(num);
        }
    }

    [CodeInterpreter(BlockType = "rtc_getDay")]
    public class RTC_GetDay_Cmdtor : ValueCmdtor
    {
        protected override DataStruct Execute(Block block)
        {
            Number num = new Number(RTCFunctions.GetDay());
            return new DataStruct(num);
        }
    }

    [CodeInterpreter(BlockType = "rtc_getHour")]
    public class RTC_GetHour_Cmdtor : ValueCmdtor
    {
        protected override DataStruct Execute(Block block)
        {
            Number num = new Number(RTCFunctions.GetHour());
            return new DataStruct(num);
        }
    }

    [CodeInterpreter(BlockType = "rtc_getMinute")]
    public class RTC_GetMinute_Cmdtor : ValueCmdtor
    {
        protected override DataStruct Execute(Block block)
        {

            Number num = new Number(RTCFunctions.GetMinute());
            return new DataStruct(num);
        }
    }

    [CodeInterpreter(BlockType = "rtc_getSecond")]
    public class RTC_GetSecond_Cmdtor : ValueCmdtor
    {
        protected override DataStruct Execute(Block block)
        {
            Number num = new Number(RTCFunctions.GetSecond());
            return new DataStruct(num);
        }
    }

    [CodeInterpreter(BlockType = "rtc_getLocalTime")]
    public class RTC_GetLocalTime_Cmdtor : ValueCmdtor
    {
        protected override DataStruct Execute(Block block)
        {
            ArrayList num = new ArrayList(RTCFunctions.GetLocalTime());
            return new DataStruct(num);
        }
    }
}

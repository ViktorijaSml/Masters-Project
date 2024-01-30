namespace UBlockly
{
    [CodeInterpreter(BlockType = "envIV_getPressure")]
    public class EnvIV_GetPressure_Cmdtor : ValueCmdtor
    {
        protected override DataStruct Execute(Block block)
        {
            return new DataStruct(Env4Manager.instance.Pressure);
        }
    }
    [CodeInterpreter(BlockType = "envIV_getTemperature")]
    public class EnvIV_GetTemperature_Cmdtor : ValueCmdtor
    {
        protected override DataStruct Execute(Block block)
        {
            return new DataStruct(Env4Manager.instance.Temperature);
        }
    }
    [CodeInterpreter(BlockType = "envIV_getHumidity")]
    public class EnvIV_GetHumidity_Cmdtor : ValueCmdtor
    {
        protected override DataStruct Execute(Block block)
        {
            return new DataStruct(Env4Manager.instance.Humidity);
        }
    }
}
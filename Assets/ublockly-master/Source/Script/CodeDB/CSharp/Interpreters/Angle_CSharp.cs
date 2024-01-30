namespace UBlockly
{
    [CodeInterpreter(BlockType = "angle_getAngle")]
    public class Angle_GetValue_Cmdtor : ValueCmdtor
    {
        protected override DataStruct Execute(Block block)
        {
            return new DataStruct(AngleManager.instance.GetAngle());
        }
    }
}
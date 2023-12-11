using System.Collections;
using UBlockly;
using UnityEngine;

public class Speaker_CSharp : MonoBehaviour
{
    [CodeInterpreter(BlockType = "speaker_setVolume")]
    public class Speaker_Volume_Cmdtor : VoidCmdtor
    {
        protected override void Execute(Block block)
        {
            string value = block.GetFieldValue("VOLUME");
            Number num = new Number(value);
            SpeakerManager.instance.SetVolume(num.Value);
        }
    }

    [CodeInterpreter(BlockType = "speaker_playTone")]
    public class Speaker_PlayTone_Cmdtor : VoidCmdtor
    {
        protected override void Execute(Block block)
        {
            string tone = block.GetFieldValue("TONE");
            string beat = block.GetFieldValue("BEAT");
            Debug.Log(tone);
            Debug.Log(beat);
            switch (tone)
            {
                case "C":
                    SpeakerManager.instance.PlayKeyC();
                    break;
                case "CSHARP":
                    SpeakerManager.instance.PlayKeyCsharp();
                    break;
                case "D":
                    SpeakerManager.instance.PlayKeyD();
                    break;
                case "DSHARP":
                    SpeakerManager.instance.PlayKeyDsharp();
                    break;
                case "E":
                    SpeakerManager.instance.PlayKeyE();
                    break;
                case "F":
                    SpeakerManager.instance.PlayKeyF();
                    break;
                case "FSHARP":
                    SpeakerManager.instance.PlayKeyFsharp();
                    break;
                case "G":
                    SpeakerManager.instance.PlayKeyG();
                    break;
                case "GSHARP":
                    SpeakerManager.instance.PlayKeyGsharp();
                    break;
                case "A":
                    SpeakerManager.instance.PlayKeyA();
                    break;
                case "ASHARP":
                    SpeakerManager.instance.PlayKeyAsharp();
                    break;
                case "H":
                    SpeakerManager.instance.PlayKeyH();
                    break;
                case "CTWO":
                    SpeakerManager.instance.PlayKeyC2();
                    break;
            }

            float beat_num = 0;
            switch (beat)
            {
                case "ONE_BEAT":
                    beat_num = 1f;
                    break;
                case "HALF_BEAT":
                    beat_num = 0.5f;
                    break;
                case "FOURTH_BEAT":
                    beat_num = 0.25f;
                    break;
                case "TWO_BEAT":
                    beat_num = 2f;
                    break;
                case "FOUR_BEAT":
                    beat_num = 4f;
                    break;
            }

            SpeakerManager.instance.PlayAudio(beat_num);
        }
    }

    [CodeInterpreter(BlockType = "speaker_playBeep")]
    public class Speaker_Beep_Cmdtor : VoidCmdtor
    {
        protected override void Execute(Block block)
        {
            Number freq = new Number(block.GetFieldValue("FREQUENCY"));
            Number time = new Number(block.GetFieldValue("DURATION"));
            SpeakerManager.instance.PlayBeep(freq.Value, ((int)time.Value));
        }
    }

}

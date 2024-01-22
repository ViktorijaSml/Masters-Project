using UnityEngine;

public class SpeakerManager : MonoBehaviour
{
    public AudioClip[] audioClips;
    private AudioSource audioSource;
    public static SpeakerManager instance;

    public void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        instance = this;
    }
    public void ResetAudioClips() => audioSource.clip = null;
    private int position = 0;
    private int samplerate = 44100; //samples per second
    private float freq = 0;
    public void PlayBeep(float frequency, int timeMilisec)
    {
        freq = frequency;
        int samples = (int)(timeMilisec * 44.1f);
        AudioClip myClip = AudioClip.Create("MySinusoid", samples, 1, samplerate, true, OnAudioRead, OnAudioSetPosition);
        AudioSource aud = GetComponent<AudioSource>();
        aud.clip = myClip;
        aud.Play();
    }
    private void OnAudioRead(float[] data)
    {
        int count = 0;
        while (count < data.Length)
        {
            data[count] = Mathf.Sin(2 * Mathf.PI * freq * position / samplerate);
            position++;
            count++;
        }
    }
    private void OnAudioSetPosition(int newPosition)
    {
        position = newPosition;
    }

    public void SetVolume (float volume)
    {
        audioSource.volume = volume;
    }

    //ideja je da odvojimo pokretanje audioclipa od odredivanja koji audioclip se mora pokrenuti
    //iz razloga sto se pokretanje audioclipa ponavlja 
    private void StopAudio()
    {
        audioSource.Stop();
    }
    public void PlayAudio(float beat)
    {
        audioSource.PlayOneShot(audioSource.clip);
        Invoke("StopAudio", beat);
    }
    //1 beat = 1 second 
    public void PlayKeyC() => audioSource.clip = audioClips[0];
    public void PlayKeyCsharp() => audioSource.clip = audioClips[1];
    public void PlayKeyD() => audioSource.clip = audioClips[2];
    public void PlayKeyDsharp() => audioSource.clip = audioClips[3];
    public void PlayKeyE() => audioSource.clip = audioClips[4];
    public void PlayKeyF() => audioSource.clip = audioClips[5];
    public void PlayKeyFsharp() => audioSource.clip = audioClips[6];
    public void PlayKeyG() => audioSource.clip = audioClips[7];
    public void PlayKeyGsharp() => audioSource.clip = audioClips[8];
    public void PlayKeyA() => audioSource.clip = audioClips[9];
    public void PlayKeyAsharp() => audioSource.clip = audioClips[10];
    public void PlayKeyH() => audioSource.clip = audioClips[11];
    public void PlayKeyC2() => audioSource.clip = audioClips[12];
   }

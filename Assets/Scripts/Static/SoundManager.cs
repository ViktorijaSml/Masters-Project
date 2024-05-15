using UnityEngine;

public static class SoundManager
{
    public static void PlaySound(SoundName soundName, float pitch = 1.0f)
    {
        AudioClip soundClip = Resources.Load<AudioClip>($"Sound Effects/{soundName.ToString()}");

        if(soundClip == null )
        {
            Debug.LogError($"Sound clip with name {soundName.ToString()} not found in Resources folder.");
            return;
        }

        GameObject soundGameObject = new GameObject("SoundEffect");
        AudioSource soundSource = soundGameObject.AddComponent<AudioSource>();  
        soundSource.clip = soundClip;
        soundSource.pitch = pitch;
        soundSource.Play();

        Object.Destroy(soundGameObject, soundClip.length);
    }
}

public enum SoundName
{
    BlockConnect,
    ButtonPressSimulator,
    ButtonPressUI,
    BlockDragNDrop,
    CategoryPress
}

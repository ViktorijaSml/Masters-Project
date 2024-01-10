using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ToggleLight : MonoBehaviour
{
    public static ToggleLight instance;
    public bool isOn = false;
    public Color LightOn = new Color(1, 1, 1, 1);
    public Color LightOff = new Color(1, 1, 1, 1);
    private Image lightImage;

    void Awake() => instance = this;

    void Start() 
    {
        lightImage = GetComponent<Image>();
        StartCoroutine(OnValueChange());
    }

    public void Light() 
    {
        isOn = !isOn;
        lightImage.color = isOn ? LightOn : LightOff;
    }

    public void SetLightOn(bool value) 
    {
        isOn = value;
        lightImage.color = isOn ? LightOn : LightOff;
    }

    public IEnumerator OnValueChange() 
    {
        while(true) {
            yield return new WaitForSeconds(0.1f);
            lightImage.color = isOn ? LightOn : LightOff;
        }
    }
}

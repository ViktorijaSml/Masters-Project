using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleLight : MonoBehaviour
{
    public bool isOn = false;
    public Color LightOn = new Color(1, 1, 1, 1);
    public Color LightOff = new Color(1, 1, 1, 1);
    private Image light;

    void Start() {
        light = GetComponent<Image>();
        StartCoroutine(OnValueChange());
    }

    public void Light()
    {
        isOn = !isOn;
        light.color = isOn ? LightOn : LightOff;
    }

    public IEnumerator OnValueChange()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.1f);
            light.color = isOn ? LightOn : LightOff;
        }
    }
}

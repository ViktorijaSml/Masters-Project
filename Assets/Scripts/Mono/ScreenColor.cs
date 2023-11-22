using UnityEngine;
using UnityEngine.UI;

public class ScreenColor : MonoBehaviour
{
    private Image imageScreen;
    void Start() => imageScreen = GetComponent<Image>();
    //Testiranje "ekrana": mijenjaj boje
    public void RandomColor() =>  imageScreen.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1);
}
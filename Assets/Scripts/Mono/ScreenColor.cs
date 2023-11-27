using UnityEngine;
using UnityEngine.UI;

public class ScreenColor : MonoBehaviour
{
    public static ScreenColor instance;
    private Image imageScreen;
    void Awake()
    {
        instance = this;
        imageScreen = GetComponent<Image>();
    }
   
    public void ChangeToBlack() => imageScreen.color = new Color(0, 0, 0);

    public void ChangeToRed() => imageScreen.color = new Color(255, 0, 0);

    public void ChangeToBlue() => imageScreen.color = new Color(0, 0, 255);

    public void ChangeToYellow() => imageScreen.color = new Color(255, 255, 0);

    public void ChangeToGreen() => imageScreen.color = new Color(0, 255, 0);

    public void ChangeToPurple() => imageScreen.color = new Color(255, 0, 255);

    public void ChangeToWhite() => imageScreen.color = new Color(255, 255, 255);

    public void RandomColor() =>  imageScreen.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1);
}
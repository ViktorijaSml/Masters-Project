using UnityEngine;
using UnityEngine.UI;

public class ScreenColor : MonoBehaviour
{
    public static ScreenColor instance;
    private Image imageScreen;
    private Color backgroundColor;
    void Awake()
    {
        instance = this;
        imageScreen = GetComponent<Image>();
        backgroundColor = imageScreen.color;
    }

    // Funkcija za postavljanje svijetline ekrana mikrokontrolera
    // Uzmi vec postojecu boju ali promijeni svijetlinu
    
    // U setBrigthness i setColor moramo spremat u backgroundColor kako bi mogli sacuvat taj broj i mijenat
    // samo tu odredenu stvar
    public void SetBrigthness(float brightness)
    {
        brightness = Mathf.Clamp01(brightness / 255f);
        imageScreen.color = new Color (imageScreen.color.r, imageScreen.color.g, imageScreen.color.b, brightness);
        backgroundColor = imageScreen.color;

    }

    // Slijedeci set funkcija sluze za mijenjanje boje ekrana mikrokontrolera
    // Stavi novu boju ali sacuvaj svijetlinu 
    public void SetColorRGB(float red, float green, float blue)
    {
        red = Mathf.Clamp01(red/255f);
        green = Mathf.Clamp01(green/255f);
        blue = Mathf.Clamp01(blue/255f);

        imageScreen.color = new Color(red, green, blue, backgroundColor.a);
        backgroundColor = imageScreen.color;
    }

    public void SetColorBlack()
    {
        imageScreen.color = new Color(0f, 0f, 0f, backgroundColor.a);
        backgroundColor = imageScreen.color;
    }
    public void SetColorRed()
    {
        imageScreen.color = new Color(255f, 0f, 0f, backgroundColor.a);
        backgroundColor = imageScreen.color;
    }
    public void SetColorBlue()
    {
        imageScreen.color = new Color(0f, 0f, 255f, backgroundColor.a);
        backgroundColor = imageScreen.color;
    }
    public void SetColorYellow()
    {
        imageScreen.color = new Color(255f, 255f, 0f, backgroundColor.a);
        backgroundColor = imageScreen.color;
    }
    public void SetColorGreen()
    {
        imageScreen.color = new Color(0f, 255f, 0f);
        backgroundColor = imageScreen.color;
    }
    public void SetColorPurple()
    {
        imageScreen.color = new Color(255f, 0f, 255f);
        backgroundColor = imageScreen.color;
    }
    public void SetColorWhite()
    {
        imageScreen.color = new Color(255f, 255f, 255f);
        backgroundColor = imageScreen.color;
    }
    public void SetColorColor()
    {
        imageScreen.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1);
        backgroundColor = imageScreen.color;
    }
    }
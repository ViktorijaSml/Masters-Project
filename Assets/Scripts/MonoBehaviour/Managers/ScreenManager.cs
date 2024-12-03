using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager instance;
    private Image imageScreen;
    private Color backgroundColor;
    private readonly Dictionary<string, (int R, int G, int B)> allColors = new Dictionary<string, (int, int, int)>
        {
            { "TURQUOISE", (64, 224, 208) },
            { "ORANGE", (255, 165, 0) },
            { "BLACK", (0, 0, 0) },
            { "RED", (255, 0, 0) },
            { "BLUE", (0, 0, 255) },
            { "YELLOW", (255, 255, 0) },
            { "GREEN", (0, 128, 0) },
            { "PURPLE", (128, 0, 128) },
            { "WHITE", (255, 255, 255) },
            { "BROWN", (150, 75, 0) }
        };

    public Color ScreenColor 
    { 
        get { return backgroundColor; }  
        set { imageScreen.color = value; }  
    }
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

    public void SetColor(string color)
    {
        imageScreen.color = new Color(allColors[color].R / 255f, allColors[color].G / 255f, allColors[color].B / 255f, backgroundColor.a);
        backgroundColor = imageScreen.color;
    }
}
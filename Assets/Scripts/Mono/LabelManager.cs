using System.Collections.Generic;
using System.Linq;
using TMPro;
using UBlockly;
using UBlockly.UGUI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class LabelManager : MonoBehaviour
{
    [SerializeField] GameObject binArea;
    public static LabelManager instance;
    public GameObject labelPrefab;
    private int count = 0;
    private List<bool> labelCounts = new List<bool>();
    private Color lastColor = new Color();

    void Start()
    {
        instance = this;
        Button buttonLabel = GameObject.FindGameObjectWithTag("Label").GetComponent<Button>();
        buttonLabel.onClick.RemoveAllListeners();
        buttonLabel.onClick.AddListener(AddLabelByCorrectOrder);
        
    }

    public int GetLabelCount()
    {
        return labelCounts.Count;
    }
    public void ShowBin(bool isDraging)
    {
        if (isDraging)
        {
            binArea.gameObject.SetActive(true);
        }
        else
        {
            binArea.gameObject.SetActive(false);
        }
    }
    public void AddLabelByCorrectOrder()
    {
        if (labelCounts.Any(x => x == false))
        {
            AddLabel(labelCounts.FindIndex(x => x == false));
        }
        else
        {
            AddLabel(count);
            count++;
        }
    }
    public void AddLabel(int numOrder)
    {
        GameObject label = Instantiate(labelPrefab, Vector3.zero, Quaternion.identity);
        GameObject labelParent = GameObject.Find("DisplayText");
        label.transform.SetParent(labelParent.transform, false);
        label.gameObject.name = label.gameObject.name.Replace("(Clone)", numOrder.ToString());
        label.GetComponent<TextMeshProUGUI>().text += numOrder.ToString();
		BlocklyUI.WorkspaceView.Workspace.CreateVariable(label.name);

		if (labelCounts.Count <= numOrder)
        {
            labelCounts.Add(true);
        }
        else
        {
            labelCounts[numOrder] = true;
        }
    }
    public void RemoveLabel(GameObject obj)
    {
        RectTransform toggleTrans = binArea.transform as RectTransform;
        if (RectTransformUtility.RectangleContainsScreenPoint(toggleTrans, UnityEngine.Input.mousePosition, BlocklyUI.UICanvas.worldCamera))
        {
            string numberPart = obj.name.Substring("Label".Length);
            int number = int.Parse(numberPart);

            if (labelCounts.Count - 1 == number)
            {
                labelCounts.RemoveAt(number);
				count--;
            }
            else
            {
                labelCounts[number] = false;
            }
			BlocklyUI.WorkspaceView.Workspace.DeleteVariable(obj.name);
			Destroy(obj);
        }
    }

    public void HideLabel(bool hide, string labelName)
    {

        if (hide)
        {
             lastColor = GetLabelColor(labelName);
             Color screenColor = ScreenColor.instance.GetScreenColor();
             SetLabelColor(labelName, screenColor);
        }
        else
        {
            SetLabelColor(labelName, lastColor);
        }
    } 
    public void WriteText(string labelName, string text)
    {
        GameObject label = GameObject.Find(labelName);
        label.GetComponent<TextMeshProUGUI>().text = text;

    }
    public Color GetLabelColor (string labelName)
    {
        GameObject label = GameObject.Find(labelName);
        return label.GetComponent<TextMeshProUGUI>().color;
    }
    public void SetLabelColorByRGB(string labelName, float red, float green, float blue)
    {
        red = Mathf.Clamp01(red / 255f);
        green = Mathf.Clamp01(green / 255f);
        blue = Mathf.Clamp01(blue / 255f);
        GameObject label = GameObject.Find(labelName);

        label.GetComponent<TextMeshProUGUI>().color = new Color(red, green, blue);
    }
    public void SetLabelColor(string labelName, Color color) 
    {
        GameObject label = GameObject.Find(labelName);
        label.GetComponent<TextMeshProUGUI>().color = color;
    }
    public Color SetColorBlack() => new Color(0f, 0f, 0f);
    public Color SetColorRed() => new Color(255f, 0f, 0f);
    public Color SetColorBlue() => new Color(0f, 0f, 255f);
    public Color SetColorYellow() => new Color(255f, 255f, 0f);
    public Color SetColorGreen() => new Color(0f, 255f, 0f);
    public Color SetColorPurple() => new Color(255f, 0f, 255f);
    public Color SetColorWhite() => new Color(255f, 255f, 255f);
}

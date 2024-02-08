using System.Collections.Generic;
using System.Linq;
using TMPro;
using UBlockly.UGUI;
using UnityEngine;
using UnityEngine.UI;

public class LabelManager : MonoBehaviour, IShowable, IInteractible
{
    [SerializeField] GameObject binArea;
    public static LabelManager instance;
    public GameObject labelPrefab;
    private int numOrder = 0; 
    private List<bool> labelCounts = new List<bool>();
    private Color lastColor = new Color();

    private void Awake() => instance = this;

    public int GetLabelCount() => labelCounts.Count;

    public bool AnyTrueInList() => labelCounts.Any(x => x == true);

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
    public Button GetLabelButton() => GameObject.FindGameObjectWithTag("Label").transform.GetChild(0).gameObject.GetComponent<Button>();
    public void SetButtonInteractive(bool isInteractive) => GetLabelButton().interactable = isInteractive;   
    public void AddLabelByCorrectOrder()
    {
        numOrder = (labelCounts.Any(x => x == false)) ? labelCounts.FindIndex(x => x == false) : labelCounts.Count;
        AddLabel(numOrder);
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
    public void BinFunctionality(GameObject obj)
    {
        RectTransform toggleTrans = binArea.transform as RectTransform;
        if (RectTransformUtility.RectangleContainsScreenPoint(toggleTrans, UnityEngine.Input.mousePosition, BlocklyUI.UICanvas.worldCamera))
        {
            RemoveLabel(obj);
        }
    }
    public void RemoveLabel(GameObject obj)
    {
            string numberPart = obj.name.Substring("Label".Length);
            int number = int.Parse(numberPart);

            if (labelCounts.Count - 1 == number)
            {
                labelCounts.RemoveAt(number);
            }
            else
            {
                labelCounts[number] = false;
            }
			BlocklyUI.WorkspaceView.Workspace.DeleteVariable(obj.name);
			Destroy(obj);
    }
    public void HideLabel(bool hide, string labelName)
    {
        if (hide)
        {
             lastColor = GetLabelColor(labelName);
             Color screenColor = ScreenManager.instance.GetScreenColor();
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

    public bool CanShowCategory() => GetLabelCount() > 0 && AnyTrueInList();
    public string GetCategoryName() => "LABEL";
    public void ClearGarbage() { /*do nothing*/}
}

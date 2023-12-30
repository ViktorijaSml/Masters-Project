using System.Collections.Generic;
using System.Linq;
using TMPro;
using UBlockly.UGUI;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class LabelManager : MonoBehaviour
{
    [SerializeField] GameObject binArea;
    public static LabelManager instance;
    public GameObject labelPrefab;
    private int count = 0;
    private int checkCount = 0;
    private List<bool> labelCounts = new List<bool>();

    //Sta sve triba iskodirat
    //set label color by rgb
    void Start()
    {
        instance = this;
        Button buttonLabel = GameObject.FindGameObjectWithTag("Label").GetComponent<Button>();
        buttonLabel.onClick.RemoveAllListeners();
        buttonLabel.onClick.AddListener(() =>
        {
            AddLabelByCorrectOrder();
            SetLabelColorByRGB("Label0", 120, 255, 0);
        });
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
            Debug.Log("prvi");
            AddLabel(labelCounts.FindIndex(x => x == false));
        }
        else
        {
            Debug.Log("drugi");
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
        if (RectTransformUtility.RectangleContainsScreenPoint(toggleTrans, Input.mousePosition, BlocklyUI.UICanvas.worldCamera))
        {
            string numberPart = obj.name.Substring("Label".Length);
            int number = int.Parse(numberPart); //sada imamo broj te labele
 
            if (labelCounts.Count-1 == number) 
            {
                labelCounts.RemoveAt(number);
                count--;
            }
            else
            {
                labelCounts[number] = false;
            }

            Destroy(obj);    
        }
    }

    public void WriteText(string labelName, string text)
    {
        GameObject label = GameObject.Find(labelName);
        label.GetComponent<TextMeshProUGUI>().text  = text;

    }
    public void SetLabelColorByRGB(string labelName, float red, float green, float blue)
    {
        red = Mathf.Clamp01(red / 255f);
        green = Mathf.Clamp01(green / 255f);
        blue = Mathf.Clamp01(blue / 255f);
        GameObject label = GameObject.Find(labelName);

        label.GetComponent<TextMeshProUGUI>().color = new Color(red, green, blue);
    }
}

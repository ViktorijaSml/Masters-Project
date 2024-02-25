using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LabelBehaviour : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Vector3 startPos;
    private VerticalLayoutGroup canvasGroup;
    private RectTransform rectTransform;
    public string Text {
        get { return gameObject.GetComponent<TextMeshProUGUI>().text; }
        set { gameObject.GetComponent<TextMeshProUGUI>().text = value; }
    }
    public Color FontColor
    {
        get { return gameObject.GetComponent<TextMeshProUGUI>().color; }
        set { gameObject.GetComponent<TextMeshProUGUI>().color = value; }
    }

    private void Start()
    {
        canvasGroup = GetComponentInParent<VerticalLayoutGroup>();
        rectTransform = GetComponent<RectTransform>();
        startPos = transform.localPosition;
	}


    public void OnBeginDrag(PointerEventData eventData)
    {
		RefreshCanvasGroup();
		LabelManager.instance.ShowBin(true);
    }

    public void OnDrag(PointerEventData eventData)
      => rectTransform.anchoredPosition += eventData.delta / canvasGroup.transform.localScale.x * 2;

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localPosition = startPos;
		RefreshCanvasGroup();
		LabelManager.instance.ShowBin(false);
        LabelManager.instance.BinFunctionality(gameObject);
    }

    private void RefreshCanvasGroup()
    {
        if (canvasGroup == null) return;
        canvasGroup.CalculateLayoutInputHorizontal();
        canvasGroup.CalculateLayoutInputVertical();
        canvasGroup.SetLayoutHorizontal();
        canvasGroup.SetLayoutVertical();
    }
}

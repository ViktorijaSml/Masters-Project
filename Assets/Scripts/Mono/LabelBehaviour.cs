using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LabelBehaviour : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Vector3 startPos;
    private VerticalLayoutGroup canvasGroup;
    private RectTransform rectTransform;


    private void Start()
    {
        startPos = transform.localPosition;
        canvasGroup = GetComponentInParent<VerticalLayoutGroup>();
        rectTransform = GetComponent<RectTransform>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (canvasGroup != null)
        {
            canvasGroup.enabled = false;
        }
        LabelManager.instance.ShowBin(true);
    }
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvasGroup.transform.localScale.x * 3;

    }

    public void OnEndDrag(PointerEventData eventData)
    {

        transform.localPosition = startPos;
        if (canvasGroup != null)
        {
            canvasGroup.enabled = true;
        }
        LabelManager.instance.ShowBin(false);
        LabelManager.instance.RemoveLabel(this.gameObject);
    }
}

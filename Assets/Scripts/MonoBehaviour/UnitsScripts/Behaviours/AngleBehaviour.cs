using UnityEngine;
using UnityEngine.EventSystems;

public class AngleBehaviour : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    private Camera myCam;
    private Vector3 screenPos;
    private float angleOffset, angle = 0;
    private RectTransform handleCollider;

    public float Angle { get { return transform.localEulerAngles.z; } }
    private void Start()
    {
        myCam = Camera.main;
        Collider2D col = GetComponent<Collider2D>();
        handleCollider = col.transform as RectTransform;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(handleCollider, Input.mousePosition, myCam))
        {
            screenPos = myCam.WorldToScreenPoint(transform.position);
            Vector3 vec3 = Input.mousePosition - screenPos;
            angleOffset = (Mathf.Atan2(transform.right.y, transform.right.x) - Mathf.Atan2(vec3.y, vec3.x)) * Mathf.Rad2Deg;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(handleCollider, Input.mousePosition, myCam))
        {
            Vector3 vec3 = Input.mousePosition - screenPos;
            angle = Mathf.Atan2(vec3.y, vec3.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, 0, angle + angleOffset);
        }
    }
}



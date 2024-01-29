using UnityEngine;
using UnityEngine.EventSystems;

public class AngleBehaviour : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    private Camera myCam;
    private Vector3 screenPos;
    private float angleOffset, angle = 0;
    private RectTransform handleCollider;

	public float totalRotation = 0f;
	public float previousAngle = 0f;
	public AngleManager angleManager;

	public float Angle { get { return transform.localEulerAngles.z; } }
    private void Start()
    {
        myCam = Camera.main;
        Collider2D col = GetComponent<Collider2D>();
        handleCollider = col.transform as RectTransform;
		angleManager = transform.parent.GetComponent<AngleManager>();

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
			float rawAngle = Vector2.SignedAngle(Vector2.right, vec3);
			float deltaAngle = rawAngle - previousAngle;

			// Detektira skok
			if (deltaAngle < -180)
			{
				deltaAngle += 360;
			}
			else if (deltaAngle > 180)
			{
				deltaAngle -= 360;
			}

			// Provjerava da li je rotacija unutar granica
			if ((totalRotation + deltaAngle) <= 1080 && (totalRotation + deltaAngle) >= 0)
			{
				totalRotation += deltaAngle;
			}

			previousAngle = rawAngle;

			Debug.Log("ANGLE " + totalRotation);
			float zRotation = totalRotation + angleOffset;
			transform.rotation = Quaternion.AngleAxis(zRotation, Vector3.forward);
		}
	}

}



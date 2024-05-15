using UnityEngine;
using UnityEngine.UI;

public class UnitsBehaviour : MonoBehaviour
{
    public Button unitButton;
    private GameObject unitSimulation, objectPrefab, unitSlot;
   [HideInInspector] public bool disableCreateObject;

	private void Start()
	{
		objectPrefab = Resources.Load<GameObject>("Units Objects/" + this.name);

		unitButton = GetComponent<Button>();
		unitSlot = UnitsManager.instance.GetUnitSlot();
	
		if (disableCreateObject == false)
		{
			unitButton.onClick.AddListener(() =>
			{
                SoundManager.PlaySound(SoundName.BlockDragNDrop);
                Debug.Log("Unit added: " + this.name);
				CreateUnitObject();
				UnitsManager.instance.CloseUnits(); 
			});
		}
		else
		{
			unitButton.onClick.RemoveAllListeners();
			unitButton.onClick.AddListener(() =>
			{
				SoundManager.PlaySound(SoundName.ButtonPressUI, 0.6f);
				Debug.Log("Unit deleted: " + this.name);
				DestroyUnitObject();
			});

		}
	}


	public void CreateUnitObject()
    {
        GameObject unit = Instantiate(gameObject, Vector3.zero, Quaternion.identity);

        unit.transform.SetParent(unitSlot.transform, false);
        unit.GetComponent<UnitsBehaviour>().disableCreateObject = true; 
        unit.transform.GetChild(0).GetComponent<Image>().enabled = true; //otkrivanje ikonice 'x'
        unit.gameObject.name = unit.gameObject.name.Replace("(Clone)", "");

		unitSimulation = Instantiate(objectPrefab, Vector3.zero, Quaternion.identity);
        unitSimulation.transform.SetParent (UnitsManager.instance.UnitsSimulation.transform, false);
        unitSimulation.gameObject.name = unitSimulation.gameObject.name.Replace("(Clone)", "");

        unit.GetComponent<UnitsBehaviour>().unitSimulation = unitSimulation;

        RectTransform rectTransform = unit.GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            rectTransform.sizeDelta = new Vector2(160f, 170f);
            rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            rectTransform.pivot = new Vector2(0.5f, 0.5f);
            rectTransform.anchoredPosition = Vector2.zero;
        }
    }

    public void DestroyUnitObject() 
    {
        Destroy(unitSimulation);
        Destroy(gameObject);
    }
}

using UnityEngine;
using UnityEngine.UI;

public class OnClickEvent : MonoBehaviour
{
    private Button unitButton;
    private GameObject unitSimulation;
    public GameObject objectPrefab;
   [HideInInspector] public bool disableCreateObject;

    private void Start()
    {
        unitButton = GetComponent<Button>();
        // Provjerava ima li objekt sposobnost kreiranje vlastitog objekta.
        // Odnosno, je li objekt kreiran 
        if(disableCreateObject == false)
        {
            // Ako objekt nije kreiran (nema jos modula dodanih),
            // Dodaje se metoda koja će se pozvati pritiskom na tipku objekta:
            // "Kreiraj mi taj objekt/modul"
            unitButton.onClick.AddListener(() => 
            {
                Debug.Log("create");
                CreateUnitObject();
                UnitsManager.instance.CloseUnits(); //Makni Units prozor
                UnitsManager.instance.OpenUnitsSimulation();
            });            
        }
        else
        {
            //Ako je objekt kreiran (dodali smo modul),
            //Makni prethodni Listener i dodaj novi sa novom metodom:
            //Zatvori/Makni modul 
            unitButton.onClick.RemoveAllListeners();
            unitButton.onClick.AddListener(() => 
            {
                Debug.Log("delete");
                DestroyUnitObject();
                UnitsManager.instance.CloseUnitsSimulation();

            });
        }
    }

    public void CreateUnitObject()
    {
        GameObject unitSlot = GameObject.FindGameObjectWithTag("UnitSlot");
        // Stvaranje novog odabranog objekta
        GameObject unit = Instantiate(gameObject, Vector3.zero, Quaternion.identity);

        // Postavljanje parenta na "UnitSlot"
        unit.transform.SetParent(unitSlot.transform, false);
        unit.GetComponent<OnClickEvent>().disableCreateObject = true; 
        unit.transform.GetChild(0).GetComponent<Image>().enabled = true; //otkrivanje ikonice 'x'
        unit.gameObject.name = unit.gameObject.name.Replace("(Clone)", "");
        // Postavljanje svojstava RectTransform za pravilno pozicioniranje i skaliranje

		unitSimulation = Instantiate(objectPrefab, Vector3.zero, Quaternion.identity);
        unitSimulation.transform.SetParent (UnitsManager.instance.UnitsSimulation.transform, false);
        unitSimulation.gameObject.name = unitSimulation.gameObject.name.Replace("(Clone)", "");
        
        unit.GetComponent<OnClickEvent>().unitSimulation = unitSimulation;

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

using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UnitsBehaviour : MonoBehaviour
{
    private Button unitButton;
    private GameObject unitSimulation, objectPrefab, unitSlot;
   [HideInInspector] public bool disableCreateObject;

    private void Start()
    {
        objectPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/Units Objects/" + this.name + ".prefab");

        unitButton = GetComponent<Button>();
        unitSlot = UnitsManager.instance.GetUnitSlot();
        // Provjerava ima li objekt sposobnost kreiranje vlastitog objekta.
        // Odnosno, je li objekt kreiran 
        if (disableCreateObject == false)
        {
            // Ako objekt nije kreiran (nema jos modula dodanih),
            // Dodaje se metoda koja će se pozvati pritiskom na tipku objekta:
            // "Kreiraj mi taj objekt/modul"
            unitButton.onClick.AddListener(() => 
            {
                Debug.Log("Unit added: " + this.name);
                CreateUnitObject();
                UnitsManager.instance.CloseUnits(); //Makni Units prozor
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
                Debug.Log("Unit deleted: " + this.name);
                DestroyUnitObject();
            });
            
        }
    }

    public void CreateUnitObject()
    {
        // Stvaranje novog odabranog objekta
        GameObject unit = Instantiate(gameObject, Vector3.zero, Quaternion.identity);

        unit.transform.SetParent(unitSlot.transform, false);
        unit.GetComponent<UnitsBehaviour>().disableCreateObject = true; 
        unit.transform.GetChild(0).GetComponent<Image>().enabled = true; //otkrivanje ikonice 'x'
        unit.gameObject.name = unit.gameObject.name.Replace("(Clone)", "");

		unitSimulation = Instantiate(objectPrefab, Vector3.zero, Quaternion.identity);
        unitSimulation.transform.SetParent (UnitsManager.instance.UnitsSimulation.transform, false);
        unitSimulation.gameObject.name = unitSimulation.gameObject.name.Replace("(Clone)", "");

        // Postavljanje svojstava RectTransform za pravilno pozicioniranje i skaliranje
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
        //this.GetComponent<IShowable>().ClearGarbage();
        Destroy(unitSimulation);
        Destroy(gameObject);
    }
}

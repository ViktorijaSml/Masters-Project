using UnityEngine;

public class UnitsManager : MonoBehaviour
{
    private CanvasGroup Units;
    public GameObject UnitsSimulation;
    public static UnitsManager instance;

    void Awake() 
    {
        Units = GameObject.FindGameObjectWithTag("Units").GetComponent<CanvasGroup>();
        instance = this;
        Application.targetFrameRate = 60;
    }

    public void CloseUnits() =>  Units.enabled = true;
    public void OpenUnits() =>  Units.enabled = false;

    public void CloseUnitsSimulation() => UnitsSimulation.SetActive(false);
    public void OpenUnitsSimulation() => UnitsSimulation.SetActive(true);
}

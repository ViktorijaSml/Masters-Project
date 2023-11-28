using UnityEngine;

public class UnitsManager : MonoBehaviour
{
    private CanvasGroup Units;
    public static UnitsManager instance;

    void Awake() {
        Units = GameObject.FindGameObjectWithTag("Units").GetComponent<CanvasGroup>();
        instance = this;
    }
   
    public void Close() =>  Units.enabled = true;

    public void Open() =>  Units.enabled = false;


}

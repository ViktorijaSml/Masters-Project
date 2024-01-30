using UnityEngine;

public class AngleManager : MonoBehaviour, IShowable
{
    public static AngleManager instance;
    public bool _isCounterClockwise;
    private void Awake() => instance = this;
  
    public int GetAngle() => (int)transform.GetChild(0).GetComponent<AngleBehaviour>().Angle;
   
    public bool CanShowCategory() => UnitsManager.instance.UnitSlotHasChildren() && UnitsManager.instance.GetUnitImage().name.ToUpper() == GetCategoryName();
    public void ClearGarbage() { /*do nothing*/}
    public string GetCategoryName() => this.name.ToUpper();
}

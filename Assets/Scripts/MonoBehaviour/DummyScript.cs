using UnityEngine;

public class DummyScript : MonoBehaviour, IShowable
{
    //this class is used for units that use an already existing class, so we can use the interface
    //without corrupting the other (used) class

    public bool CanShowCategory()
    {
        GameObject unitSlot = UnitsManager.instance.GetUnitSlot();
        return UnitsManager.instance.UnitSlotHasChildren() && UnitsManager.instance.GetUnitImage().name.ToUpper() == GetCategoryName();
    }
    public void ClearGarbage()
    {
           ButtonManager.instance.ClearAllListeners("RedBtn");
           ButtonManager.instance.ClearAllListeners("BlueBtn");
    }
    public string GetCategoryName() => this.name.ToUpper();
}

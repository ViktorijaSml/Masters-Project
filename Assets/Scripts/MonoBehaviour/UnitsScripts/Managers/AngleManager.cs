using UnityEngine;

public class AngleManager : MonoBehaviour, IShowable
{
    private float _totalAngle = 0, _previousAngle = 0, _prevDelta = 0;
    public static AngleManager instance;
    public bool _isCounterClockwise;
    private void Awake() => instance = this;
  
    private void Update()
    {
      // Debug.Log(GetAngle());
        GetAngle();
    }
    public int GetAngle()
    {
        float _currentAngle = transform.GetChild(0).GetComponent<AngleBehaviour>().totalRotation;
        float currDelta = _currentAngle - _previousAngle;
        float calculated = currDelta % 360;

       //Debug.Log(calculated);
        if (calculated > 180 || calculated < 0) //clockwise
        {
            _isCounterClockwise = false;
        }
        else if (calculated < -180 || calculated > 0) //counterclockwise
        {
            _isCounterClockwise = true;
        }

        float angle = _currentAngle; 
        if ( _isCounterClockwise)
        {   
                _totalAngle += Mathf.Abs(_currentAngle - _previousAngle);
        }
        else
        {
            _totalAngle -= Mathf.Abs(angle - _previousAngle);
        }
        
        _previousAngle = _currentAngle;
        _prevDelta = _currentAngle - _previousAngle;
        return (int)_totalAngle;
    }

  
    public bool CanShowCategory() => UnitsManager.instance.UnitSlotHasChildren() && UnitsManager.instance.GetUnitImage().name.ToUpper() == GetCategoryName();
    public void ClearGarbage() { /*do nothing*/}
    public string GetCategoryName() => this.name.ToUpper();
}

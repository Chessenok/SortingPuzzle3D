using UnityEngine;
using UnityEngine.Serialization;

public class SlotView : MonoBehaviour
{
    public float LeftLimit { get; private set; }
    public float RightLimit { get; private set; }
    private int _lockedFor;
    [SerializeField] private float[] _layers;
    [FormerlySerializedAs("_leftPlaceVector")][SerializeField] private Transform _leftPlaceTransform;
    [FormerlySerializedAs("_rightPlaceVector")] [SerializeField] private Transform _rightPlaceTransform;
    [FormerlySerializedAs("_centerPlaceVector")] [SerializeField] private Transform _centerPlaceTransform;


    private GameObject _left, _right, _center;

    public SlotModel slot {  get; private set; } 
    private void Start()
    {
        LeftLimit = (_centerPlaceTransform.position.x + _leftPlaceTransform.position.x) / 2;
        RightLimit = (_centerPlaceTransform.position.x + _rightPlaceTransform.position.x) / 2;
    }

    public void ChangeLayout()
    {

    }

    public GameObject GetObject(string place)
    {
        if (place == "right")
        {
            return _right;
        } 
        else if (place == "left")
        {
            return _left;
        }
        return _center;
    }

    public bool IsFreePlace(string place)
    {
        if (place == "right")
        {
            return _right == null;
        }
        else if (place == "left")
        {
            return _left == null;
        }
        return _center == null;
    }

   // public bool FreeOn

    public void TryPutObject(GameObject obj, string place,out bool success)
    {
        if (place == "right")
        {
           if (_right == null)
            {
                _right = obj;
                obj.transform.position = _rightPlaceTransform.position;
                success = true;
            }
            else 
                success = false;
        }
        else if (place == "left")
        {
           if(_left == null)
            {
                _left = obj;
                obj.transform.position = _leftPlaceTransform.position;
                success = true;
            }
           else
                success = false;
        }
        if (_center == null)
        {
            _center = obj;
            obj.transform.position = _centerPlaceTransform.position;
            success = true;
        }
        else
            success = false;
    }

    public string GetMousePosition(float xpos)
    {
        if (xpos > RightLimit)
        {
            return "right";
        }
        else if (xpos < LeftLimit)
        {
            return "left";
        }
        return "center";
    }

    public Vector3 GetLeftPos()
    {
        return _leftPlaceTransform.position;
    }

    public Vector3 GetCenterPos()
    {
        return _centerPlaceTransform.position;
    }

    public Vector3 GetRightPos()
    {
        return _rightPlaceTransform.position;
    }
}

using System.Collections.Generic;
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


    private Dictionary<string, GameObject> _currentLayer;

    public SlotModel slotModel {  get; private set; } 
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
        GameObject controlObject;
        _currentLayer.TryGetValue(place, out controlObject);
        return controlObject;
    }

    public bool IsFreePlace(string place)
    {
        GameObject controlObject;
        _currentLayer.TryGetValue(place, out controlObject);
        if (controlObject != null)
        {
            return true;
        }
       return false;
    }


   // public bool FreeOn

    public void TryPutObject(GameObject obj, string place,out bool success)
    {
        if (IsFreePlace(place))
        {
            if(_currentLayer.ContainsKey(place))
            {
                _currentLayer[place] = obj;
            }
            else
                _currentLayer.Add(place, obj);

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

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SlotView : MonoBehaviour
{
    public float LeftLimit { get; private set; }
    public float RightLimit { get; private set; }
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

    public void ResetSlot()
    {
        slotModel.ResetSlot();
        _currentLayer.Clear();
    }

    public GameObject GetObject(string place)
    {
        GameObject controlObject = null;
        if (_currentLayer.TryGetValue(place,out controlObject))
        {
            return controlObject;
        }
        return null;
        
    }

    public GameObject TryGetObject(string place, out bool success)
    {
        GameObject gameObject = null;
        if (_currentLayer == null)
        {
            Debug.Log("currentLayer == null");
        }
        if (_currentLayer.TryGetValue(place,out gameObject))
        {
            success = true;
            return gameObject;
        }

        success = false;
        return null;
        
    }

    public bool IsFreePlace(string place)
    {
        GameObject controlObject;
        _currentLayer.TryGetValue(place, out controlObject);
        if (controlObject == null)
        {
            return true;
        }
       return false;
    }


   // public bool FreeOn

    public void TryPutObject(GameObject obj, string place,out bool success,out Vector3 pos)
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

    public void RemoveObject(string place)
    {
        if (_currentLayer.ContainsKey(place))
            _currentLayer.Remove(place);
    }

    public void SetFirstLayer(Dictionary<string, GameObject> layer)
    {
        _currentLayer = layer;
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

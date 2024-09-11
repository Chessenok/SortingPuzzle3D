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

    public void ChangeLayer()
    {
        _currentLayer = new Dictionary<string, GameObject>();
        if(slotModel.ThereAreMoreLayers())
        {
            slotModel.MoveLayersAndGetNextLayer(out _currentLayer);
        }
    }
    public void SetModel(SlotModel model)
    {
        slotModel = model;
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
            RemoveObject(place);
            return gameObject;
        }

        success = false;
        return null;
        
    }

    private void CheckWin()
    {
        if(_currentLayer.Count == 3)
        {
            if(WinFlow.Instance.CheckWinSlot(_currentLayer,slotModel))
                ChangeLayer();

        }
    }

    public void OnSuccessMuteObject()
    {
        if(_currentLayer.Count == 0)
            ChangeLayer();
    }

    public bool IsFreePlace(string place)
    {
        if (_currentLayer == null) 
        { 
            Debug.LogError("No layer");
            return false; 
        }
        if( _currentLayer.ContainsKey(place) )
        {
            return false;
        }
        return true;
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
            CheckWin();
        }
        else
            success = false;
        pos = GetPos(place);
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

    public Vector3 GetPos(string place)
    {
        switch (place)
        {
            case "left":
                return GetLeftPos();
            case "right":
                return GetRightPos();
            case "center":
                return GetCenterPos();
            default:
                Debug.LogError("Tryna get wrong place");
                return Vector3.zero;
        }
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

using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SlotView : MonoBehaviour
{
    public float LeftLimit { get; private set; }
    public float RightLimit { get; private set; }
    [SerializeField] private TextMeshPro _lockText;
    [SerializeField] private Transform _leftPlaceTransform;
    [SerializeField] private Transform _rightPlaceTransform;
    [SerializeField] private Transform _centerPlaceTransform;
    private int lockedFor;
    [SerializeField] private GameObject _lockPanel;
    private bool locked;
    private IWinFlow winFlow;
    private Dictionary<string, GameObject> _currentLayer;

    public SlotModel slotModel {  get; private set; } 
    private void Start()
    {
        LeftLimit = (_centerPlaceTransform.position.x + _leftPlaceTransform.position.x) / 2;
        RightLimit = (_centerPlaceTransform.position.x + _rightPlaceTransform.position.x) / 2;
        LevelManager.Instance.GetWinFlow(out winFlow);
        winFlow.OnLayerWin += OnWinLayer;
    }

    private void ChangeLayer()
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
        lockedFor = model.LockedFor;
        if (lockedFor > 0) 
        {
            _lockPanel.SetActive(true);
            _lockText.text = $"Locked for:{lockedFor}";
            locked = true;
        }
    }

    private void OnWinLayer(SlotModel slotModel)
    {
        lockedFor -= 1;
        if (lockedFor > 0)
        {
            _lockText.text = $"Locked for:{lockedFor}";
        }
        if (lockedFor == 0)
        {
            _lockPanel.SetActive(false);
            locked = false;
        }
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
        if (locked)
        {
            success = false; return null;
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

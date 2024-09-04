
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Pool : MonoBehaviour
{
    public static Pool Instance { get; private set; }

    [SerializeField] private GameObject RedPrefab;
    [SerializeField] private GameObject GreenPrefab;
    [SerializeField] private GameObject YellowPrefab;
    [SerializeField] private GameObject SlotPrefab;

    private List<GameObject> _greens = new List<GameObject>();
    private List<GameObject> _reds = new List<GameObject>();
    private List<GameObject> _yellows = new List<GameObject>();
    private List<GameObject> _slots = new List<GameObject>();
    private List<GameObject> _allGreens = new List<GameObject>();
    private List<GameObject> _allReds = new List<GameObject>();
    private List<GameObject> _allYellows = new List<GameObject>();
    private List<GameObject> _allSlots = new List<GameObject>();
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public GameObject GetNewObject(string color, Transform transform)
    {
        GameObject obj;
        switch (color) {
            case "green":
                if (_greens[0] != null)
                {
                    obj = _greens[0];
                    obj.gameObject.SetActive(true);
                    // obj.gameObject.transform.parent = transform;
                    obj.gameObject.transform.position = transform.position;
                    _greens.RemoveAt(0);
                    return obj;
                }
                obj = Instantiate(GreenPrefab, transform);
                _allGreens.Add(obj);
                return obj;
            case "red":
                if (_reds[0] != null)
                {
                    obj = _reds[0];
                    obj.gameObject.SetActive(true);
                    //obj.gameObject.transform.parent = transform;
                    obj .gameObject.transform.position = transform.position;
                    _reds.RemoveAt(0);
                    return obj;
                }
                obj = Instantiate(RedPrefab, transform);
                _allReds.Add(obj);
                return obj;
            case "yellow":
                if (_yellows[0] != null)
                {
                    obj = _yellows[0];
                    obj.gameObject.SetActive(true);
                  //  obj.gameObject.transform.parent = transform;
                    obj.transform.position = transform.position; 
                    _yellows.RemoveAt(0);
                    return obj;
                }
                obj = Instantiate(YellowPrefab, transform);
                _allYellows.Add(obj);
                return obj;
            default:
                Debug.LogError("Tryna get wrong color");
                return null;
        }

    }

    public GameObject GetNewSlot(Transform transform)
    {
        GameObject obj;
        if (_slots[0] != null)
        {
            obj = _slots[0];
            obj.gameObject.SetActive(true);
           // obj.transform.SetParent(transform);
            obj.transform.position = transform.position;
            _slots.RemoveAt(0);
            return obj;
        }
        obj = Instantiate(SlotPrefab, transform.position,SlotPrefab.transform.rotation);
        _allSlots.Add(obj);
        return obj;
    }    
    public GameObject GetNewSlot(Vector3 vector)
    {
        GameObject obj;
        if (_slots[0] != null)
        {
            obj = _slots[0];
            obj.gameObject.SetActive(true);
           // obj.transform.SetParent(transform);
            obj.transform.position = vector;
            _slots.RemoveAt(0);
            return obj;
        }
        obj = Instantiate(SlotPrefab, vector,SlotPrefab.transform.rotation);
        _allSlots.Add(obj);
        return obj;
    }


    
}

using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour, ISpawnObjects
{
    [SerializeField] private GameObject[] _objects;
    private Dictionary<string, GameObject> _objectsDictionary = new Dictionary<string, GameObject>();

    private void Start()
    {
        foreach (GameObject obj in _objects)
        {
            _objectsDictionary.Add(obj.name, obj);
        }
    }
    public void CreateObject(SlotView slotView, string objIndex, string pos, int layer)
    {
        
    }
}

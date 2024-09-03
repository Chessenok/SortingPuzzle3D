using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour, ISpawnObjects
{
    [SerializeField] private GameObject[] _objects;
    private Dictionary<string, GameObject> _objectsDictionary = new Dictionary<string, GameObject>();
    public void CreateObject(SlotView slotView, string objIndex, string pos, int layer)
    {
        
    }
}

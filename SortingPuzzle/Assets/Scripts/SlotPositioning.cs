using System.Collections.Generic;
using UnityEngine;

public class SlotPositioning : MonoBehaviour, IPositionateSlots
{
    [SerializeField] private Vector3[] _firstTemplate;
    public Pool pool { get; private set; }

    private void OnEnable()
    {
        pool = Pool.Instance;
    }

    public List<GameObject> GetSlotsInPosition(int num)
    {
      List<GameObject> _list = new List<GameObject>();
        switch (num)
        {
            case 3:
                foreach (Vector3 vector in _firstTemplate) 
                {
                    _list.Add(pool.GetNewSlot(vector));
                }
                return _list;



            default:
                Debug.LogError($"Tryna get wrong template, or it doesnt exist,{num}");
                return _list;
        }
    }
}

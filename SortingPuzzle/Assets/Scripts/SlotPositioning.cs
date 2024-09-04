using System.Collections.Generic;
using UnityEngine;

public class SlotPositioning : MonoBehaviour
{
    [SerializeField] private Vector3[] _firstTemplate;
    private Pool _pool;

    private void OnEnable()
    {
        _pool = Pool.Instance;
    }

    public List<GameObject> GetSlotsInPosition(int num)
    {
      List<GameObject> _list = new List<GameObject>();
        switch (num)
        {
            case 3:
                foreach (Vector3 vector in _firstTemplate) 
                {
                    _list.Add(_pool.GetNewSlot(vector));
                }
                return _list;



            default:
                Debug.LogError("Tryna get wrong template, or it doesnt exist");
                return _list;
        }
    }
}

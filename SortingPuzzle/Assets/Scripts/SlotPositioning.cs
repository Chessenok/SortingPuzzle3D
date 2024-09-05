using System.Collections.Generic;
using UnityEngine;

public class SlotPositioning : MonoBehaviour, IPositionateSlots
{
    [SerializeField] private Vector3[] _3Template;
    [SerializeField] private Vector3[] _4Template;
    [SerializeField] private Vector3[] _5Template;
    [SerializeField] private Vector3[] _6Template;
    [SerializeField] private Vector3[] _7Template;
    [SerializeField] private Vector3[] _8Template;
    [SerializeField] private Vector3[] _9Template;
    [SerializeField] private Vector3[] _10Template;
    [SerializeField] private Vector3[] _11Template;
    [SerializeField] private Vector3[] _12Template;
    [SerializeField] private Vector3[] _13Template;
    [SerializeField] private Vector3[] _14Template;
    [SerializeField] private Vector3[] _15Template;
    [SerializeField] private Vector3[] _16Template;
    [SerializeField] private Vector3[] _17Template;
    [SerializeField] private Vector3[] _18Template;
    [SerializeField] private List<List<Vector3>> _templates;
    [SerializeField]private Dictionary<int, Vector3[]> templates = new Dictionary<int, Vector3[]>();
    public Pool pool { get; private set; }


    private void OnEnable()
    {
        pool = Pool.Instance;
        templates.Add(3,_3Template); templates.Add(4,_4Template); templates.Add(5,_5Template); templates.Add(6,_6Template); templates.Add(7,_7Template);
        templates.Add(8,_8Template); templates.Add(9,_9Template); templates.Add(10,_10Template); templates.Add(11,_11Template); templates.Add(12,_12Template);
        templates.Add(13,_13Template); templates.Add(14,_14Template); templates.Add(15,_15Template); templates.Add(16,_16Template); templates.Add(17,_17Template);
        templates.Add(18,_18Template);
    }


    public List<GameObject> GetSlotsInPosition(int num)
    {
      List<GameObject> _list = new List<GameObject>();
        Vector3[] array;
        if (templates.ContainsKey(num))
        {
             array = templates[num];
        }
        else
        {
            Debug.LogError($"Tryna get wrong tempate, {num}");
            return _list;
        }

        for (int i = 0; i < num; i++)
        {
            _list.Add(pool.GetNewSlot(array[i]));
        }
        return _list;
    }
}

using UnityEngine;

public class ObjectPostitioning : MonoBehaviour
{
    private Pool _pool;
    private void Start()
    {
        _pool = Pool.Instance;
    }

    public GameObject GetObjectOnPos(SlotView slotView, string place,string color)
    {
        switch (place)
        {
            case "left":
                return _pool.GetNewObject(color, slotView.GetLeftPos());
            case "center":
                return _pool.GetNewObject(color, slotView.GetCenterPos());
            case "right":
                return _pool.GetNewObject(color, slotView.GetRightPos());
            default:
                Debug.LogError("Tryna get wrong place");
                return null;
        }
    }
}

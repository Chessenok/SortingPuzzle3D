using UnityEngine;

public class ObjectPostitioning : MonoBehaviour
{
    private Pool _pool;
    private void Start()
    {
        _pool = Pool.Instance;
    }

    [SerializeField] private float[] _layering;

    public GameObject GetObjectOnPos(SlotView slotView, string place,string color,int layerIndex)
    {
        GameObject go;
        switch (place)
        {
            case "left":
                go = _pool.GetNewObject(color, new Vector3(slotView.GetLeftPos().x, slotView.GetLeftPos().y, _layering[layerIndex]));
               // slotView.PutObject(go, place);
                return go;
            case "center":
                go = _pool.GetNewObject(color, new Vector3(slotView.GetCenterPos().x, slotView.GetCenterPos().y, _layering[layerIndex]));
              //  slotView.PutObject(go, place);
                return go;
            case "right":
                go = _pool.GetNewObject(color, new Vector3(slotView.GetRightPos().x, slotView.GetRightPos().y, _layering[layerIndex]));
            //    slotView.PutObject(go, place);
                return go;
            default:
                Debug.LogError("Tryna get wrong place");
                return null;
        }
    }
}

using UnityEngine;
using UnityEngine.Serialization;

public class SlotView : MonoBehaviour
{
    public float LeftLimit { get; private set; }
    public float RightLimit { get; private set; }
    private int _lockedFor;
    [SerializeField] private float[] _layers;
    [FormerlySerializedAs("_leftPlaceVector")][SerializeField] private Transform _leftPlaceTransform;
    [FormerlySerializedAs("_rightPlaceVector")] [SerializeField] private Transform _rightPlaceTransform;
    [FormerlySerializedAs("_centerPlaceVector")] [SerializeField] private Transform _centerPlaceTransform;


    private GameObject _left, _right, _center;

    public SlotModel slot {  get; private set; } 
    private void Start()
    {
        LeftLimit = (_centerPlaceTransform.position.x + _leftPlaceTransform.position.x) / 2;
        RightLimit = (_centerPlaceTransform.position.x + _rightPlaceTransform.position.x) / 2;
    }

    public void ChangeLayout()
    {

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

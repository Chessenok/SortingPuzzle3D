using UnityEngine;

public class SlotView : MonoBehaviour
{
    public float LeftLimit { get; private set; }
    public float RightLimit { get; private set; }
    private int _lockedFor;
    [SerializeField] private float[] _layers;
    [SerializeField] private Transform _leftPlaceVector;
    [SerializeField] private Transform _rightPlaceVector;
    [SerializeField] private Transform _centerPlaceVector;


    private GameObject _left, _right, _center;

    public SlotModel slot {  get; private set; }
    private void Start()
    {
        LeftLimit = (_centerPlaceVector.position.x + _leftPlaceVector.position.x) / 2;
        RightLimit = (_centerPlaceVector.position.x + _rightPlaceVector.position.x) / 2;
    }

    public void ChangeLayout()
    {

    }


}

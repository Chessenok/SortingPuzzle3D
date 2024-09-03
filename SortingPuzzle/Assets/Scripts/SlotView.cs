using UnityEngine;

public class SlotView : MonoBehaviour
{
    private float _leftLimit;
    private float _rightLimit;
    private int _lockedFor;
    [SerializeField] private float[] _layers;
    [SerializeField] private Transform _leftPlaceVector;
    [SerializeField] private Transform _rightPlaceVector;
    [SerializeField] private Transform _centerPlaceVector;

    private void Start()
    {
        _leftLimit = (_centerPlaceVector.position.x + _leftPlaceVector.position.x) / 2;
        _rightLimit = (_centerPlaceVector.position.x + _rightPlaceVector.position.x) / 2;
    }

    public void ChangeLayout()
    {

    }

    public GameObject CreateObject(GameObject obj, string place, int layer)
    {
        GameObject newObj;
        switch (place) {
            case "left":
                newObj = Instantiate(obj,_leftPlaceVector);
                break;
            case "center":
               newObj = Instantiate(obj,_centerPlaceVector);
                break;
            case "right":
               newObj = Instantiate(obj,_rightPlaceVector);
                break;

            default:
                Debug.LogError("Invalid place, returning null");
                return null;
        }
        if(layer != 0)
        {
            Transform transform = obj.transform;
            Vector3 newpos  = new Vector3(transform.position.x, transform.position.y, _layers[layer]);
        }
        return newObj;
    }

}

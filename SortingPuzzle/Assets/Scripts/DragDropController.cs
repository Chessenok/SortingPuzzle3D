using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDropController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    private GameObject _currentDraggedObject;
    [SerializeField] private LevelGeneration _levelGeneration;
    private GameObject currentSlot;
    private Dictionary<GameObject, SlotModel> slots = new Dictionary<GameObject, SlotModel>();
    [SerializeField] private AnimationCurve _animationCurve;
    [SerializeField] private float _animationDuration;
    private Vector3 startPos;
    public void TakeSlots()
    {
       List<SlotModel> slotList = _levelGeneration.GetSlots();//temporary solution, should get better after
       
        foreach (SlotModel slot in slotList)
        {
            slots.Add(slot.SlotGO, slot);
        }
    }
    private void Start()
    {
       // TakeSlots();
    }

    void Update()
    {
        HandleInput();

        if (_currentDraggedObject != null)
        {
            MoveObjectWithMouse();
        }
    }


    private void ReturnObjectBack()
    {

    }

    private IEnumerator MovingCoroutine(GameObject gameObject, Vector3 finalPos) 
    {
        Vector3 startpos = gameObject.transform.position;
        for(float i = 0; i < _animationDuration; i+=Time.deltaTime)
        {
            gameObject.transform.position = new Vector3 (Mathf.Lerp(startpos.x, finalPos.x, _animationCurve.Evaluate(i / _animationDuration)), Mathf.Lerp(startpos.y, finalPos.y, _animationCurve.Evaluate(i / _animationDuration)), Mathf.Lerp(startpos.z, finalPos.z, _animationCurve.Evaluate(i / _animationDuration)));
        }
        yield return null;
    }
    private void StartDrag()
    {
        bool success;
        GameObject controlGameObject;
        if(slots.Count == 0) 
            TakeSlots();
        Debug.Log("StartDrag");
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            if (hitInfo.collider.CompareTag("Slot")) 
            {
                currentSlot = hitInfo.collider.gameObject.transform.parent.gameObject;
                SlotModel slotModel = slots[currentSlot];
                string place = slotModel.SlotView.GetMousePosition(_camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 9f)).x);
                controlGameObject = slotModel.SlotView.TryGetObject(place, out success);
                if (success)
                    _currentDraggedObject = controlGameObject;
                startPos = _currentDraggedObject.transform.position;
                Debug.Log($"current drag obj == {_currentDraggedObject != null}, place - {place}, pos = {_camera.ScreenToWorldPoint(Input.mousePosition).x},pos2 = {Input.mousePosition.x}, screebn={_camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,9f))}");
            }
        }
    }


    private void EndDrag()
    {
        Debug.Log("EndDrag");
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            if (hitInfo.collider.CompareTag("Slot"))
            {
                string place = slots[hitInfo.collider.gameObject.transform.parent.gameObject].SlotView.GetMousePosition(_camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 9f)).x);
                if (slots[hitInfo.collider.gameObject.transform.parent.gameObject].SlotView.IsFreePlace(place)) {
                    bool success; 
                    slots[hitInfo.collider.gameObject.transform.parent.gameObject].SlotView.TryPutObject(_currentDraggedObject,place,out success, out Vector3 finalPos);
                    Debug.Log($"Success, ");
                }
            }
            else
            {
                // Return to original position if not dropped on a valid slot
               _currentDraggedObject.transform.position = Vector3.zero;
            }
        }
        else
        {
            // Return to original position if no raycast hit
            _currentDraggedObject.transform.position = Vector3.zero;
        }

        _currentDraggedObject = null;  // End dragging
    }

    private void HandleInput()
    {
    
        if (Input.GetMouseButtonDown(0))
        {
            StartDrag();
        }
        else if (Input.GetMouseButtonUp(0) && _currentDraggedObject != null)
        {
            EndDrag();
        }
    }


    private void MoveObjectWithMouse()
    {
        Vector3 vector = _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 9f));
            _currentDraggedObject.transform.position = new Vector3(vector.x,vector.y, -2f);// should get fixed after
        
    }

}

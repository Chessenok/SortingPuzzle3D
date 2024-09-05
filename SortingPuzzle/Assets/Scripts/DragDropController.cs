using System.Collections.Generic;
using UnityEngine;

public class DragDropController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    private GameObject _currentDraggedObject;
    [SerializeField] private LevelGeneration _levelGeneration;
    private GameObject currentSlot;
    private Dictionary<GameObject, SlotModel> slots = new Dictionary<GameObject, SlotModel>();
    


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

    private void StartDrag()
    {   
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
                string place = slotModel.SlotView.GetMousePosition(Input.mousePosition.x);
                _currentDraggedObject = slotModel.SlotView.GetObject(place);
                Debug.Log($"current drag obj == {_currentDraggedObject != null}");
            }
        }
    }


    private void EndDrag()
    {
        Debug.Log("EndDrag");
        // Raycast to check if the object is dropped on a valid slot
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            if (hitInfo.collider.CompareTag("Slot"))
            {
                string place = slots[hitInfo.collider.gameObject.transform.parent.gameObject].SlotView.GetMousePosition(Input.mousePosition.x);
                if (slots[hitInfo.collider.gameObject.transform.parent.gameObject].SlotView.IsFreePlace(place)) {
                    bool success; 
                    slots[hitInfo.collider.gameObject.transform.parent.gameObject].SlotView.TryPutObject(_currentDraggedObject,place,out success);

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
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            _currentDraggedObject.transform.position = new Vector3(hitInfo.point.x,hitInfo.point.y, 2f);// should get fixed after
        }
    }

}

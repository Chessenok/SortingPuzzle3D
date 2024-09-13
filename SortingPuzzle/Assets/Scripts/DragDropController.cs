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
    private string startPlace,finalPlace;
    private SlotModel currentSlotModel;
    private ILoseFlow loseFlow;
    private bool _lost = false;
    
    
    public void TakeSlots()
    {
       List<SlotModel> slotList = _levelGeneration.GetSlots();
       slots.Clear();
        foreach (SlotModel slot in slotList)
        {
            slots.Add(slot.SlotGO, slot);
        }
    }
    private void Start()
    {
        LevelManager.Instance.GetLoseFlow(out loseFlow);
        loseFlow.OnLoseEvent += OnLose;
        LevelManager.Instance.OnStartLevel += OnStartLevel;
    }

    private void OnStartLevel()
    {
        _lost = false;
        TakeSlots();
    }
    private void OnLose()
    {
        _lost = true;
    }

    void Update()
    {
        if (_lost) return;
        HandleInput();

        if (_currentDraggedObject != null)
        {
            MoveObjectWithMouse();
        }
    }


    private void ReturnObjectBack(GameObject gameobject, string place,SlotModel slotModel)
    {
        bool success;
        Vector3 pos;
        
        slotModel.SlotView.TryPutObject(gameobject,place,out success,out pos);
        if (success == false)
        {
            Debug.LogError($"Cannot return object back, place == {place}");
        }
        StartCoroutine(MovingCoroutine(_currentDraggedObject, pos));
    }

    private IEnumerator MovingCoroutine(GameObject gameObject, Vector3 finalPos) 
    {
        Vector3 startpos = gameObject.transform.position;
        for(float i = 0; i < _animationDuration; i+=Time.deltaTime)
        {
            gameObject.transform.position = new Vector3 (Mathf.Lerp(startpos.x, finalPos.x, _animationCurve.Evaluate(i / _animationDuration)), Mathf.Lerp(startpos.y, finalPos.y, _animationCurve.Evaluate(i / _animationDuration)), Mathf.Lerp(startpos.z, finalPos.z, _animationCurve.Evaluate(i / _animationDuration)));
            yield return null;
        }
        gameObject.transform.position = finalPos;
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
                currentSlotModel = slots[currentSlot];
                startPlace = currentSlotModel.SlotView.GetMousePosition(_camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 9f)).x);
                controlGameObject = currentSlotModel.SlotView.TryGetObject(startPlace, out success);
                if (success)
                    _currentDraggedObject = controlGameObject;
                else
                {
                    _currentDraggedObject = null;
                    startPlace = null;
                    currentSlotModel = null;
                }
                   
                
               // Debug.Log($"current drag obj == {_currentDraggedObject != null}, place - {place}, pos = {_camera.ScreenToWorldPoint(Input.mousePosition).x},pos2 = {Input.mousePosition.x}, screebn={_camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,9f))}");
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
                finalPlace = slots[hitInfo.collider.gameObject.transform.parent.gameObject].SlotView.GetMousePosition(_camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 9f)).x);
                if (slots[hitInfo.collider.gameObject.transform.parent.gameObject].SlotView.IsFreePlace(finalPlace))
                {
                    bool success;
                    slots[hitInfo.collider.gameObject.transform.parent.gameObject].SlotView.TryPutObject(_currentDraggedObject, finalPlace, out success, out Vector3 finalPos);
                    Debug.Log($"Success, ");
                    if (success)
                    {
                        StartCoroutine(MovingCoroutine(_currentDraggedObject, finalPos));
                        currentSlotModel.SlotView.OnSuccessMuteObject();
                    }
                      


                }
                else
                    ReturnObjectBack(_currentDraggedObject, startPlace, currentSlotModel);
            }
            else
                ReturnObjectBack(_currentDraggedObject, startPlace, currentSlotModel);
        }
        else
            ReturnObjectBack(_currentDraggedObject, startPlace, currentSlotModel);

        _currentDraggedObject = null; 
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

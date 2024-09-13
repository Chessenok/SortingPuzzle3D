using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;


public struct Layer
{
    public string LeftObject;
    public string RightObject;
    public string CenterObject;

    public Layer(string leftObject, string centerObject, string rightObject)
    {
        LeftObject = leftObject;
        CenterObject = centerObject;
        RightObject = rightObject;
    }
}

public class ObjectMover
{
    public static ObjectMover Instance { get; private set; }

    public void OnCreate()
    {
        if (Instance == null)
            Instance = this;
    }
    public async Task MoveObjectAsync(Transform objectToMove, Vector3 targetPosition, float duration)
    {
        Vector3 startPos = objectToMove.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            objectToMove.position = Vector3.Lerp(startPos, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            await Task.Yield();
        }
        objectToMove.position = targetPosition;
    }
}
public class SlotModel
{
    public GameObject SlotGO { get; private set; }
    public SlotView SlotView { get; private set; }
    public List<Layer> Layers;
    public int LockedFor { get; private set; }

    private List<Dictionary<string, GameObject>> layerList = new List<Dictionary<string, GameObject>>();

    public void ResetSlot()
    {
        layerList.Clear();
        Layers.Clear();
        LockedFor = 0;
    }

    public void GetFirstLayer()
    {
        SlotView.SetFirstLayer(layerList[0]);
    }

    public void AddLayer(Dictionary<string, GameObject> layer)
    {

        layerList.Add(layer);
        if (layerList.Count == 1)
        {
            SlotView.SetFirstLayer(layer);
        }
    }
    private async void MoveObjectsToNewPos(Dictionary<string, GameObject> layer)
    {   
        if(ObjectMover.Instance == null)
        {
            ObjectMover objectMover =  new ObjectMover();
            objectMover.OnCreate();
        }
        foreach(var gameObject in layer)
        {
            ObjectMover.Instance.MoveObjectAsync(gameObject.Value.transform,new Vector3(gameObject.Value.transform.position.x, gameObject.Value.transform.position.y, gameObject.Value.transform.position.z - 0.5f),1f);
        }
    }

    public bool ThereAreMoreLayers()
    {
        if (layerList.Count > 1)
        {
            return true;
        }
        return false;
    }

    public void MoveLayersAndGetNextLayer(out Dictionary<string,GameObject> gameObjects)
    {
        layerList.RemoveAt(0);
        gameObjects = layerList[0];
        foreach(var layer in layerList)
        {
            MoveObjectsToNewPos(layer);
        }
    }

    public SlotModel(GameObject slot,SlotView slotView, List<Layer> layers, int lockedFor)
    {
        SlotGO = slot;
        Layers = layers;
        LockedFor = lockedFor;
        SlotView = slotView;
        slotView.SetModel(this);
    }
}



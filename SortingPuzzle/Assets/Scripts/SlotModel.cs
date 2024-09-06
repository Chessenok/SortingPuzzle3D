using System.Collections.Generic;
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
public class SlotModel
{
    public GameObject SlotGO { get; private set; }
    public SlotView SlotView { get; private set; }
    public List<Layer> Layers;
    public int LockedFor { get; private set; }

    // public GameObject LeftGO, CenterGO, RightGO;

    private List<Dictionary<string, GameObject>> layerList = new List<Dictionary<string, GameObject>>();

    public void AddLayer(Dictionary<string, GameObject> layer)
    {
        layerList.Add(layer);
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
        foreach (var layer in layerList)
        {
            
        }
    }

    public SlotModel(GameObject slot,SlotView slotView, List<Layer> layers, int lockedFor)
    {
        SlotGO = slot;
        Layers = layers;
        LockedFor = lockedFor;
        SlotView = slotView;
    }

   // public void RemoveObjectAt(string place,)
}



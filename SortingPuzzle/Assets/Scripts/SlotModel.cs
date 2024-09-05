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
    public int LockedFor {  get; private set; }

    public GameObject LeftGO, CenterGO, RightGO;
  
    public SlotModel(GameObject slot,SlotView slotView, List<Layer> layers, int lockedFor)
    {
        SlotGO = slot;
        Layers = layers;
        LockedFor = lockedFor;
        SlotView = slotView;
    }
}



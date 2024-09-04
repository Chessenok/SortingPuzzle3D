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
    public GameObject Slot { get; private set; }
    public SlotView SlotView { get; private set; }
    public List<Layer> Layers;
    public int LockedFor {  get; private set; }

    public GameObject LeftGO, CenterGO, RightGO;
  
    public SlotModel(GameObject slot,SlotView slotView, List<Layer> layers, int lockedFor)
    {
        Slot = slot;
        Layers = layers;
        LockedFor = lockedFor;
        SlotView = slotView;
    }
}



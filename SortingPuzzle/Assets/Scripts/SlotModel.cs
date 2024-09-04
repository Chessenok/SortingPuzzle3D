using System.Collections.Generic;
using UnityEngine;

public struct Layer
{
    public string LeftObject;
    public string RightObject;
    public string CenterObject;
}
public class SlotModel
{
    public GameObject Slot { get; private set; }
    public SlotView SlotView { get; private set; }
    public List<Layer> Layers;
    public int LockedFor {  get; private set; }
  
    public SlotModel(GameObject slot, List<Layer> layers, int lockedFor)
    {
        Slot = slot;
        Layers = layers;
        LockedFor = lockedFor;
    }
}



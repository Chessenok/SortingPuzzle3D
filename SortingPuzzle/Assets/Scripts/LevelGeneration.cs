using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    private LevelData _levelData;
    private List<SlotModel> slots = new List<SlotModel>();
    private List<int> locks = new List<int>();
    private List<List<Layer>> layers = new List<List<Layer>>();
    [SerializeField] private SlotPositioning _positionateSlots;
    [SerializeField] private ObjectPostitioning _objectPostitioning;
    [SerializeField] private TestGenerate tg;
    private void OnEnable()
    {
        _levelData = tg._levelData;
    }

    private void Start()
    {
        GetData();
        CreateSlots();
        CreateObjects();
    }

    private void GetData()
    {
        layers = _levelData.GetAllLayers();
        locks = _levelData.GetAllLocks();
    }
    private void CreateSlots()
    {
        List<GameObject> golist = _positionateSlots.GetSlotsInPosition(locks.Count);
        for (int i = 0; i < locks.Count; i++)
        {
            slots.Add(new SlotModel(golist[i],golist[i].GetComponent<SlotView>(),layers[i],locks[i]));
        }
    }

    private void CreateObjects()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            SlotModel slot = slots[i];
            foreach (Layer layer in slot.Layers)
            {
                if (layer.LeftObject != "")
                {
                    slot.LeftGO = _objectPostitioning.GetObjectOnPos(slot.SlotView, "left", layer.LeftObject);
                }
                if (layer.RightObject != "")
                {
                    slot.RightGO = _objectPostitioning.GetObjectOnPos(slot.SlotView, "right", layer.RightObject);
                }
                if (layer.CenterObject != "")
                {
                    slot.CenterGO = _objectPostitioning.GetObjectOnPos(slot.SlotView, "center", layer.CenterObject);
                }
            }
        }   
    }
    
}

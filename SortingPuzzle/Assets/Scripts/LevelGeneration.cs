using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    private LevelData _levelData;
    private List<SlotModel> slots = new List<SlotModel>();
    private List<int> locks = new List<int>();
    
    private void OnEnable()
    {
        _levelData = LevelData.Instance;
    }

    private void CreateSlots()
    {
        List<List<Layer>> layers = _levelData.GetAllLayers();
        locks = _levelData.GetAllLocks();
        foreach (var layer in layers)
        {
            
        }
    }
    
}

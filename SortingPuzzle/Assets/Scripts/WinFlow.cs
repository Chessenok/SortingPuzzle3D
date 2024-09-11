using System;
using System.Collections.Generic;
using UnityEngine;

public class WinFlow : MonoBehaviour , IWinFlow
{
    public static WinFlow Instance;
   // public delegate void WinHandler (SlotModel slotModel);
    public event Action<SlotModel> OnLayerWin;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }


    public bool CheckWinSlot(Dictionary<string, GameObject> slot, SlotModel slotModel)
    {
        string centerTag = slot["center"].tag;
        if (slot["left"].CompareTag(centerTag))
        {
            if (slot["right"].CompareTag(centerTag))
            {
                Pool.Instance.TakeToPool(slot["left"]);
                Pool.Instance.TakeToPool(slot["center"]);
                Pool.Instance.TakeToPool(slot["right"]);
                OnLayerWin?.Invoke(slotModel);
                return true;
            }
        }
        return false;
    }

}

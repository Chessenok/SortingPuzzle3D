using System;
using System.Collections.Generic;
using UnityEngine;
    public class TestGenerate : MonoBehaviour
    {
        public LevelData _levelData = new LevelData();
        
        private void Awake()
        {
            _levelData.TakeTime(180);
            List<List<Layer>> list = new List<List<Layer>>();
            List<Layer> slist = new List<Layer>();
            List<Layer> tlist = new List<Layer>();
            List<Layer> qlist = new List<Layer>();
            slist.Add(new Layer("","red","green"));
            slist.Add(new Layer("yellow", "red", "yellow"));
            tlist.Add(new Layer("yellow","","green"));
            qlist.Add(new Layer("red","green",""));
            
            _levelData.GetLayersAndLockForSlot(slist,0);
            _levelData.GetLayersAndLockForSlot(tlist,0);
            _levelData.GetLayersAndLockForSlot(qlist,0);
        }
    }

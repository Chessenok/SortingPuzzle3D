using System.Collections.Generic;
using System.IO;
using UnityEngine;


[System.Serializable]
public class LayerClass
{
    public string left;
    public string center;
    public string right;
}

[System.Serializable]
public class Container
{
    public int lockValue;
    public List<LayerClass> layers;
}

[System.Serializable]
public class Level
{
    public float time;
    public List<Container> containers;
}


public class LoadFromJsonFile 
{
    private Level level;


    public LevelData LoadLevel(string filePath)
    {
        LevelData levelData = new LevelData();

        string json = File.ReadAllText(filePath);
        level = JsonUtility.FromJson<Level>(json);
        levelData.TakeTime((int)level.time);
        foreach (var container in level.containers)
        {
            List<Layer> layerStructs = new List<Layer>();
            foreach (var layer in container.layers)
            {
                layerStructs.Add(new Layer(layer.left,layer.center,layer.right));
            }
            levelData.GetLayersAndLockForSlot(layerStructs,container.lockValue);
        }
        return levelData;
    }

    public int LoadCurrentLevel(string filePath)
    {

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            
            LevelIndex levelIndex = JsonUtility.FromJson<LevelIndex>(json);

            return levelIndex.index;
        }
        else
        {
            Debug.LogError("No saved level found. Starting from level 1.");
            return 0; 
        }
    }
   

}

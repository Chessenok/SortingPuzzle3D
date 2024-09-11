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

   /* private bool IsValidLayer(Layer layer)
    {
        string[] validColors = { "red", "yellow", "green", "" };
        return System.Array.Exists(validColors, color => color == layer.left) &&
               System.Array.Exists(validColors, color => color == layer.center) &&
               System.Array.Exists(validColors, color => color == layer.right);
    }

    public void ValidateLevel(Level level)
    {
        foreach (var container in level.containers)
        {
            foreach (var layer in container.layers)
            {
                if (!IsValidLayer(layer))
                {
                    Debug.LogError("Invalid layer detected");
                }
            }
        }
    }*/

}

using System.IO;
using UnityEngine;
[System.Serializable]
public class LevelIndex
{
    public int index;
}
public class SaveToJSONFile
{

    public void SaveCurrentLevel(int currentLevel, string folderPath)
    {
        string json = JsonUtility.ToJson(new LevelIndex { index = currentLevel }, true);

        File.WriteAllText(folderPath, json);

    }
}

using UnityEngine;

[CreateAssetMenu(fileName = "LevelPaths", menuName = "Level/LevelPaths")]
public class LevelPaths : ScriptableObject
{
    [SerializeField, TextArea]
    private string[] levelFilePaths;
    [SerializeField, TextArea]
    private string saveLevelInfoPath;
    public string[] LevelFilePaths => levelFilePaths;
    public string SaveLevelInfoPath => saveLevelInfoPath;
}


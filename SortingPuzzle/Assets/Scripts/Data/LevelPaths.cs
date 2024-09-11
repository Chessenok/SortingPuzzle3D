using UnityEngine;

[CreateAssetMenu(fileName = "LevelPaths", menuName = "Level/LevelPaths")]
public class LevelPaths : ScriptableObject
{
    [SerializeField, TextArea]
    private string[] levelFilePaths;
    public string[] LevelFilePaths => levelFilePaths;
}


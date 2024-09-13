using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour 
{
    private LevelData m_Data;
    public static LevelManager Instance { get; private set; }
    [SerializeField] private LevelGeneration m_Generation;
    [SerializeField] private TestGenerate tg;
    [SerializeField] private GameObject _losePanel;
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private Timer _timer;
    [SerializeField] private WinFlow _winFlow;
    private int _currentLevelIndex;
    private LevelPaths levelPaths;
    private string savePathFile;
    public event Action OnStartLevel;
    public event Action OnWinLevel;
    private List<LevelData> levelData = new List<LevelData>();
    private LoadFromJsonFile _loader;

    private void OnEnable()
    {
        if(Instance == null) 
        { 
            Instance = this;
        }
        levelPaths = Resources.Load<LevelPaths>("LevelPaths");
        savePathFile = levelPaths.SaveLevelInfoPath;
        _loader = new LoadFromJsonFile();
        _currentLevelIndex = _loader.LoadCurrentLevel(savePathFile);
        if (levelPaths != null && levelPaths.LevelFilePaths.Length > 0)
        {
            foreach (var path in levelPaths.LevelFilePaths)
            {
               levelData.Add(_loader.LoadLevel(path));
            }
        }
    }


    private void Start()
    {
        _timer.OnLoseEvent += OnLose;
        _winFlow.OnLayerWin += OnWinLayer;
        StartLevel();
    }

    public void StartLevel()
    {
        Pool.Instance.ClearScene();
        _winPanel.SetActive(false);
        _losePanel.SetActive(false);
        m_Generation.StartLevel(levelData[_currentLevelIndex]);
        Debug.Log($"Loaded level : {_currentLevelIndex + 1}");
        OnStartLevel?.Invoke();
    }

    public void GetWinFlow(out IWinFlow winFlow)
    {
        winFlow = _winFlow; return;
    }
    
    public void GetLoseFlow(out ILoseFlow loseFlow)
    {
        loseFlow = _timer; return;
    }

    
    private void OnLose()
    {
        _losePanel.SetActive(true);
    }

    private void OnWinLayer(SlotModel slot)
    {
        if(Pool.Instance.RemainingActiveObjects() == 0)
        {
            _winPanel.SetActive(true); 
            SaveNewLevel();
            OnWinLevel?.Invoke();
        }
    }


    private void SaveNewLevel()
    {
        if (_currentLevelIndex == levelData.Count)
        {
            return;
        }
        _currentLevelIndex += 1;
        SaveToJSONFile saver = new SaveToJSONFile();
        saver.SaveCurrentLevel(_currentLevelIndex,savePathFile);
    }
}





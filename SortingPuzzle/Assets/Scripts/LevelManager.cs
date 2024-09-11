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
    private LevelPaths levelPaths;
    public event Action OnStartLevel;
    public event Action OnWinLevel;
    private List<LevelData> levelData = new List<LevelData>();
    private LoadFromJsonFile _loader;

    private void OnEnable()
    {
        if(Instance == null) {
        Instance = this;
        }
        levelPaths = Resources.Load<LevelPaths>("LevelPaths");
        _loader = new LoadFromJsonFile();
        Debug.Log($"levelpaths num = {levelPaths.LevelFilePaths.Length}");
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
        m_Generation.StartLevel(levelData[0]);
        _timer.OnLoseEvent += OnLose;
        _winFlow.OnLayerWin += OnWinLayer;
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
    public void RestartLevel()
    {
        Pool.Instance.ClearScene();
        _losePanel.SetActive(false);
       // _winPanel.SetActive(false);
        m_Generation.StartLevel(tg._levelData);
        OnStartLevel?.Invoke();
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
            OnWinLevel?.Invoke();
        }
    }
}

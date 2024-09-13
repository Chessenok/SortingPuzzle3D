using TMPro;
using UnityEngine;

public class Combo : MonoBehaviour, IUpdateCombo
{
    private int _currentCombo;
    private float _currentComboTime;
    [SerializeField] private float _totalComboTime;
    private LevelManager _levelManager;
    private IWinFlow winFlow;
    private ILoseFlow loseFlow;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;
    private bool _noCombo;
    private bool _levelFinished;
    private void Start()
    {
        _levelManager = LevelManager.Instance;
        _levelManager.GetWinFlow(out winFlow);
        winFlow.OnLayerWin += ComboUp;
        _levelManager.GetLoseFlow(out loseFlow);
        loseFlow.OnLoseEvent += OnLose;
        _levelManager.OnWinLevel += OnWin;
        _levelManager.OnStartLevel += OnStartLevel;
    }
    public void ComboUp(SlotModel slot)
    {
        if (_levelFinished)
        {
            return;
        }
        _currentComboTime = _totalComboTime;
        _currentCombo += 1;
        UpdateComboText();
        _noCombo = false;
    }

    private void OnStartLevel()
    {
        _levelFinished = false;
    }
    private void OnLose()
    {
        StopGame();
    }

    private void OnWin()
    {
        StopGame();
    }
    private void Update()
    {
        if (_currentCombo > 0)
        {
            _currentComboTime -= Time.deltaTime;
            _rectTransform.localScale = new Vector3(_currentComboTime/_totalComboTime,1f,1f);
        }
        if (_currentComboTime < Time.deltaTime && _noCombo != true)
        {
            _currentCombo = 0;
            _rectTransform.localScale = new Vector3(0f, 1f, 1f);
            UpdateComboText();
            _noCombo = true;
        }
    }
    private void StopGame()
    {
        _currentCombo = 0;
        _currentComboTime = 0f;
        UpdateComboText();
        _noCombo = true;
        _levelFinished = true;
        _rectTransform.localScale = new Vector3(0f, 1f, 1f);
    }
    private void UpdateComboText()
    {
        _textMeshProUGUI.text = $"Combo : {_currentCombo}";
    }
}

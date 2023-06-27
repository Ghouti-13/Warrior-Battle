using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance = null;
    public enum WindowType { Pause, Win, Lose }

    [SerializeField] private GameObject _gameScreen;
    [SerializeField] private TMP_Text _titleText;
    [SerializeField] private Button _resumeButton, _restartButton, _leaveButton;

    [Header("WAVES UI ")]
    [SerializeField] private Slider _wavesSlider;
    [SerializeField] private TMP_Text _wavesText;

    [Header("Enemies UI")]
    [SerializeField] private Slider _enemiesSlider;
    [SerializeField] private TMP_Text _enemiesText;

    private SceneLoader _sceneLoader;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        _sceneLoader = FindObjectOfType<SceneLoader>();
    }
    private void Start()
    {
        _resumeButton.onClick.AddListener(OnResumeClicked);
        _restartButton.onClick.AddListener(OnRestartButtonClicked);
        _leaveButton.onClick.AddListener(OnLeaveButtonClicked);
    }
    public void OpenWindow(WindowType windowType)
    {
        _gameScreen.SetActive(true);

        switch (windowType)
        {
            case WindowType.Pause:
                _titleText.text = "GAME PAUSED";
                break;

            case WindowType.Win:
                _titleText.text = "GAME OVER YOU WIN!";
                _resumeButton.gameObject.SetActive(false);
                break;

            case WindowType.Lose:
                _titleText.text = "GAME OVER YOU LOSE!";
                _resumeButton.gameObject.SetActive(false);
                break;
        }
    }
    public void SetMaxWaves(float maxAmount)
    {
        _wavesSlider.maxValue = maxAmount;
        SetWaveValue(1);
    }
    public void SetMaxEnemies(float maxEnemies)
    {
        _enemiesSlider.maxValue = maxEnemies;
        SetKillsText(0);
    }
    public void SetWaveValue(int currentWave)
    {
        _wavesText.text = "Wave : " + currentWave + " / " + _wavesSlider.maxValue;
        _wavesSlider.value = currentWave;
    }
    public void SetKillsText(int currentKills)
    {
        _enemiesText.text = "Kills : " + currentKills + " / " + _enemiesSlider.maxValue;
        _enemiesSlider.value = currentKills;
    }
    public void CloseWindow()
    {
        _gameScreen.SetActive(false);
    }
    private void OnResumeClicked()
    {
        GameManager.Instance.Pause(false);
        _gameScreen.SetActive(false);
    }
    private void OnRestartButtonClicked()
    {
        // Load the current scene...
        _sceneLoader?.LoadScene(1);
    }
    private void OnLeaveButtonClicked()
    {
        // Load the main menu scene...
        _sceneLoader?.LoadScene(0);
    }
}

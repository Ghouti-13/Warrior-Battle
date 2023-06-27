using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    public enum GameState { None, GameInProgress, GamePaused, Gameover }

    [SerializeField] private GameState gameState = GameState.None;
    [SerializeField] private int score;

    public GameState CurrentState => gameState;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        Time.timeScale = 1f;
    }
    private void Start()
    {
        gameState = GameState.GameInProgress;
    }
    private void Update()
    {
        if (gameState == GameState.Gameover) return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(gameState == GameState.GameInProgress)
            {
                Pause(true);
            }
            else
            {
                Pause(false);
            }
        }
    }
    public void Pause(bool value)
    {
        if (value)
        {
            Time.timeScale = 0f;
            Cursor.visible = true;
            gameState = GameState.GamePaused;
            UIManager.Instance.OpenWindow(UIManager.WindowType.Pause);
        }
        else
        {
            Time.timeScale = 1f;
            Cursor.visible = false;
            gameState = GameState.GameInProgress;
            UIManager.Instance.CloseWindow();
        }
    }
    public void EndGame(bool playerWon)
    {
        Time.timeScale = 0f;
        gameState = GameState.Gameover;

        StartCoroutine(EndGameRoutine(playerWon));
    }
    IEnumerator EndGameRoutine(bool playerWon)
    {
        yield return new WaitForSecondsRealtime(2f);

        Cursor.visible = true;
        UIManager.Instance.OpenWindow((playerWon) ? UIManager.WindowType.Win : UIManager.WindowType.Lose);
    }
}

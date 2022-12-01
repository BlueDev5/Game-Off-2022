using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameplayMode
{
    HomeMenu,
    Walking,
    Editing,
    Dead,
}
public class GameplayModeManager : MonoBehaviour
{
    GameplayMode gameplayMode;
    public Event<GameplayMode> OnGamePlayModeChanged;
    public GameplayMode m_GameplayMode { get { return gameplayMode; } }
    public static GameplayModeManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        OnGamePlayModeChanged += UpdateSettings;
    }

    private void UpdateSettings(GameplayMode lastMode)
    {
        if (gameplayMode == GameplayMode.Walking)
        {
            Time.timeScale = 1;
        }
        else if (gameplayMode == GameplayMode.Editing)
        {
            Time.timeScale = 0.2f;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (gameplayMode == GameplayMode.Walking)
            {
                SetState(GameplayMode.Editing);
            }
            else if (gameplayMode == GameplayMode.Editing)
            {
                SetState(GameplayMode.Walking);
            }
        }
    }

    public void SetState(GameplayMode mode)
    {
        if (gameplayMode == mode) return;

        var lastMode = gameplayMode;
        gameplayMode = mode;
        OnGamePlayModeChanged?.Invoke(lastMode);
    }
}

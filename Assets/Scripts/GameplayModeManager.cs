using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GamePlayMode
{
    HomeMenu,
    Walking,
    Editing,
    Dead,
}
public class GameplayModeManager : MonoBehaviour
{
    public GamePlayMode GamePlayMode;
    public Event<GamePlayMode> OnGamePlayModeChanged;
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

    private void UpdateSettings(GamePlayMode lastMode)
    {
        if (GamePlayMode == GamePlayMode.Walking)
        {
            Time.timeScale = 1;
        }
        else if (GamePlayMode == GamePlayMode.Editing)
        {
            Time.timeScale = 0.2f;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (GamePlayMode == GamePlayMode.Walking)
            {
                SetState(GamePlayMode.Editing);
            }
            else if (GamePlayMode == GamePlayMode.Editing)
            {
                SetState(GamePlayMode.Walking);
            }
        }
    }

    public void SetState(GamePlayMode mode)
    {
        if (GamePlayMode == mode) return;

        var lastMode = GamePlayMode;
        GamePlayMode = mode;
        OnGamePlayModeChanged?.Invoke(lastMode);
    }
}

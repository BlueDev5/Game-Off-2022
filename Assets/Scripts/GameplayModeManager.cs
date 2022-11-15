using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameplayMode
{
    Walking,
    Editing
}
public class GameplayModeManager : MonoBehaviour
{
    GameplayMode gameplayMode;
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
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (gameplayMode == GameplayMode.Walking)
            {
                gameplayMode = GameplayMode.Editing;
            }
            else if (gameplayMode == GameplayMode.Editing)
            {
                gameplayMode = GameplayMode.Walking;
            }
        }
    }
}

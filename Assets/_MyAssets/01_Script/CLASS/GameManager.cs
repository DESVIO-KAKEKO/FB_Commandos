using System;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Unknown, StartMenu, Playing, Pause, Victory, Defeat
    }

    [SerializeField] LevelEnd _lvlEnd;

    public GameState currentState;
    public GameState requestedState;

    public UnityEvent PlayerDetected;

    private void Awake()
    {
        currentState = GameState.Playing;
    }

    private void CheckObjective()
    {
        if (currentState == GameState.Playing)
        {
            currentState = GameState.Victory;
        }
    }

    private void OnEnable()
    {
        _lvlEnd.ExtractionReached.AddListener(CheckObjective);
    }
    private void OnDisable()
    {
        _lvlEnd.ExtractionReached.RemoveListener(CheckObjective);
    }
}

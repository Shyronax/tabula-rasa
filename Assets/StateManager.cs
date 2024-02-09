using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    #region Singleton
    private static StateManager m_Instance;

    public static StateManager Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = new StateManager();
            }

            return m_Instance;
        }
    }
    #endregion

    public GameState State;
    public void ChangeState(GameState NewState)
    {
        State = NewState;

        switch (NewState)
        {
            case GameState.Intro:
                break;
            case GameState.Fight:
                break;
            case GameState.Win:
                break;
            case GameState.Lose:
                break;
        }
    }
}
public enum GameState
{
    Intro,
    Fight,
    Win,
    Lose
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    [SerializeField] private GameObject m_Boss;
    [SerializeField] private GameObject m_Player;
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
    private void ChangeState(GameState NewState)
    {
        State = NewState;

        switch (NewState)
        {
            case GameState.Intro:
                m_Player.SetActive(true);
                m_Player.transform.position = new Vector3(0, 0.77f, -5.41f);
                m_Boss.SetActive(true);
                break;
            case GameState.Fight:
                break;
            case GameState.Win:
                break;
            case GameState.Lose:
                break;
        }
    }

    public void Restart() {
        ChangeState(GameState.Intro);
    }
}
public enum GameState
{
    Intro,
    Fight,
    Win,
    Lose
}


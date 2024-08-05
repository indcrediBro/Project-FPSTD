using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("References")]
    [SerializeField] private GameObject m_PlayerGO;
    [SerializeField] private GameObject m_KingGO;
    [SerializeField] private GameObject m_WeaponDealerGO;

    private bool m_isGameStarted;
    private bool m_isPaused;

    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        //TODO: Reset Score

        //TODO: Clear Enemies
        ClearEnemies();

        //TODO: Respawn Player, King, Weapon Dealer
        SpawnCharacters();

        UnpauseGame();
        m_isGameStarted = true;
    }

    public void HandlePause(bool _input)
    {
        if (m_isGameStarted && _input)
        {
            if (m_isPaused)
            {
                UnpauseGame();
                return;
            }
            PauseGame();
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
        Debug.Log("Game Paused!");
    }

    private void UnpauseGame()
    {
        Time.timeScale = 1f;
        Debug.Log("Game Unpaused!");
    }

    public void EndGame()
    {
        m_isGameStarted = false;
    }

    private void ClearEnemies()
    {

    }

    private void SpawnCharacters()
    {

    }
}

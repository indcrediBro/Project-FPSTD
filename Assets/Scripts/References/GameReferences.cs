using UnityEngine;
using System.Collections;

public class GameReferences : Singleton<GameReferences>
{
    public PlayerStats m_PlayerStats;
    public PlayerBaseHealth m_PlayerBase;

    public bool m_IsPaused;
    public bool m_IsGameOver;
}

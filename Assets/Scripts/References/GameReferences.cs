using UnityEngine;
using System.Collections;

public class GameReferences : Singleton<GameReferences>
{

    [Header("Game References")]
    public PlayerStats m_PlayerStats;
    public PlayerBaseHealth m_PlayerBase;

    public bool m_IsPaused;
    public bool m_IsGameOver;
    [Space(5)]
    [Header("VFX References")]
    public CameraShake m_CameraShake;
    public ScreenFlash m_ScreenFlash;
}

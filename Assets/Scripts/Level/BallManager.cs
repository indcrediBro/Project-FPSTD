using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    [SerializeField] private BallHealth[] m_balls;
    [SerializeField] private GameObject m_ballBase, m_houseBase;

    private void Awake()
    {
        m_houseBase.SetActive(true);
        m_ballBase.SetActive(false);
    }

    private void Update()
    {
        ChangeBase();
    }

    private bool AllBallsDestroyed()
    {
        foreach (BallHealth ball in m_balls)
        {
            if (!ball.IsDead())
            {
                return false;
            }
        }
        return true;
    }

    private void ChangeBase()
    {
        if (AllBallsDestroyed())
        {
            AudioManager.Instance.PlaySound("SFX_EasterEgg");
            m_houseBase.SetActive(false);
            m_ballBase.SetActive(true);
            DestroyAllBalls();
            Destroy(this);
        }
    }

    private void DestroyAllBalls()
    {
        foreach (BallHealth ball in m_balls)
        {
            if (ball.IsDead())
            {
                Destroy(ball.gameObject);
            }
        }
    }
}

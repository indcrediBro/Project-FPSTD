public class ScoreManager : Singleton<ScoreManager>
{
    private int m_score;

    public int GetScore()
    {
        return m_score;
    }

    public void AddScore(int amount)
    {
        m_score += amount;
    }

    public void SubtractScore(int amount)
    {
        m_score -= amount;
        if (m_score < 0) m_score = 0;
    }

    public void ResetScore()
    {
        m_score = 0;
    }
}

using System.Collections.Generic;

public class EconomyManager : Singleton<EconomyManager>
{
    private int m_playerMoney = 10;
    public int GetPlayerMoney()
    {
        return m_playerMoney;
    }

    public bool SpendMoney(int _amount)
    {
        if (m_playerMoney >= _amount)
        {
            m_playerMoney -= _amount;
            return true;
        }
        return false;
    }

    public void EarnMoney(int _amount)
    {
        m_playerMoney += _amount;
    }
}

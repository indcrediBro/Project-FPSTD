using System.Collections.Generic;

public class EconomyManager : Singleton<EconomyManager>
{
    private int m_playerMoney;
    public int GetPlayerMoney()
    {
        return m_playerMoney;
    }
    private int m_blockCost = 25;
    private Dictionary<string, int> m_TrapCosts;

    void Start()
    {
        InitializeTrapCosts();
    }

    void InitializeTrapCosts()
    {
        m_TrapCosts = new Dictionary<string, int>
        {
            { "Cannon", 150 },
            { "Crossbow", 100 },
            { "Spike", 50 },
            { "FirePit", 75 }
        };
    }


    public int GetBlockCost()
    {
        return m_blockCost;
    }

    public int GetTrapCost(string _trapName)
    {
        return m_TrapCosts.ContainsKey(_trapName) ? m_TrapCosts[_trapName] : 0;
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

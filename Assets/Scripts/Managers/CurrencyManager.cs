public class CurrencyManager : Singleton<CurrencyManager>
{
    private int m_currency;

    public int GetCurrency()
    {
        return m_currency;
    }

    public void AddCurrency(int amount)
    {
        m_currency += amount;
    }

    public void SubtractCurrency(int amount)
    {
        m_currency -= amount;
        if (m_currency < 0) m_currency = 0;
    }

    public void ResetCurrency()
    {
        m_currency = 0;
    }
}
